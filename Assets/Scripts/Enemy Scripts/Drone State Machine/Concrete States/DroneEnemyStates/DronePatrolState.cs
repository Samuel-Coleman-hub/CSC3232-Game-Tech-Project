using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePatrolState : DroneEnemyState
{
    private Vector3 flyPoint;
    private bool flyPointSet = false;
    private RaycastHit hit;

    public DronePatrolState(DroneStateManager stateMachine) : base("DronePatrolState", stateMachine) { }

    public override void EnterState()
    {
        base.EnterState();
        stateMachine.patrolLightObj.SetActive(true);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Patrolling();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        if(Physics.BoxCast(stateMachine.droneCollider.bounds.center, stateMachine.boxCastScale,
            -stateMachine.transform.up, out hit, stateMachine.transform.rotation, 10f))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                stateMachine.SwitchState(stateMachine.attackState);
            }
        }
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }

    private void Patrolling()
    {
        if (!flyPointSet)
        {
            FindFlyPoint();
        }
        else
        {
            stateMachine.agent.SetDestination(flyPoint);
        }

        Vector3 distanceToFlyPoint = stateMachine.transform.position - flyPoint;

        if (distanceToFlyPoint.magnitude < 1f)
        {
            flyPointSet = false;
        }
    }

    private void FindFlyPoint()
    {
        float randomZ = Random.Range(-stateMachine.flyPointRange, stateMachine.flyPointRange);
        float randomX = Random.Range(-stateMachine.flyPointRange, stateMachine.flyPointRange);

        flyPoint = new Vector3(stateMachine.transform.position.x + randomX, stateMachine.transform.position.y, stateMachine.transform.position.z + randomZ);

        if (UnityEngine.AI.NavMesh.SamplePosition(flyPoint, out UnityEngine.AI.NavMeshHit hit, 1f, UnityEngine.AI.NavMesh.AllAreas))
        {
            flyPointSet = true;
        }
    }

    
}
