using UnityEngine;

public abstract class PlayerMoveBaseState
{
    public abstract void EnterState(PlayerMoveStateMachine context);

    public abstract void UpdateState(PlayerMoveStateMachine context);

    public abstract void FixedUpdateState(PlayerMoveStateMachine context);

    public abstract void ExitState(PlayerMoveStateMachine context);

    public abstract void CheckSwitchStates(PlayerMoveStateMachine context);

    public abstract void InitializeSubState();

    void UpdateStates() { }
    void SetSuperState() { }
    void SetSubState() { }



}
