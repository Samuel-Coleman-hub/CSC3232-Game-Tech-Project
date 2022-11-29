using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayerAction : BaseAction
{
    AttackPlayerGoal attackPlayerGoal;

    List<System.Type> supportedGoals = new List<System.Type>(new System.Type[] { typeof(AttackPlayerGoal) });

    public override List<System.Type> SupportedGoals()
    {
        return supportedGoals;
    }

    public override void OnActivate(BaseGoal _linkedGoal)
    {
        base.OnActivate(_linkedGoal);
        attackPlayerGoal = (AttackPlayerGoal)linkedGoal;
        agent.MoveAgent(attackPlayerGoal.playerTransform);
        shooting.ShootingActive();
    }

    public override void OnDeactivate()
    {
        shooting.ShootingDeactivate();
    }

    public override void UpdateAction()
    {
        agent.MoveAgent(attackPlayerGoal.playerTransform);
        transform.LookAt(attackPlayerGoal.playerTransform);
    }
}
