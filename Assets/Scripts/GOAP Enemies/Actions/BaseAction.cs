using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAction : MonoBehaviour
{
    protected AgentMovement agent;
    protected EnemyHealth health;
    protected SightSensor sensor;
    protected AgentShooting shooting;
    protected GameManager gameManager;
    protected EnemySpawner spawner;
    protected BaseGoal linkedGoal;

    private void Awake()
    {
        agent = GetComponent<AgentMovement>();
        health = GetComponent<EnemyHealth>();
        sensor = GetComponent<SightSensor>();
        shooting = GetComponent<AgentShooting>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        spawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
    }

    public virtual List<System.Type> SupportedGoals()
    {
        return null;
    }
    public virtual float Cost()
    {
        return 0f;
    }

    public virtual void OnActivate(BaseGoal _linkedGoal)
    {
        linkedGoal = _linkedGoal;
    }

    public virtual void OnDeactivate()
    {
        linkedGoal = null;
    }

    public virtual void UpdateAction()
    {

    }
}
