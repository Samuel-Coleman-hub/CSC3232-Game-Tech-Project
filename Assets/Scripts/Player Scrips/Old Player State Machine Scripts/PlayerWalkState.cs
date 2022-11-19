using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }
    public override void EnterState()
    {
        
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        HandleMovement();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void InitializeSubstate()
    {
        throw new System.NotImplementedException();
    }

    public override void CheckSwitchStates()
    {
        
    }

    private void HandleMovement()
    {
        
        MoveCharacter();
    }
    private void MoveCharacter()
    {
        ctx.MoveDirection = ctx.Orientation.forward * ctx.VerticalInput + ctx.Orientation.right * ctx.HorizontalInput;
        ctx.PlayerRigidbody.AddForce(10f * ctx.MovementSpeed * ctx.MoveDirection.normalized, ForceMode.Force);

        //if (ctx.isOnSlope && !ctx.ExitSlope)
        //{
        //    ctx.Sliding = true;
        //    ctx.SlidingMovement.StartSliding();

        //    ctx.PlayerRigidbody.AddForce(20f * ctx.MovementSpeed * ctx.GetSlopeMoveDirection(ctx.MoveDirection), ForceMode.Force);

        //    if (ctx.PlayerRigidbody.velocity.y > 0)
        //    {
        //        ctx.PlayerRigidbody.AddForce(Vector3.down * 80f, ForceMode.Force);
        //    }
        //}
        //else if (onGround)
        //{
        //    rb.AddForce(10f * movementSpeed * moveDirection.normalized, ForceMode.Force);
        //}
        //else if (!onGround)
        //{
        //    rb.AddForce(10f * airMultiplier * movementSpeed * moveDirection.normalized, ForceMode.Force);
        //}

        //rb.useGravity = !OnSlope();
    }
}
