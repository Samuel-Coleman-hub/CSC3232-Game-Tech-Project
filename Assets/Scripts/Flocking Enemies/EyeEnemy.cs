using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PSFlocking;

public class EyeEnemy : Shooter
{
    private PSUnitManager flockingUnitManager;

    private void Awake()
    {
        flockingUnitManager = GameObject.FindGameObjectWithTag("FlockingManager").GetComponent<PSUnitManager>();
    }

    public override void Update()
    {
        base.Update();
        base.StartShooting();
    }

    private void OnDestroy()
    {
        flockingUnitManager.RemoveFlockingUnit(this.gameObject);
    }


}
