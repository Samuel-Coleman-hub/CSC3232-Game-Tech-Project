using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePatrolState : DroneBaseState
{
    private Vector3 flyPoint;
    private bool flyPointSet = false;
    private RaycastHit hit;
    public override void EnterState(DroneStateManager droneState)
    {
        
    }

    public override void UpdateState(DroneStateManager droneState)
    {
        Patrolling(droneState);
    }

    public override void FixedUpdateState(DroneStateManager droneState)
    {
        Physics.BoxCast(droneState.droneCollider.bounds.center, droneState.boxCastScale,
            -droneState.transform.up, out hit, droneState.transform.rotation, 10f);
        if (hit.transform.gameObject.tag == "Player")
        {
            droneState.SwitchState(droneState.droneAttackState);
        }
    }

    public override void OnCollisionEnter(Collision collision)
    {
        
    }

    private void Patrolling(DroneStateManager context)
    {
        if (!flyPointSet)
        {
            FindFlyPoint(context);
        }
        else
        {
            context.agent.SetDestination(flyPoint);
        }

        Vector3 distanceToFlyPoint = context.transform.position - flyPoint;

        if (distanceToFlyPoint.magnitude < 1f)
        {
            flyPointSet = false;
        }
    }

    private void FindFlyPoint(DroneStateManager context)
    {
        float randomZ = Random.Range(-context.flyPointRange, context.flyPointRange);
        float randomX = Random.Range(-context.flyPointRange, context.flyPointRange);

        flyPoint = new Vector3(context.transform.position.x + randomX, context.transform.position.y, context.transform.position.z + randomZ);

        if (UnityEngine.AI.NavMesh.SamplePosition(flyPoint, out UnityEngine.AI.NavMeshHit hit, 1f, UnityEngine.AI.NavMesh.AllAreas))
        {
            flyPointSet = true;
        }
    }

    
}
