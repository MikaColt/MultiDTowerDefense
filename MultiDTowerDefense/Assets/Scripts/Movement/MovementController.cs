using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    private GameObject Parent 
    {
        get 
        {
            return gameObject;
        }
    }
    private Vector3 Position 
    {
        get 
        {
            return Parent.transform.position;
        }
        set 
        {
            Parent.transform.position = value;
        }
    }
    private float Speed;
    public Vector3 Destination;
    private Vector3 Lerp() 
    {
        return new Vector3();
    }
    public void MoveTo(Vector3 destination) 
    {
        Destination = destination;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Position != Destination)
        {
            Position = Destination;
        }
    }
}
