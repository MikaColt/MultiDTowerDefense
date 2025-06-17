using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClass : ItemClass
{
    public Attack AttackStats 
    {
    get 
        {
       return new Attack(this);
        }
    }


    public GunTypeEnum Type;
    public float Range;
    public float BaseDamage;
    public float BasePenetration;
    public float BaseAttackSpeed;
    public float BaseAccuracy;

    public string AttackName;

    public DamageType Damage_Type;
}
