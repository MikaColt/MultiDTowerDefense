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
        get {return gameObject.transform.position; }
        set { Movement.MoveTo(value); }  
    }


    // Start is called before the first frame update
    void Start()
    {
        Movement.LookAtDestination = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }





    public float RotationMultiplier = 10f;
    public float MovementMultiplier = 10f;
    public float Speed 
    {
        get 
        {
            return Time.deltaTime * MovementMultiplier;
        }
    }


    public void MoveLeft()
    {
        Position = Vector3.left* Speed + Position;
    }
    public void MoveRight()
    {
        Position = Vector3.right* Speed + Position;
    }
    public void MoveForward()
    {
        Position = Vector3.forward* Speed + Position;
    }
    public void MoveBackward()
    {
        Position = Vector3.back* Speed + Position;
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
