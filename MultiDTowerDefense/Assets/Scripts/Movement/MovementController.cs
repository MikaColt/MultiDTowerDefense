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
    public Quaternion Rotation
    {
        get
        {
            return Parent.transform.rotation;
        }
        set
        {
            Parent.transform.rotation = value;
        }
    }


    private float Speed = 0.005f;
    public Vector3 Destination = new Vector3();
    private Vector3 Lerp(float amount) 
    {
        Vector3 lerp = Vector3.Lerp(Position, Destination, amount);
        return lerp;
    }
    public void MoveTo(Vector3 destination) 
    {
        Destination = destination;
    }
    public void Rotate(Vector3 rotation)
    {
        Parent.transform.Rotate(rotation);
    }
    public void LookAt(Vector3 target) 
    {

        Parent.transform.LookAt (target);
    }
    public void AddWaypoint(Vector3 destination) 
    {
    Waypoints.Add(destination);
    }

    public List<Vector3> Waypoints = new List<Vector3>();



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    float AccuracyThreshold = 1f;
    void Update()
    {
        float distance = Vector3.Distance(Position, Destination);
        if ( distance > AccuracyThreshold)
        {

            Position = Lerp(Speed);
            distance = Vector3.Distance(Position, Destination);
            if (distance <= AccuracyThreshold) 
            {
                if (Waypoints.Count > 0)
                {
                    Destination = Waypoints[0];
                    LookAt(Destination);
                    Waypoints.RemoveAt(0);
                }
            }
        }
        else
        {
            if (Waypoints.Count > 0)
            {
                Destination = Waypoints[0];
                LookAt(Destination);
                Waypoints.RemoveAt(0);
            }
        }
        
        /*
        if (Rotation != DesiredRotation)
        {
            Debug.Log($"rotating {Rotation} => {DesiredRotation}");
            Rotation = DesiredRotation;
            Debug.Log($"rotated {Rotation}");
        }
        */
}
}
