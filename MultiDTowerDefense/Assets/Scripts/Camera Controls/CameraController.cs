using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CameraController() 
    {
    instance = this;
    }
    public static CameraController instance;

    public GameObject Camera;

    public MovementController Movement 
    {
        get 
        {
            return Camera.GetComponent<MovementController>();
        }
    }
    public Vector3 Position 
    {
        get {return Movement.LastMovementPosition; }
        set { Movement.AddWaypoint(value); }  
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }





    public float RotationMultiplier = 10f;
    public float MovementMultiplier = 10f;



    public void MoveLeft()
    {
        Position = Vector3.left* MovementMultiplier + Position;
    }
    public void MoveRight()
    {
        Position = Vector3.right* MovementMultiplier + Position;
    }
    public void MoveForward()
    {
        Position = Vector3.forward* MovementMultiplier + Position;
    }
    public void MoveBackward()
    {
        Position = Vector3.back* MovementMultiplier + Position;
    }
    public void RotateLeft()
    {
        Movement.Rotate(new Vector3(0,-1f,0)* RotationMultiplier);
    }
    public void RotateRight()
    {
        Movement.Rotate(new Vector3(0, 1f, 0)* RotationMultiplier);
    }
    public void Jump()
    {
        Position = Vector3.up + Position;
    }
}
