using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatAction : BaseAction
{
    List<System.Type> supportedGoals = new List<System.Type>(new System.Type[] { typeof(RetreatGoal), typeof(GoHideGoal)});
    public override List<System.Type> SupportedGoals()
    {
        return supportedGoals;
    }

    public override float Cost()
    {
        if (agent.isHiding)
        {
            return 2f;
        }
        return 10f;
    }
    public override void OnActivate(BaseGoal _linkedGoal)
    {
        base.OnActivate(_linkedGoal);
        agent.backUpUI.SetActive(true);
        agent.MoveAgent(agent.enemyBaseLocation);
        agent.AgentRun();
    }

    public override void OnDeactivate()
    {
        agent.backUpUI.SetActive(false);
        agent.AgentWalk();
        agent.isHiding = false;
    }

    public override void UpdateAction()
    {
        if (agent.destinationReached)
        {
            spawner.EnemyCallingForBackup(2);
            OnDeactivate();
        }
    }


}
