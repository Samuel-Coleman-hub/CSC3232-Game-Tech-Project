using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHideGoal : BaseGoal
{
    [SerializeField] int minPriority = 0;
    [SerializeField] int maxPriority = 30;

    [SerializeField] float priorityMultiplier = 1f;
    [SerializeField] float priorityDiminisher = 0.1f;
    float priority = 0f;

    public override void GoalActivate(BaseAction _linkedAction)
    {
        base.GoalActivate(_linkedAction);
        priority = maxPriority;
    }

    public override void UpdateGoal()
    {
        if (!agent.isHiding && priority <= maxPriority)
        {
            priority += priorityMultiplier * Time.deltaTime;
        }
        else if (priority >= minPriority)
        {
            priority -= priorityDiminisher * Time.deltaTime;
        }
    }

    public override int GetCalculatePriority()
    {
        return Mathf.FloorToInt(priority);
    }


    public override bool Runable()
    {
        if(health.GetHealth() <= 50)
        {
            return true;
        }
        return false;
    }
}
