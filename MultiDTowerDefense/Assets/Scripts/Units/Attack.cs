using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack 
{
    public string Name;
    public float Range;
    public float Damage;
    public float BonusDamage;
    public float Penetration;
    public float Accuracy;
    public DamageType Damage_Type;


    public Attack() { }
    public Attack(WeaponClass weapon) 
    {
        Name = weapon.AttackName;
        Damage = weapon.BaseDamage;
        Penetration = weapon.BasePenetration;
        Accuracy = weapon.BaseAccuracy;
        Damage_Type = weapon.Damage_Type;
        BonusDamage = weapon.BonusDamage;
       
    }
}
