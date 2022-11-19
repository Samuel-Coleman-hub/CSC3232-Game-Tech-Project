using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneDeadState : DroneBaseState
{
    public override void EnterState(DroneStateManager droneState)
    {
        droneState.enabled = false;
    }

    public override void UpdateState(DroneStateManager droneState)
    {
        
    }

    public override void FixedUpdateState(DroneStateManager droneState)
    {

    }

    public override void OnCollisionEnter(Collision collision)
    {
        
    }
    

}
