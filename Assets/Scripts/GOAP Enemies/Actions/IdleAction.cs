using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : BaseAction
{
    List<System.Type> supportedGoals = new List<System.Type>(new System.Type[] { typeof(IdleGoal) });

    public override List<System.Type> SupportedGoals()
    {
        return supportedGoals;
    }

    public override float Cost()
    {
        return 0f;
    }
}
