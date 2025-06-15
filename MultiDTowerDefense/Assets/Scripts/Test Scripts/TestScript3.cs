using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript3 : MonoBehaviour
{
    public GameObject Target;
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


            Movement.LookAt(Target.transform.position);

                       _Timer = 0;
        }
    }
}
