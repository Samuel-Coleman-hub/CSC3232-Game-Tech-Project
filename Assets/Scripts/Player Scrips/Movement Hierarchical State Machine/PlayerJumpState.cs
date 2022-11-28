using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        InitializeSubState();
    }

    public override void EnterState()
    {
        base.EnterState();
        Jump();
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
        if (_ctx.OnGround)
        {
            SwitchStates(_factory.Grounded());
        }
    }

    private void Jump()
    {
        _ctx.Rb.velocity = new Vector3(_ctx.Rb.velocity.x, 0f, _ctx.Rb.velocity.z);
        _ctx.Rb.AddForce(_ctx.transform.up * _ctx.JumpForce, ForceMode.Impulse);
    }
}
