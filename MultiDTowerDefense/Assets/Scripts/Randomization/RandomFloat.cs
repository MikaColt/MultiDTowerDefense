using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFloat
{


    public float this[(float, float) minMax]
    {
        get
        {
            
            return Random.Range(minMax.Item1, minMax.Item2);
        }
    }
}
