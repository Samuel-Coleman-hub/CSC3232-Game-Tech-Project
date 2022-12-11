using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePlayerTurnOnState : DronePlayerState
{
    private Vector3 navMeshPosition;
    public DronePlayerTurnOnState(DroneStateManager stateMachine) : base("DronePlayerIdleState", stateMachine){}

    public override void EnterState()
    {
        base.EnterState();
        stateMachine.patrolLightObj.SetActive(true);
        stateMachine.eyeObj.SetActive(true);
        stateMachine.leftPropellorObj.SetActive(true);
        stateMachine.rightPropellorObj.SetActive(true);
        FindNavMesh();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        ReturnToNavMesh();
    }

    private void FindNavMesh()
    {
        RaycastHit hit;
        if(Physics.Raycast(stateMachine.transform.position, stateMachine.transform.up, out hit,
            Mathf.Infinity, stateMachine.whatIsNavMesh))
        {
            navMeshPosition = new Vector3(hit.point.x, hit.point.y + 5f, hit.point.z);
        }
    }

    private void ReturnToNavMesh()
    {
        if(Vector3.Distance(stateMachine.transform.position, navMeshPosition) <= 2f)
        {
            stateMachine.agent.enabled = true;
            stateMachine.agent.ResetPath();
            stateMachine.gameObject.layer = LayerMask.NameToLayer("FlightDrone");
            stateMachine.SwitchState(stateMachine.playerTargetState);
        }
        else
        {
            //stateMachine.transform.position =
            //Vector3.MoveTowards(stateMachine.transform.position, navMeshPosition, 0.5f * Time.deltaTime);
            //Debug.Log("moving");
            stateMachine.rb.AddForce(stateMachine.transform.up * 80f);
        }
    }
}
