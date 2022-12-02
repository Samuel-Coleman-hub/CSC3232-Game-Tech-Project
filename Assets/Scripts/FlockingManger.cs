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
        
        //flockingUnitManager.spawnRange = spawnLocation.position;
        //flockingUnitManager.AddFlockingUnit(flockingUnitPrefab);
        
    }

    private void Start()
    {
        flockingUnitManager.units.ForEach(unit => unit.GetComponent<Transform>().position = this.transform.position);
    }

}
