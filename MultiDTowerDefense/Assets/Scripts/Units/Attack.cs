using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack 
{
    public string Name = "Attack";
    public float Range = 1f;
    public float Damage = 10f;
    public float Penetration = 0.01f;
    public float Accuracy = 0.9f;
    public DamageType Damage_Type = DamageType.Ballistic;
}
