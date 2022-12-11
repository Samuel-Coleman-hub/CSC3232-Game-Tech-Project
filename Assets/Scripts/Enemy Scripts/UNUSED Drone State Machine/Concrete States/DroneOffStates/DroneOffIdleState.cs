using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneOffIdleState : DroneOffState
{
    private float timer;
    public DroneOffIdleState(DroneStateManager stateMachine) : base("DroneOffIdleState", stateMachine){}

    public override void EnterState()
    {
        base.EnterState();
        ResetTimer();
        stateMachine.agent.enabled = false;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            stateMachine.SwitchState(stateMachine.offExplodeState);
        }
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if(collision.gameObject.CompareTag("Bullet"))
        {
            stateMachine.SwitchState(stateMachine.offExplodeState);
        }
    }

    public override void HitByDroneRemote()
    {
        base.HitByDroneRemote();
        stateMachine.SwitchState(stateMachine.playerIdleState);
    }

    public override void OnCollisionChildEnter(ChildHitBox.DroneHitBoxType hitBoxType, GameObject objHit)
    {
        base.OnCollisionChildEnter(hitBoxType, objHit);   
    }
    private void ResetTimer()
    {
        timer = stateMachine.timeTillExplode;
    }
}
