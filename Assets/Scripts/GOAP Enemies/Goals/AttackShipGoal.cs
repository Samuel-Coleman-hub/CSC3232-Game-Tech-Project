using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackShipGoal : BaseGoal
{
    [SerializeField] int priority;
    public override void GoalActivate(BaseAction _linkedAction)
    {
        base.GoalActivate(_linkedAction);
    }

    public override int GetCalculatePriority()
    {
        return priority;
    }

    public override bool Runable()
    {
        return true;
    }
}
