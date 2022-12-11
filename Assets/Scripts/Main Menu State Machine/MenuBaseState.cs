using UnityEngine;

public abstract class MenuBaseState
{
    public virtual string StateName { get; set; }
    public abstract void EnterState(MenuStateManager menu);

    public abstract void UpdateState(MenuStateManager menu);
    public abstract void ExitState(MenuStateManager menu);
}
