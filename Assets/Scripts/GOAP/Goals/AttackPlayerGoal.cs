using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayerGoal : BaseGoal
{
    [SerializeField] int minPriority = 0;
    [SerializeField] int maxPriority = 30;

    [SerializeField] float priorityMultiplier = 1f;
    [SerializeField] float priorityDiminisher = 0.1f;

    [SerializeField] float maxDistanceFromPlayer = 10f;
    float priority = 0f;
    public override void GoalActivate(BaseAction _linkedAction)
    {
        base.GoalActivate(_linkedAction);
        priority = maxPriority;
    }

    public override void GoalDeactivate()
    {
        
    }

    public override void UpdateGoal()
    {
        if(sensor.distanceToPlayer <= maxDistanceFromPlayer)
        {
            priority += priorityMultiplier * Time.deltaTime;
        }
        else
        {
            priority -= priorityDiminisher * Time.deltaTime;
        }
    }

    public override int GetCalculatePriority()
    {
        if (!sensor.InSight("Player"))
        {
            return 0;
        }
        else
        {
            return Mathf.FloorToInt(priority);
        }
    }

    public override bool Runable()
    {
        if (sensor.InSight("Player"))
        {
            return true;
        }
        return false;
    }
}
