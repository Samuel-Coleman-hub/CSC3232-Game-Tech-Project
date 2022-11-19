using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementRunState : PlayerMovementBaseState
{
    public override void EnterState(PlayerMovementStateManager playerMovement)
    {

    }

    public override void UpdateState(PlayerMovementStateManager playerMovement)
    {

    }

    public override void FixedUpdateState(PlayerMovementStateManager playerMovement)
    {
        MoveCharacter(playerMovement);
    }

    private void MoveCharacter(PlayerMovementStateManager context)
    {
        context.moveDirection = context.orientation.forward * context.verticalInput + context.orientation.right * context.horizontalInput;

        if (context.OnSlope() && !context.exitSlope)
        {
            context.sliding = true;
            context.slidingMovement.StartSliding();

            context.rb.AddForce(20f * context.movementSpeed * context.GetSlopeMoveDirection(context.moveDirection), ForceMode.Force);

            if (context.rb.velocity.y > 0)
            {
                context.rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }
        else if (context.onGround)
        {
            Debug.Log("movement speed" + context.movementSpeed);
            context.rb.AddForce(10f * context.movementSpeed * context.moveDirection.normalized, ForceMode.Force);
            
        }
        else if (!context.onGround)
        {
            context.rb.AddForce(10f * context.airMultiplier * context.movementSpeed * context.moveDirection.normalized, ForceMode.Force);
        }

        context.rb.useGravity = !context.OnSlope();
    }

}
