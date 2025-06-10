using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCoord 
{
    private Vector3 CurrentCoord 
    {
        get { return _CurrentCoord; }
        set { _CurrentCoord = value; }
    }
    private Vector3 _CurrentCoord = new Vector3();
    private Vector3Int CurrentOrientation { get { return _CurrentOrientation; } set { _CurrentOrientation = value; } }
    private Vector3Int _CurrentOrientation = new Vector3Int();


    private int Length {  get { return Coordinates.Count; } set { } }

    private List<float> Coordinates = new List<float> { };

    public MultiCoord() { }
    public MultiCoord(Vector3Int orientation) 
    {
        CurrentOrientation = orientation;
    }


}
