using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneOffExplodeState : DroneOffState
{
    public DroneOffExplodeState(DroneStateManager stateMachine) : base("DroneOffExplodeState", stateMachine){}
    public override void EnterState()
    {
        base.EnterState();
        Explosion();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }

    public override void OnCollisionChildEnter(ChildHitBox.DroneHitBoxType hitBoxType, GameObject objHit)
    {
        base.OnCollisionChildEnter(hitBoxType, objHit);
    }

    private void Explosion()
    {
        Vector3 explosionPosition = stateMachine.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, stateMachine.explosionRadius);
        foreach (Collider hit in colliders)
        {
            if (hit.GetComponent<Rigidbody>())
            {
                hit.GetComponent<Rigidbody>().AddExplosionForce(stateMachine.explosionForce, explosionPosition,
                    stateMachine.explosionRadius, stateMachine.explosionLift);
            }
        }
        GameObject.Destroy(stateMachine.gameObject);
    }
}
