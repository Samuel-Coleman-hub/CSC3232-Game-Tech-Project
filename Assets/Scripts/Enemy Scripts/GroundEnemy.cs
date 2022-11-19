using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundEnemy : MonoBehaviour
{
    [SerializeField] Transform destination;
    private NavMeshAgent agent;

    private void Start()
    {
       agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.destination = destination.position;
    }
}
