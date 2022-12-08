using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToHealPointAction : BaseAction
{
    List<System.Type> supportedGoals = new List<System.Type>(new System.Type[] { typeof(GetHealedGoal) });
    public override List<System.Type> SupportedGoals()
    {
        return supportedGoals;
    }
     
    public override float Cost()
    {
        return 5f;
    }
    public override void OnActivate(BaseGoal _linkedGoal)
    {
        base.OnActivate(_linkedGoal);
        float closestDistance = 100;
        GameObject closestPoint = agent.healPoints[0];
        foreach (GameObject point in agent.healPoints)
        {
            float distance = Vector3.Distance(agent.transform.position, point.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPoint = point;
            }
        }

        agent.MoveAgent(closestPoint.transform);
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
            health.ResetHealth();
            OnDeactivate();
        }
    }
}
