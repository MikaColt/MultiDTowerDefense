using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public bool LookAtDestination = true;
    public bool IsMoving 
    {
        get 
        {
        return !HasArrived && Waypoints.Count ==0;
        }
    }
    public bool PauseMovement = false;
    public bool CanMove = true;

    public bool IsFollowing = false;
    public UnitObject FollowTarget;

    private GameObject Parent 
    {
        get 
        {
            return gameObject;
        }
    }
    public Vector3 Position 
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


    public float Speed = 1f;
    public Vector3 Destination = new Vector3();
    private Vector3 Lerp(float amount) 
    {
        Vector3 lerp = Vector3.Lerp(Position, Destination, amount);
        return lerp;
    }
    private Vector3 LerpFollow(float amount)
    {
        Vector3 lerp = Vector3.Lerp(Position, FollowTarget.transform.position, amount);
        return lerp;
    }
    public void MoveTo(Vector3 destination) 
    {
        Destination = destination;
    }
    public void TeleportTo(Vector3 position)
    {
        Position = position;
        Destination = position;
        ClearWaypoints();
    }


    public void Rotate(Vector3 rotation)
    {
        Parent.transform.Rotate(rotation);
    }
    public void LookAt(Vector3 target) 
    {

        Parent.transform.LookAt (target);
    }


    public void SetWaypoints (List<Vector3> points)
    {
    Waypoints = points; 
    }
    public Vector3 FirstWaypoint 
    {
    get { return Waypoints[0]; }
    }
    public Vector3 LastWaypoint
    {
        get { return Waypoints[Waypoints.Count-1]; }
    }
    public void AddWaypoint(Vector3 destination) 
    {

        if (Waypoints.Count < MaxWaypoints)
        {
            Waypoints.Add(destination);
        }
    }
    public void ClearWaypoints() 
    {
    Waypoints = new List<Vector3>();
    }
    public List<Vector3> Waypoints = new List<Vector3>();
    public int MaxWaypoints = 25;
    public bool HasWaypoints { get { return Waypoints.Count > 0; } }

    public Vector3 LastMovementPosition 
    {
        get 
        {
            if (HasWaypoints) { return LastWaypoint; }
            else if (HasArrived) { return Position; }
            else {return Destination; }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    float AccuracyThreshold = 1f;
    void Update()
    {
        
        if (PauseMovement) {return;}

        if (IsFollowing)
        {
            LerpFollow(Speed * Time.deltaTime);
            return;
        }


        float distance = Vector3.Distance(Position, Destination);
        if (HasArrived == false)
        {

            Position = Lerp(Speed*Time.deltaTime);

            if (HasArrived) 
            {
                if (Waypoints.Count > 0)
                {
                    Destination = Waypoints[0];
                    if (LookAtDestination)
                    {
                        LookAt(Destination);
                    }
                    Waypoints.RemoveAt(0);
                }
            }
        }
        else
        {
            if (Waypoints.Count > 0)
            {
                Destination = Waypoints[0];
                if (LookAtDestination)
                {
                    LookAt(Destination);
                }
                Waypoints.RemoveAt(0);
            }
        }
        

}
    public bool HasArrived 
    {
        get 
        {
            float distance = Vector3.Distance(Position, Destination);
            return distance <= AccuracyThreshold;
        }
    }

}
