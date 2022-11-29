using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    public float agentStoppingDistance;
    public bool moving => agent.velocity.magnitude > float.Epsilon;
    public Transform shipLocation;
    public Transform enemyBaseLocation;

    public float agentRunSpeed;
    public float agentWalkSpeed;

    public bool destinationReached;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = agentStoppingDistance;
    }

    private void Update()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    Debug.Log("Stopping");
                    destinationReached = true;
                    agent.isStopped = true;
                }
                
            }
        }
        
    }
    public void MoveAgent(Transform newLocation)
    {
        agent.SetDestination(newLocation.position);
        agent.isStopped = false;
        destinationReached = false;
    }

    public void AgentRun()
    {
        agent.speed = agentRunSpeed;
    }

    public void AgentWalk()
    {
        agent.speed = agentWalkSpeed;
    }


}
