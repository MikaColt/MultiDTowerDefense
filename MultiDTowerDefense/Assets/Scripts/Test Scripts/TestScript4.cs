using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript4 : MonoBehaviour
{

    public int despawnInterval = 100;
    public int despawnTimer = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        despawnTimer += 1;
        if (despawnTimer > despawnInterval*60f)
        {
            gameObject.GetComponent<UnitSpawner>().DeSpawnUnit(0);
            despawnTimer = 0;
        }
    }
}
