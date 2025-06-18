using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health 
{
    //checks whether Hp is greater than 0
    public bool HasHP 
    {
        get 
        {
            return HP > 0;
        }
    }
    // keeps track of current HP
    public float HP = 0f;
    //keeps track of max base HP
    public float MaxHP = 100f;
    //returns current HP percent of Max (0-1 for 0-100%)
    public float HP_Percent 
    {
        get 
        {
            if (HP == 0)
            {
                return 0f;
            }
            return HP / MaxHP;
        }
        set 
        {
            SetHP_Percent(value);
        }
    }


    //bool check for whether HP is allowed to exceed max hp
    private bool Hp_Overflow = false;

    //function for losing hp
    public void LoseHP(float amount) 
    {
        HP -= amount;
    }
    //function for gaining hp
    public void GainHP(float amount) 
    {
        HP += amount;
        if (Hp_Overflow != true && HP > MaxHP)
        {
            SetHP_Max();
        }
    }
    //function for setting hp directly
    public void SetHP(float amount) 
    {
        HP = amount;
    }
    //function for setting hp to max
    public void SetHP_Max() 
    {
        HP = MaxHP;
    }
    //function for setting hp to a percent of max. input betweeen 0-1 for 0-100 %
    public void SetHP_Percent(float percent) 
    {
        HP = MaxHP * percent;
    }

    public void InitHP(float max) 
    {
    MaxHP = max;
        SetHP_Max();
    }
}
