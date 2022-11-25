using UnityEngine;

public abstract class  DroneBaseState 
{
    public string name;
    protected DroneStateManager stateMachine;
    public DroneBaseState(string name, DroneStateManager stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }

    public virtual void EnterState() { }
    public virtual void UpdateState() { }

    public virtual void FixedUpdateState() { }

    public virtual void OnCollisionEnter(Collision collision) { }

    public virtual void OnCollisionChildEnter(ChildHitBox.DroneHitBoxType hitBoxType, GameObject objHit) { }

    public virtual void HitByDroneRemote(){}

    protected void SwitchState()
    {
        
    }
}
