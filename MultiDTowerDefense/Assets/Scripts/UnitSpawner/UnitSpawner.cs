using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public List<Vector3> Waypoints = new List<Vector3> ();



    public bool Summoning = false;
    public bool CanSummon 
    {
    get 
        {
        return SpawnObjects.Count > 0;
        }
    }
    public Vector3 SpawnLocation 
        {
        get 
        {
        return gameObject.transform.position;
        }
        set
        {
             gameObject.transform.position = value;
        }
    }
    public UnitObject SpawningUnit;
    public List<UnitObject> SpawnQueue = new List<UnitObject> ();
    public List<GameObject> SpawnObjects = new List<GameObject> ();
    public List<GameObject> SpawnedObjects = new List<GameObject>();
    public float SpawnTimer = 5f;
    public float SpawnSummonDuration = 1f;
    public void SpawnUnit() 
    {
        SpawnQueue.Add(SpawningUnit);
    }
    public void EnableSpawnObject(int index) 
    {
        SpawnObjects[index].SetActive(true);
    }
    public void EnableSpawnedObject(int index)
    {
        SpawnedObjects[index].SetActive(true);
    }
    public void EnableSpawnedObject()
    {
        int index = SpawnedObjects.Count - 1;
        SpawnedObjects[index].SetActive(true);
        SpawnedObjects[index].GetComponent<UnitObject>().SpawnerUnitIndex = index;
        SpawnedObjects[index].GetComponent<MovementController>().TeleportTo(SpawnLocation);
    }
    public void DeSpawnUnit(int index) 
    {
        DisableSpawnedObject(index);
        SpawnObjects.Add(SpawnedObjects[index]);
        SpawnedObjects.Remove(SpawnedObjects[index]);
    }
    public void DisableSpawnedObject(int index) 
    {
        SpawnedObjects[index].SetActive(false);
    }

    public void SpawnQueueCheck() 
    {
        if (SpawnObjects.Count > 0 && SpawnQueue.Count > 0)
        {
  
            SpawnedObjects.Add(SpawnObjects[0]);
            SpawnObjects.RemoveAt(0);
            SpawnQueue.RemoveAt(0);
            Summoning = true;
        }
    }
    public void CompleteSummoning() 
    {
        Summoning = false;
        EnableSpawnedObject();
    }
    public void InitialDisable() 
    {
    for (int i = 0; i < SpawnObjects.Count; i++) { SpawnObjects[i].SetActive(false); }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitialDisable();
    }

    // Update is called once per frame

    private float updateTimer = 0f;
    private float updateSummonTimer = 0f;
    void Update()
    {
        updateTimer += 1;
        if (Summoning)
        {
            updateSummonTimer += 1;
        }
        if (updateTimer >= SpawnTimer*360f) 
        {
        updateTimer = 0f;
            SpawnUnit();
        }

        if (updateSummonTimer >= SpawnSummonDuration * 360f)
        { 
        updateSummonTimer =0f;
            CompleteSummoning();

        }
        if (Summoning == false && CanSummon)
        {

            SpawnQueueCheck();
        }

    }
}
