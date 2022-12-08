using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToHidingSpotAction : BaseAction
{
    List<System.Type> supportedGoals = new List<System.Type>(new System.Type[] { typeof(GoHideGoal) , typeof(RetreatGoal) });
    public override List<System.Type> SupportedGoals()
    {
        return supportedGoals;
    }

    public override float Cost()
    {
        return 3f;
    }
    public override void OnActivate(BaseGoal _linkedGoal)
    {
        base.OnActivate(_linkedGoal);
        float closestDistance = 100;
        GameObject closestSpot = null;
        foreach (GameObject spot in agent.hidingSpots)
        {
            float distance = Vector3.Distance(agent.transform.position, spot.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestSpot = spot;
            }
        }

        if(closestSpot != null)
        {
            agent.MoveAgent(closestSpot.transform);
        }
        agent.AgentRun();
    }

    public override void OnDeactivate()
    {
        base.OnDeactivate();
        shooting.ShootingDeactivate();
    }

    public override void UpdateAction()
    {
        if (agent.destinationReached)
        {
            agent.isHiding = true;
            transform.LookAt(agent.shipLocation);
            shooting.StartShooting();
        }
    }
}
