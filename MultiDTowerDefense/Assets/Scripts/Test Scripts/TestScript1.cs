using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript1 : MonoBehaviour
{


    //makes cubesource object move randomly
    public GameObject CubeSource;
    public MovementController Movement 
    {
        get
        {
            return CubeSource.GetComponent<MovementController>();
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private int _Timer = 0;
    private int _TimerDuration = 250;
    private Vector3 lastDestination = new Vector3();
    void Update()
    {
        _Timer += 1;
        if (_Timer >= _TimerDuration)
        {


            RandomFloat rand = new RandomFloat();
            float randx = rand[(-10, 10)];
 //           float randy = rand[(-1, 1)];
            float randz = rand[(-10, 10)];
            Vector3 destination = (new Vector3(randx, 0f, randz) + lastDestination);
            destination.y = 1f;
            Movement.AddWaypoint(destination);
            lastDestination = destination;

            _Timer = 0;
        }
    }
}
