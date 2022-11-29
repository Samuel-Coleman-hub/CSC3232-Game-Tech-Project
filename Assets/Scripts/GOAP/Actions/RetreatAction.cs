using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatAction : BaseAction
{
    List<System.Type> supportedGoals = new List<System.Type>(new System.Type[] { typeof(RetreatGoal) });
    public override List<System.Type> SupportedGoals()
    {
        return supportedGoals;
    }

    public override float Cost()
    {
        return 0f;
    }
    public override void OnActivate(BaseGoal _linkedGoal)
    {
        base.OnActivate(_linkedGoal);
        agent.MoveAgent(agent.enemyBaseLocation);
        agent.AgentRun();
    }

    public override void OnDeactivate()
    {
        agent.AgentWalk();
    }

    public override void UpdateAction()
    {
        if (agent.destinationReached)
        {
            OnDeactivate();
        }
    }


}
