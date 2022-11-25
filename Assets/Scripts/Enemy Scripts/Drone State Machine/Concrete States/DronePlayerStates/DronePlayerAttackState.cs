using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePlayerAttackState : DronePlayerState
{
    private Vector3 distanceToEnemy;
    private float timer;
    public DronePlayerAttackState(DroneStateManager stateMachine) : base("DronePlayerAttackState", stateMachine){}

    public override void EnterState()
    {
        base.EnterState();
        stateMachine.patrolLightObj.SetActive(false);
        ResetTimer();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        FollowEnemy();
        timer -= Time.deltaTime;

        StartShooting();
    }

    private void FollowEnemy()
    {
        stateMachine.transform.LookAt(stateMachine.lastSeenEnemy.position);
        distanceToEnemy = stateMachine.transform.position - stateMachine.lastSeenEnemy.position;

        if (distanceToEnemy.magnitude <= stateMachine.patrolMaxDistanceToPlayer)
        {
            stateMachine.agent.isStopped = true;
        }
        else if (distanceToEnemy.magnitude > stateMachine.patrolMinDistanceToPlayer)
        {
            stateMachine.agent.isStopped = false;
            stateMachine.SwitchState(stateMachine.playerTargetState);
        }
        else
        {
            stateMachine.agent.isStopped = false;
            stateMachine.agent.SetDestination(stateMachine.lastSeenEnemy.position);
        }
    }

    private void StartShooting()
    {
        if (timer <= 0)
        {
            GameObject bulletGameObject = GameObject.Instantiate(stateMachine.bulletPrefab, stateMachine.gunTip.position,
                stateMachine.transform.rotation, stateMachine.bulletContainer.transform);

            Rigidbody bulletRb = bulletGameObject.GetComponent<Rigidbody>();

            bulletRb.AddForce(stateMachine.gunTip.forward * stateMachine.shotForce + stateMachine.rb.velocity, ForceMode.Impulse);
            ResetTimer();
        }
    }

    private void ResetTimer()
    {
        timer = stateMachine.gunCooldown;
    }
}
