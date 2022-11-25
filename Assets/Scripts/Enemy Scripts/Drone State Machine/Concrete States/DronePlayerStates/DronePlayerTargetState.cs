using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePlayerTargetState : DronePlayerState
{
    public DronePlayerTargetState(DroneStateManager stateMachine) : base("DronePlayerTargetState", stateMachine){}

    public override void EnterState()
    {
        base.EnterState();
        stateMachine.patrolLightObj.SetActive(true);
        Debug.Log("In target state");
    }

    public override void UpdateState()
    {
        base.UpdateState();
        stateMachine.agent.SetDestination(stateMachine.droneRemote.beamDes);

        if(Vector3.Distance(stateMachine.transform.position, stateMachine.droneRemote.beamDes) < 10f)
        {
            RaycastHit hit;
            if (Physics.BoxCast(stateMachine.droneCollider.bounds.center, stateMachine.boxCastScale,
            -stateMachine.transform.up, out hit, stateMachine.transform.rotation, 10f))
            {
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    stateMachine.lastSeenEnemy = hit.transform;
                    stateMachine.SwitchState(stateMachine.playerAttackState);
                }
            }
        }
    }
}
