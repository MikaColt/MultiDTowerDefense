using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCoord 
{
    private Vector3 CurrentCoord 
    {
        get 
        {
            return new Vector3(Coordinates[CurrentOrientation[0]], Coordinates[CurrentOrientation[1]], Coordinates[CurrentOrientation[2]]); 
        }
        set 
        {
            Coordinates[CurrentOrientation[0]] = value.x;
            Coordinates[CurrentOrientation[1]] = value.y;
            Coordinates[CurrentOrientation[2]] = value.z;
        }
    }
    public Vector3Int CurrentOrientation = new Vector3Int(0,0,0);


    public int Length {  get { return Coordinates.Count; } set { } }

    public List<float> Coordinates = new List<float> { };

    public MultiCoord() { }
    public MultiCoord(Vector3Int orientation) 
    {
        CurrentOrientation = orientation;
    }
    public MultiCoord(Vector3Int orientation, int size)
    {
        CurrentOrientation = orientation;
        for (int i = 0; i < Coordinates.Count; i++) 
        { Coordinates[i] = 0; }
    }
    public MultiCoord(int size)
    {

        for (int i = 0; i < Coordinates.Count; i++)
        { Coordinates[i] = 0; }
    }

}
