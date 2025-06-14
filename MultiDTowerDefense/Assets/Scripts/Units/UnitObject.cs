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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
