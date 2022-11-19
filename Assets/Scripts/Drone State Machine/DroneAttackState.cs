using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAttackState : DroneBaseState
{
    private Vector3 distanceToPlayer;
    private float timer;
    private bool timerOn;
    public override void EnterState(DroneStateManager droneState)
    {
        Debug.Log("Switched to attack");
        ResetTimer(droneState);
    }

    public override void UpdateState(DroneStateManager droneState)
    {
        FollowPlayer(droneState);
        timer -= Time.deltaTime;

        StartShooting(droneState);
    }
    public override void OnCollisionEnter(Collision collision)
    {

    }
    public override void FixedUpdateState(DroneStateManager droneState)
    {

    }

    private void FollowPlayer(DroneStateManager context)
    {
        context.agent.SetDestination(context.playerPosition.position);
        context.transform.LookAt(context.playerPosition.position);
        distanceToPlayer = context.transform.position - context.playerPosition.position;

        if(distanceToPlayer.magnitude > 15f)
        {
            context.SwitchState(context.dronePatrolState);
        }
    }

    private void StartShooting(DroneStateManager context)
    {
        if(timer <= 0)
        {
            GameObject bulletGameObject = GameObject.Instantiate(context.bulletPrefab, context.gunTip.position,
                context.transform.rotation, context.bulletContainer.transform);

            Rigidbody bulletRb = bulletGameObject.GetComponent<Rigidbody>();

            bulletRb.AddForce(context.gunTip.forward * context.shotForce + context.rb.velocity, ForceMode.Impulse);
            ResetTimer(context);
        }

    }

    private void ResetTimer(DroneStateManager context)
    {
        timer = context.gunCooldown;
    }

}
