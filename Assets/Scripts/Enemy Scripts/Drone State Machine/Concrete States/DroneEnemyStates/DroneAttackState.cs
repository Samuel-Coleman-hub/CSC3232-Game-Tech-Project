using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAttackState : DroneEnemyState
{
    private Vector3 distanceToPlayer;
    private float timer;
    private bool timerOn;

    public DroneAttackState(DroneStateManager stateMachine) : base("DroneAttackState", stateMachine){}

    public override void EnterState()
    {
        base.EnterState();
        ResetTimer();
        stateMachine.patrolLightObj.SetActive(false);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        FollowPlayer();
        timer -= Time.deltaTime;

        StartShooting();
    }
    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
    }

    private void FollowPlayer()
    {
        stateMachine.transform.LookAt(stateMachine.playerPosition.position);
        distanceToPlayer = stateMachine.transform.position - stateMachine.playerPosition.position;

        if(distanceToPlayer.magnitude <= stateMachine.patrolMaxDistanceToPlayer)
        {
            stateMachine.agent.isStopped = true;
        }
        else if(distanceToPlayer.magnitude > stateMachine.patrolMinDistanceToPlayer)
        {
            stateMachine.agent.isStopped = false;
            stateMachine.SwitchState(stateMachine.patrolState);
        }
        else
        {
            stateMachine.agent.isStopped = false;
            stateMachine.agent.SetDestination(stateMachine.playerPosition.position);
        }
    }

    private void StartShooting()
    {
        if(timer <= 0)
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
