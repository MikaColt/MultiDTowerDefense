using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionHandler : MonoBehaviour
{
    private Vector3Int CurrentOrientation { get { return _CurrentOrientation; } set { _CurrentOrientation = value; } }
    private Vector3Int _CurrentOrientation = new Vector3Int();

    private Dimension CurrentDimension = new Dimension();

    // Start is called before the first frame update
    void Start()
    {
        CurrentDimension = new Dimension(4);
        CurrentOrientation = new Vector3Int(0,1,2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
