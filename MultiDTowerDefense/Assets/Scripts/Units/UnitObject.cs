using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitObject : MonoBehaviour
{
    private Health HP = new Health();
    private bool IsAlive 
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
        DamageType type = attack.Damage_Type;
        (float, float) defenses = Defenses[type];
        float damage = attack.Damage;
        float avoidance = defenses.Item1;
        float resistance = defenses.Item2;
        float penetration = attack.Penetration;
        float total = damage + penetration - resistance;
        bool hit = true;

        if (total > 0)
        {
            LoseHP(total);
        }
        
        return hit;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
