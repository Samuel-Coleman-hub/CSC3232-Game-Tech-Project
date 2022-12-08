using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleGoal : BaseGoal
{
    [SerializeField] int priority = 10;
    public override int GetCalculatePriority()
    {
        return priority;
    }

    public override bool Runable()
    {
        return true;
    }
}
