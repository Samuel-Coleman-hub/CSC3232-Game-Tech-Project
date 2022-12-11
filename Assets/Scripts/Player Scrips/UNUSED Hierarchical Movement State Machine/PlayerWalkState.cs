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
        Debug.Log("In Walking State");
    }
    public override void UpdateState()
    {
        base.UpdateState();
        CheckSwitchState();
        
        MoveCharacter();
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
        if (!ctx.IsMovementPressed)
        {
            SwitchStates(factory.Idle());
        }
    }

    private void MoveCharacter()
    {
        ctx.MoveDirection = ctx.Orientation.forward * ctx.VerticalInput + ctx.Orientation.right * ctx.HorizontalInput;
        ctx.Rb.AddForce(10f * ctx.MovementSpeed * ctx.MoveDirection.normalized, ForceMode.Force);
        
    }
}
