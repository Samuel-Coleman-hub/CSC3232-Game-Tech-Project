using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatGoal : BaseGoal
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

        //Change to wave ended
        if (true)
        {
            priority -= priorityDiminisher * Time.deltaTime;
        }
        else
        {
            priority += priorityMultiplier * Time.deltaTime;
        }
    }

    public override int GetCalculatePriority()
    {
        return Mathf.FloorToInt(priority);
    }


    public override bool Runable()
    {
        //If wave ending or health low
        return true;
    }
}
