using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    [SerializeField] public GameObject backUpUI;
    public float agentStoppingDistance;
    public bool moving => agent.velocity.magnitude > float.Epsilon;
    public Transform playerTransform;
    public Transform shipLocation;
    public Transform enemyBaseLocation;
    public GameObject[] healPoints;
    public GameObject[] hidingSpots;

    public float agentRunSpeed;
    public float agentWalkSpeed;

    public bool destinationReached;
    public bool isHiding;

    private NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        shipLocation = GameObject.FindGameObjectWithTag("PlayerBase").transform;
        enemyBaseLocation = GameObject.FindGameObjectWithTag("EnemySpawner").transform;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        agent.stoppingDistance = agentStoppingDistance;

        healPoints = GameObject.FindGameObjectsWithTag("HealPoint");
        hidingSpots = GameObject.FindGameObjectsWithTag("HidingSpot");
    }

    private void Update()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    Debug.Log("Position Reached");
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
