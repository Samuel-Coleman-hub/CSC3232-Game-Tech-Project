using UnityEngine;

public abstract class PlayerMovementBaseState
{
    public abstract void EnterState(PlayerMovementStateManager playerMovement);

    public abstract void UpdateState(PlayerMovementStateManager playerMovement);

    public abstract void FixedUpdateState(PlayerMovementStateManager playerMovement);
}
