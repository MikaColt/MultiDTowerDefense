using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMachineGun : MachineGunClass
{
public TestMachineGun() : base() 
    {
        Name = "Basic Machine Gun";
        Weight = 2500f;
        BaseAttackSpeed = 5f;
        BasePenetration = 0.1f;
        BaseDamage = 10f;
        BonusDamage = 10f;

        Range = 20f;
        Damage_Type = DamageType.Ballistic;
        BaseAccuracy = 0.8f;

        ClipCapacity = 1000f;
        CurrentAmmo = 10000f;


    }
}


