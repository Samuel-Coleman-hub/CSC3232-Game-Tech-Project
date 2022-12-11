using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneEnemyIdleState : DroneEnemyState
{
    private DroneStateManager _sm;
    public DroneEnemyIdleState(DroneStateManager stateMachine) : base("DroneEnemyIdleState", stateMachine){}

    public override void EnterState()
    {
        base.EnterState();
        stateMachine.patrolLightObj.SetActive(false);
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
    }

    public override void OnCollisionChildEnter(ChildHitBox.DroneHitBoxType hitBoxType, GameObject objHit)
    {
        base.OnCollisionChildEnter(hitBoxType, objHit);
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
}
