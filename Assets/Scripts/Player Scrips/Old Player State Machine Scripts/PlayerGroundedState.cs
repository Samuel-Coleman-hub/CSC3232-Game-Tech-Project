using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
        : base (currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        Debug.Log("Hellow from grounded state");
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        
    }

    public override void InitializeSubstate()
    {
        throw new System.NotImplementedException();
    }

    public override void CheckSwitchStates()
    {
        if (ctx.IsCrouchPressed)
        {
            SwitchStates(factory.Crouch());
        }
    }
}
