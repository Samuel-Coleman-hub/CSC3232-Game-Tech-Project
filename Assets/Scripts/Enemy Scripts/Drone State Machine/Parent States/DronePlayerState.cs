using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePlayerState : DroneBaseState
{
    private DroneStateManager _sm;
    public DronePlayerState(string name, DroneStateManager stateMachine) : base(name, stateMachine)
    {
        _sm = (DroneStateManager)stateMachine;

    }

    public override void EnterState()
    {
        base.EnterState();
        stateMachine.agent.speed *= 2;
        stateMachine.patrolLight.color = stateMachine.lightFriendlyColour;
        stateMachine.patrolLightMeshRenderer.material = stateMachine.lightFriendlyMaterial;
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

    public virtual void NewDroneRemotePos()
    {

    }
}
