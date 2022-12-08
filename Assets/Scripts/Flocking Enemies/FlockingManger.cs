using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PSFlocking;

public class FlockingManger : MonoBehaviour
{
    [SerializeField] PSUnitManager flockingUnitManager;
    [SerializeField] GameObject flockingUnitPrefab;
    [SerializeField] Transform spawnLocation;

    private void Awake()
    {
        flockingUnitManager.goal = GameObject.FindGameObjectWithTag("PlayerBase");
        
    }

    private void Start()
    {
        flockingUnitManager.units.ForEach(unit => unit.GetComponent<Transform>().position = this.transform.position);
    }

    public void SpawnEnemies(int enemiesNum)
    {
        for(int i = 0; i < enemiesNum; i++)
        {
            GameObject flockingObj = Instantiate(flockingUnitPrefab);
            flockingUnitManager.AddFlockingUnit(flockingObj);
        }
    }

    public bool IsAlive()
    {
        if(flockingUnitManager.units.Count == 0)
        {
            return true;
        }
        return false;
    }

}
