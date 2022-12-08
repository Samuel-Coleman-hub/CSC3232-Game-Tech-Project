using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGoal
{
    int GetCalculatePriority();
    bool Runable();
    void UpdateGoal();
    void GoalActivate(BaseAction _linkedAction);
    void GoalDeactivate();
}

public class BaseGoal : MonoBehaviour, IGoal
{
    protected AgentMovement agent;
    protected EnemyHealth health;
    protected SightSensor sensor;
    protected GameManager gameManager;

    protected BaseAction linkedAction;

    // Start is called before the first frame update
    private void Awake()
    {
        agent = GetComponent<AgentMovement>();
        health = GetComponent<EnemyHealth>();
        sensor = GetComponent<SightSensor>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>(); 
    }

    private void Update()
    {
        UpdateGoal();

        //Uncomment to show planner decision making in console
        //Debug.Log(this + " " + GetType().Name + "" + " " + GetCalculatePriority());
    }

    public virtual int GetCalculatePriority()
    {
        return -1;
    }

    public virtual bool Runable()
    {
        return false;
    }

    public virtual void UpdateGoal()
    {

    }

    public virtual void GoalActivate(BaseAction _linkedAction)
    {
        linkedAction = _linkedAction;
    }

    public virtual void GoalDeactivate()
    {
        linkedAction = null;
    }
}
