using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimension 
{
    private Vector3Int CurrentOrientation = new Vector3Int(0, 1, 2);


    public Dimension() 
    {
    
    }

    public Dimension(MultiCoord coord)
    {
        Coordinate = coord;
        CurrentOrientation = coord.CurrentOrientation;
        PopulateDimensionCoordinates();
    }
    public Dimension(int depth)
    {
        Coordinate = new MultiCoord(depth);
        CurrentOrientation = Coordinate.CurrentOrientation;
        PopulateDimensionCoordinates();
    }
    private MultiCoord Coordinate = new MultiCoord();

    private List<(int,float)> DimensionCoordinates = new List<(int, float)> ();

    private void PopulateDimensionCoordinates() 
    {
        for (int i = 0; i < Coordinate.Length; i++)
        {
            if (IsOrientedDimension(i)) { continue; }
            DimensionCoordinates.Add((i,Coordinate[i]));
        }
    }
    private bool IsOrientedDimension(int dimension) 
    {
        if(dimension == CurrentOrientation.x)
        {return true; }
        if (dimension == CurrentOrientation.y)
        { return true; }
        if (dimension == CurrentOrientation.z)
        { return true; }
        return false;
    }
}
