using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitObject : MonoBehaviour
{
    public string Name;

    public int SpawnerUnitIndex = 0;

    public float DetectionRange = 30f;


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



    public Health HP = new Health();
    public bool IsAlive 
    {
        get 
        {
            return HP.HasHP;
        }
    }

    public void LoseHP(float amount)
    {
        HP.LoseHP(amount);
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
    public List<Attack> Attacks = new List<Attack>();
    public Attack MakeAttack(int index) 
    {
        return Attacks[index];
    }
    public bool RecieveAttack(Attack attack) 
    {
        RandomFloat rand = new RandomFloat();
        

        DamageType type = attack.Damage_Type;
        (float, float) defenses = Defenses[type];
        
        float accuracy = attack.Accuracy;
        float avoidance = defenses.Item1;
        float totalAccuracy = accuracy - avoidance;

        float randResult = rand[(0f,100f)];
        bool hit = (randResult >= (100f- totalAccuracy));



        float damage = attack.Damage;
        float resistance = defenses.Item2;

        float penetration = attack.Penetration;
        float total = damage + penetration - resistance;
        if (total < 0)
        {
            total = 0;
        }
        if (total > 0 && hit)
        {
            LoseHP(total);
        }
        
        return hit;

    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<UnitDetector>().DetectionRange = DetectionRange; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
