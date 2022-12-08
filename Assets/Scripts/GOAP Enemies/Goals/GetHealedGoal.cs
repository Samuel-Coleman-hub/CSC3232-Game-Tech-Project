using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHealedGoal : BaseGoal
{
    [SerializeField] int maxPriority = 45;
    float priority = 45f;

    public override void GoalActivate(BaseAction _linkedAction)
    {
        base.GoalActivate(_linkedAction);
        priority = maxPriority;
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
