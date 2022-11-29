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
    protected SightSensor sensor;

    protected BaseAction linkedAction;

    // Start is called before the first frame update
    private void Awake()
    {
        agent = GetComponent<AgentMovement>();
        sensor = GetComponent<SightSensor>();
    }

    private void Update()
    {
        UpdateGoal();
        Debug.Log(this + " " + GetType().Name + "" + " " + GetCalculatePriority());
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
