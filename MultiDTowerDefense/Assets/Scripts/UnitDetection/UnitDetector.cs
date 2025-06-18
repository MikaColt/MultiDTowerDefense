using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class UnitDetector : MonoBehaviour
{
    public UnitObject ParentUnit 
    {
        get 
        {
            return gameObject.GetComponent<UnitObject>();
        } 
    }
    public int FactionID = -1;

    public float DetectionRange = 20f;
    public Vector3 Center 
    {
        get { return gameObject.transform.position; }
    }

    // Start is called before the first frame update
    void Start()
    {
        FactionID = ParentUnit.FactionID;
    }

    // Update is called once per frame

    public float updateTimer = 0;
    public float updateSpeed = 1000f;
    void Update()
    {

        updateTimer += 1;

        if (updateTimer >= updateSpeed)
        {
            /*
            Collider[] hitColliders = Physics.OverlapSphere(Center, DetectionRange);
            foreach (var hitCollider in hitColliders)
            {
                //        hitCollider.SendMessage("AddDamage");
                Debug.Log("Within Range");
            }
            */
            HitColliders = Physics.OverlapSphere(Center, DetectionRange);
            updateTimer = 0;
            //   ParseClosestUnit();
            ParseClosestEnemy();
        }
    }
    public Collider[] HitColliders;
    public List<UnitObject> UnitsWithinRange;
    public UnitObject ClosestUnit;

    public bool UnitsAreInRange 
    {
        get { return UnitsWithinRange.Count > 0; }
    }
    public async Task ParseClosestUnit() 
    {
       List<UnitObject> unitsInRange = new List<UnitObject> ();
        Collider[] hitColliders = HitColliders;
        if (hitColliders.Length == 0) { return; }
        List<(int, float)> distances = new List<(int, float)>();
        for (int i = 0; i < hitColliders.Length-1; i++)
        {
            if (hitColliders[i].gameObject.tag == "Unit")
            {

                UnitObject unit = hitColliders[i].gameObject.GetComponent<UnitObject>();
                distances.Add((i,Vector3.Distance(Center, hitColliders[i].gameObject.transform.position)));
                unitsInRange.Add(unit);
            }

            await Task.Delay(100);
        }
        if (distances.Count == 0) { return; }
        UnitsWithinRange = unitsInRange;
        distances.Sort((d1, d2) => d1.Item2.CompareTo(d2.Item2));
        ClosestUnit = hitColliders[distances[0].Item1].gameObject.GetComponent<UnitObject>() ;
 //       Debug.Log(ClosestUnit.Name);
    }



    public List<UnitObject> EnemiesWithinRange;
    public UnitObject ClosestEnemy;



    public bool EnemiesAreInRange
    {
        get { return EnemiesWithinRange.Count > 0; }
    }
    public async Task ParseClosestEnemy()
    {
        List<UnitObject> unitsInRange = new List<UnitObject>();
        Collider[] hitColliders = HitColliders;
        if (hitColliders.Length == 0) { return; }
        List<(int, float)> distances = new List<(int, float)>();
        for (int i = 0; i < hitColliders.Length - 1; i++)
        {
            if (hitColliders[i].gameObject.tag == "Unit")
            {

                UnitObject unit = hitColliders[i].gameObject.GetComponent<UnitObject>();
                if (FactionID != unit.FactionID)
                {
                    distances.Add((i, Vector3.Distance(Center, hitColliders[i].gameObject.transform.position)));
                    unitsInRange.Add(unit);
                }
            }

            await Task.Delay(100);
        }
        if (distances.Count == 0) { return; }
        EnemiesWithinRange = unitsInRange;
        distances.Sort((d1, d2) => d1.Item2.CompareTo(d2.Item2));
        ClosestEnemy = hitColliders[distances[0].Item1].gameObject.GetComponent<UnitObject>();
        //       Debug.Log(ClosestUnit.Name);
    }


}
