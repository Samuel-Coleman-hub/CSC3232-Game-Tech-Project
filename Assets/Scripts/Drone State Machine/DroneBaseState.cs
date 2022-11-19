using UnityEngine;

public abstract class  DroneBaseState 
{
    public abstract void EnterState(DroneStateManager droneState);

    public abstract void UpdateState(DroneStateManager droneState);

    public abstract void FixedUpdateState(DroneStateManager droneState);

    public abstract void OnCollisionEnter(Collision collision);
}
