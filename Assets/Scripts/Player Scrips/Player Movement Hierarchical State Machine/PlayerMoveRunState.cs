using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveRunState : PlayerMoveBaseState
{
    private Vector3 moveDirection;
    public override void CheckSwitchStates(PlayerMoveStateMachine context)
    {

    }

    public override void EnterState(PlayerMoveStateMachine context)
    {
        context.rb.useGravity = true;
        context.desiredMoveSpeed = context.runSpeed;
        context.movementSpeed = context.desiredMoveSpeed;
    }

    public override void ExitState(PlayerMoveStateMachine context)
    {

    }

    public override void InitializeSubState()
    {

    }

    public override void UpdateState(PlayerMoveStateMachine context)
    {
        SpeedControl(context);
    }

    public override void FixedUpdateState(PlayerMoveStateMachine context)
    {
        MoveCharacter(context);
    }

    private void MoveCharacter(PlayerMoveStateMachine  context)
    {
        moveDirection = context.orientation.forward * context.verticalInput + context.orientation.right * context.horizontalInput;
        context.rb.AddForce(10f * context.movementSpeed * moveDirection.normalized, ForceMode.Force);
    }

    private void SpeedControl(PlayerMoveStateMachine context)
    {
        Vector3 flatVel = new Vector3(context.rb.velocity.x, 0f, context.rb.velocity.z);

        if (flatVel.magnitude > context.movementSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * context.movementSpeed;
            context.rb.velocity = new Vector3(limitedVel.x, context.rb.velocity.y, limitedVel.z);
        }

    }
}
