using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAPPlanner : MonoBehaviour
{
    BaseGoal[] goals;
    BaseAction[] actions;

    BaseGoal currentGoal;
    BaseAction currentAction;

    private void Awake()
    {
        goals = GetComponents<BaseGoal>();
        actions = GetComponents<BaseAction>();
    }

    private void Update()
    {
        BaseGoal bestGoal = null;
        BaseAction bestAction = null;

        foreach (var goal in goals)
        {
            goal.UpdateGoal();
        }
        

        foreach(var goal in goals)
        {
            goal.UpdateGoal();

            if (!goal.Runable())
            {
                continue;
            }

            if(!(bestGoal == null || goal.GetCalculatePriority()>bestGoal.GetCalculatePriority()))
            {
                continue;
            }

            BaseAction tempAction = null;
            foreach(var action in actions)
            {
                if (!action.SupportedGoals().Contains(goal.GetType()))
                {
                    continue;
                }

                if(tempAction == null || action.Cost() < tempAction.Cost())
                {
                    tempAction = action;
                }
            }
            if(tempAction != null)
            {
                bestGoal = goal;
                bestAction = tempAction; 
            }
        }

        //Enter if no current goal
        if (currentGoal == null) 
        {
            currentGoal = bestGoal;
            currentAction = bestAction;

            //Enable goals and actions if not null
            if (currentGoal != null)
            {
                currentGoal.GoalActivate(currentAction);
            }
            if (currentAction != null)
            {
                currentAction.OnActivate(currentGoal);
            }
        }
        
        //Enters if best goal is the current goal
        else if(currentGoal == bestGoal)
        {
            //Enters if best actions is not used
            if(currentAction != bestAction)
            {
                currentAction.OnDeactivate();
                currentAction = bestAction;
                currentAction.OnActivate(currentGoal);
            }
        }
        
        //Enters if a better goal has been found
        else if(currentGoal != bestGoal)
        {
            currentGoal.GoalDeactivate();
            currentAction.OnDeactivate();

            currentGoal = bestGoal;
            currentAction = bestAction;

            if (currentGoal != null)
            {
                currentGoal.GoalActivate(currentAction);
            }
            if (currentAction != null)
            {
                currentAction.OnActivate(currentGoal);
            }
        }

        //Updates logic in actions
        if(currentAction != null)
        {
            currentAction.UpdateAction();
        }

    }

}
