using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        InitializeSubState();
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("HELL FOEM GROUDED");
    }
    public override void UpdateState()
    {
        base.UpdateState();
        CheckSwitchState();
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void InitializeSubState()
    {
        base.InitializeSubState();
        if (!_ctx.IsMovementPressed)
        {
            SetSubState(_factory.Idle());
        }
        else
        {
            SetSubState(_factory.Walk());
        }
    }
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if (_ctx.IsJumpPressed)
        {
            SwitchStates(_factory.Jump());
        }
    }
}
