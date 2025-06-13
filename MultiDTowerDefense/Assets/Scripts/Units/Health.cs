using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health 
{
    private float HP = 0f;

    public void LoseHP(float amount) 
    {
        HP -= amount;
    }
    public void GainHP(float amount) 
    {
        HP += amount;
    }
}
