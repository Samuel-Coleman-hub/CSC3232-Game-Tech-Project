using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackShipAction : BaseAction
{
    AttackShipGoal attackShipGoal;

    List<System.Type> supportedGoals = new List<System.Type>(new System.Type[] { typeof(AttackShipGoal) });

    public override List<System.Type> SupportedGoals()
    {
        return supportedGoals;
    }

    public override void OnActivate(BaseGoal _linkedGoal)
    {
        base.OnActivate(_linkedGoal);
        attackShipGoal = (AttackShipGoal)linkedGoal;
        agent.MoveAgent(agent.shipLocation);
        shooting.ShootingActive();
    }

    public override void OnDeactivate()
    {
        shooting.ShootingDeactivate();
    }

    public override void UpdateAction()
    {
        transform.LookAt(agent.shipLocation);
    }
}
