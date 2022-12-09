using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PSFlocking;

public class EyeEnemy : Shooter
{
    [SerializeField] float stoppingDistance;
    private PSUnitManager flockingUnitManager;
    private Transform goal;
    private Transform lookAtTargetGameObject;


    private void Awake()
    {
        flockingUnitManager = GameObject.FindGameObjectWithTag("FlockingManager").GetComponent<PSUnitManager>();
        lookAtTargetGameObject = GameObject.FindGameObjectWithTag("PlayerBase").transform;
        goal = flockingUnitManager.goal.transform;
    }

    public override void Update()
    {
        base.Update();
        base.StartShooting();

        if(Vector3.Distance(this.transform.position, goal.position) < stoppingDistance)
        {
            transform.LookAt(lookAtTargetGameObject);
        }

    }

    private void OnDestroy()
    {
        flockingUnitManager.RemoveFlockingUnit(this.gameObject);
    }


}
