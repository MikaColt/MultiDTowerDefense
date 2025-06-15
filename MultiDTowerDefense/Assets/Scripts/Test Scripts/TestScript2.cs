using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript2 : MonoBehaviour
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
            //   float randx = 1;
            //rand[(-1, 1)];
            //           float randy = rand[(-1, 1)];
            //     float randz = 1;
            //rand[(-1, 1)];

            Movement.Rotate(new Vector3(0,1,0));
                //(new Vector3(randx,0f,randz));

            _Timer = 0;
     //       Movement.Rotation = Movement.DesiredRotation;
//            Debug.Log($"{Movement.Rotation}");
        }
    }
}
