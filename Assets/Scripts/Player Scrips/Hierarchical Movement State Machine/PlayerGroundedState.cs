using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        isRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        base.EnterState();
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
        if (!ctx.IsMovementPressed)
        {
            SetSubState(factory.Idle());
        }
        else
        {
            SetSubState(factory.Walk());
        }
    }
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if (ctx.IsJumpPressed)
        {
            SwitchStates(factory.Jump());
        }
    }
}
