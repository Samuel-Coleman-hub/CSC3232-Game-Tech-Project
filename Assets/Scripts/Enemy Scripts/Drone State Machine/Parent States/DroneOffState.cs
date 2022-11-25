using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneOffState : DroneBaseState
{
    private DroneStateManager _sm;
    public DroneOffState(string name, DroneStateManager stateMachine) : base(name, stateMachine)
    {
        _sm = (DroneStateManager)stateMachine;
    }

    public override void EnterState()
    {
        base.EnterState();
        stateMachine.gameObject.layer = LayerMask.NameToLayer("GroundDrone");
        stateMachine.agent.enabled = false;
        stateMachine.rb.isKinematic = false;
        stateMachine.rb.AddForce(Vector3.down * 10f);

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
