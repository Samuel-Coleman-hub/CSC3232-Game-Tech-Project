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
        flockingUnitManager.numUnits = enemiesNum;
        flockingUnitManager.manualStart = true;
    }

}
