using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : PlayerBaseState
{
    public PlayerCrouchState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        HandleCrouch();
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
        ResizePlayer(ctx.StartYScale);
    }

    public override void InitializeSubstate()
    {
        throw new System.NotImplementedException();
    }

    public override void CheckSwitchStates()
    {
        if (!ctx.IsCrouchPressed)
        {
            SwitchStates(factory.Grounded());
        }
    }

    void HandleCrouch()
    {
        ResizePlayer(ctx.CrouchYScale);
        ctx.PlayerRigidbody.AddForce(Vector3.down * 5f, ForceMode.Impulse);
    }

    void ResizePlayer(float yScale)
    {
        ctx.PlayerTransform.localScale = new Vector3(ctx.PlayerTransform.localScale.x, yScale, ctx.PlayerTransform.localScale.z);
    }
}
