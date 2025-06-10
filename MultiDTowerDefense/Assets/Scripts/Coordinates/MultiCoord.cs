using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCoord 
{
    private Vector3 CurrentCoord 
    {
        get { return new Vector3(0, 0, 0); }
        set { }
    }
    private Vector3Int CurrentOrientation { get { return new Vector3Int(0, 0, 0); } set { } }

    private int Length {  get { return 0; } }
}
