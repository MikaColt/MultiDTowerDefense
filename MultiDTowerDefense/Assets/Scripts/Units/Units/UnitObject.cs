using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitObject : MonoBehaviour
{
    public int FactionID = 0;


    public UnitDetector UnitDetector { get { return gameObject.GetComponent<UnitDetector>(); } }
    public MovementController Movement
    {
        get { return gameObject.GetComponent<MovementController>(); }
    }



    public string Name;
    public ActionMode Action = ActionMode.Idle;

    public bool HasWaypoints { get { return Movement.HasWaypoints; } }
    public bool CanMove { get { return Movement.CanMove; } }
    public int SpawnerUnitIndex = 0;
    public UnitSpawner Spawner;

    public float DetectionRange = 30f;

    public UnitObject TargetedUnit;
    public bool TargetedUnitWithinRange
    {
        get
        {
            return Vector3.Distance(gameObject.transform.position, TargetedUnit.gameObject.transform.position) <= DetectionRange;
        }

    }
    public void TargetClosestUnit()
    {
        if (UnitDetector.EnemiesAreInRange)
        {
            TargetedUnit = UnitDetector.ClosestEnemy;
        }
        else
        {
            TargetedUnit = null;
        }
    }
    public float TargetedUnitDistance
    {
        get { return Vector3.Distance(gameObject.transform.position, TargetedUnit.gameObject.transform.position); }
    }
    public bool HasTargetedUnit { get { return TargetedUnit != null; } }
    public void ClearTarget() { TargetedUnit = null; }

    public bool UnitsAreInRange
    {
        get { return UnitDetector.EnemiesAreInRange; }
    }


    public bool IsMoving 
    {
        get { return Movement.IsMoving; }
    }
    public bool IsFollowing 
    {
        get 
        {
            return Movement.IsFollowing; 
        }
    }
    public void SetFollowTarget(UnitObject target) 
    {
        MovementController movement = Movement;
        movement.FollowTarget = target;
        movement.IsFollowing = true;
    }

    public InventoryClass Inventory = new InventoryClass();
    public void AddToInventory(ItemClass item)

    {
        Inventory.AddToInventory(item);
    }
    public void AddAndEquipWeapon(WeaponClass weapon)

    {
        Inventory.AddToInventory(weapon);
        EquippedWeapon = weapon;
    }
    public WeaponClass EquippedWeapon;
    public bool HasEquippedWeapon
    { 
    get{ return (EquippedWeapon != null); }
    }
    public float WeaponRange
    {
        get
        {
            if (HasEquippedWeapon)
            {
                return EquippedWeapon.Range;
            }
            return -1f;
        } }
    public Health HP = new Health();
    public bool IsAlive 
    {
        get 
        {
            return HP.HasHP;
        }
    }
    public void Die() 
    {
        if (Spawner != null) 
        {

            Spawner.DeSpawnUnit(SpawnerUnitIndex);
        }
        Debug.Log($"{Name} has died.");
    }
    public void LifeCheck() 
    {
        if (!IsAlive)
        {
            Die();
        }
    }


    public void LoseHP(float amount)
    {
        HP.LoseHP(amount);
//        Debug.Log($"{HP.HP_Percent*100f} | {HP.HP}");
        LifeCheck();
    }
    public void GainHP(float amount)
    {
        HP.GainHP(amount);
    }
    public void SetHP(float amount)
    {
        HP.SetHP(amount);
    }

    public void SetHP_Max()
    {
        HP.SetHP_Max();
    }
    public void SetHP_Percent(float percent)
    {
        HP.SetHP_Percent(percent);
    }

    public Defense Defenses = new Defense();
    public Attack AttackStats
    {
        get 
        {
            Attack attack = new Attack(EquippedWeapon);
            return attack;
        
        }
    }
    public bool RecieveAttack(Attack attack) 
    {
        RandomFloat rand = new RandomFloat();
        

        DamageType type = attack.Damage_Type;
        (float, float) defenses = Defenses[type];
        
        float accuracy = attack.Accuracy;
        float avoidance = defenses.Item1;
        float totalAccuracy = (accuracy - avoidance)*100f;

        float randResult = rand[(0f,100f)];
        bool hit = (randResult >= (100f- totalAccuracy));



        float damage = attack.Damage;
        float bonusDamage = attack.BonusDamage * rand[(0,1)];
        float resistance = defenses.Item2;

        float penetration = attack.Penetration;
        float total = damage+bonusDamage + penetration - resistance;
        if (total < 0)
        {
            total = 0;
        }
        if (total > 0 && hit)
        {
            LoseHP(total);
        }
//        Debug.Log($"{randResult} | {100f-totalAccuracy} | {total} {type}");
        return hit;

    }


    public void AttackTarget() 
    {
        //      Debug.Log("Attacking");
        //       Debug.Log($"Attacking: {TargetedUnit.RecieveAttack(AttackStats)}");

        if (TargetedUnit.IsAlive == true)
        {
            TargetedUnit.RecieveAttack(AttackStats);
            AttackCooldownTimer = EquippedWeapon.WeaponSpeed;
        }
            if (TargetedUnit.IsAlive == false) 
        {
            ClearTarget();
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        UnitDetector.DetectionRange = DetectionRange;
        HP.InitHP(100f);
    }


    public float ProcessingSpeed = 0.025f;
    // Update is called once per frame


    public float AttackCooldownTimer = 0f;
    public float ProcessingTimer = 0f;
    public void Update()
    {
        ProcessingTimer += Time.deltaTime;
        if (AttackCooldownTimer > 0)
        {
            AttackCooldownTimer -= Time.deltaTime;
        }
        if (ProcessingTimer >= ProcessingSpeed)
        {
            switch (Action)
            {

                case ActionMode.Movement:
                    ProcessMovement();
                        break;
                case ActionMode.Attack:
                    if (AttackCooldownTimer > 0)
                    {
                        ProcessingTimer = ProcessingSpeed - AttackCooldownTimer;
                        return;
                    }



                    ProcessAttack();
                    break;
                case ActionMode.Stunned:
                    ProcessStunned();
                    break;
                case ActionMode.Idle:
                    ProcessIdle();
                    break;
                default:
                    break;
            }
            ProcessingTimer = 0f;
        }

    }



    public void ProcessMovement() 
    {
        if (HasTargetedUnit)
        {
            Process_Movement_HasTargetedUnit();
        }
        else if (UnitsAreInRange)
        {
            TargetClosestUnit();
        }
        else if (HasWaypoints)
        {
           Movement.IsFollowing = false;
        }
        else 
        {
            SetIdleMode();
        }
    }

    private void Process_Movement_TargetInRangeOfWeapon(float distance) 
    {
        if (distance <= WeaponRange)
        {
            SetAttackMode();
        }
        else
        {
            SetFollowTarget(TargetedUnit);
        }
    }
    private void Process_Movement_HasTargetedUnit() 
    {
        float distance = TargetedUnitDistance;
        if (distance <= DetectionRange)
        {
            Process_Movement_TargetInRangeOfWeapon(distance);
        }
        else if (UnitsAreInRange)
        {
            TargetClosestUnit();
        }
        else
        {
            ClearTarget();

        }
    }


    public void ProcessAttack() 
    {
        if (HasTargetedUnit)
        {
 //           Debug.Log("Test1");
            Process_Attack_HasTargetedUnit();
        }
        else if (UnitsAreInRange)
        {
            TargetClosestUnit();
        }
        else if (CanMove) 
        {
         if (HasWaypoints)
            {
                SetMovementMode();
            } 
        }
        else
        {
            SetIdleMode();
        }
    }
    private void Process_Attack_HasTargetedUnit()
    {
        float distance = TargetedUnitDistance;
        if (distance <= DetectionRange)
        {
//            Debug.Log("Test2");
            Process_Attack_TargetInRangeOfWeapon(distance);
        }
        else if (UnitsAreInRange)
        {
            TargetClosestUnit();
        }
        else
        {
            Process_Attack_HasTargetButNoUnitsInRange();

        }
    }
    private void Process_Attack_HasTargetButNoUnitsInRange() 
    {
        ClearTarget();
        if (CanMove)
        {
            if (HasWaypoints)
            {
                SetMovementMode();
            }
        }
        else 
        {
            SetIdleMode();
        }
    }
    private void Process_Attack_TargetInRangeOfWeapon(float distance)
    {
 //       Debug.Log($"{distance} |{ WeaponRange}");
        if (distance <= WeaponRange)
        {
            AttackTarget();
        }
        else if (UnitsAreInRange)
        {
            TargetClosestUnit();
        }
        else if (CanMove)
        {
            if (HasWaypoints)
            {
                SetFollowTarget(TargetedUnit);
                SetMovementMode();
            }
        }
        else 
        {
            SetIdleMode();
        }
    }



    public void ProcessStunned() 
    {
    
    }
    public void ProcessIdle() 
    {
        if (UnitsAreInRange)
        {
            TargetClosestUnit();
            SetAttackMode();
        }
        else if (CanMove)
        {
            if (HasWaypoints)
            {
                SetMovementMode();
            }
        }
    }



    private void SetAttackMode() 
    {
        Action = ActionMode.Attack;
        Movement.PauseMovement = true;
    }
    private void SetMovementMode() 
    {
        Action = ActionMode.Movement;
        Movement.PauseMovement = false;
    }
    private void SetIdleMode() 
    {
        Action = ActionMode.Idle;
        Movement.PauseMovement = true;
    }
    private void SetStunnedMode() 
    {
        Action = ActionMode.Stunned;
        Movement.PauseMovement = true;
    }



}
