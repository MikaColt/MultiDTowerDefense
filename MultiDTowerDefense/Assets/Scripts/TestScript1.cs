using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript1 : MonoBehaviour
{
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
    private int _TimerDuration = 50;
    void Update()
    {
        _Timer += 1;
        if (_Timer >= _TimerDuration)
        {


            RandomFloat rand = new RandomFloat();
            float randx = rand[(-1, 1)];
 //           float randy = rand[(-1, 1)];
            float randz = rand[(-1, 1)];

            Movement.Destination = (new Vector3(randx, 0f, randz) + Movement.Destination);

            _Timer = 0;
        }
    }
}
