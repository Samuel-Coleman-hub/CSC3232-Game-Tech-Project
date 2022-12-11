using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        isRootState = true;
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
        if (ctx.OnGround)
        {
            SwitchStates(factory.Grounded());
        }
    }

    private void Jump()
    {
        ctx.Rb.velocity = new Vector3(ctx.Rb.velocity.x, 0f, ctx.Rb.velocity.z);
        ctx.Rb.AddForce(ctx.transform.up * ctx.JumpForce, ForceMode.Impulse);
    }
}
