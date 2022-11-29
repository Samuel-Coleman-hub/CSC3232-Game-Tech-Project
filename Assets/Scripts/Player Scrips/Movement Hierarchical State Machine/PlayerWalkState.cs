using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

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
    }
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if (!_ctx.IsMovementPressed)
        {
            SwitchStates(_factory.Idle());
        }
    }
}
