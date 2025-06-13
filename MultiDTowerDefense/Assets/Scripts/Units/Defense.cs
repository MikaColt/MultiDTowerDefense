using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense 
{
    //returns (Resistance,avoidance)
    public (float,float) this[DamageType index]
    {
        get
        {
            return (Resistances[index],Avoidances[index]);
        }
    }
    //keeps track of resistances to damage types
    public Dictionary<DamageType, float> Resistances = new Dictionary<DamageType, float>();
   //keeps track of avoidances for damage types
    public Dictionary<DamageType, float> Avoidances = new Dictionary<DamageType, float>();

    public void Initialization() 
    {
        foreach (DamageType type in Enum.GetValues(typeof(DamageType)))
        {
            Resistances[type] = 1f;
            Avoidances[type] = 0f;
        }
    }
}
