using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SubGoal
{
    public Dictionary<string, int> sgoals;
    public bool remove;

    public SubGoal(string s, int i, bool r)
    {
        sgoals = new Dictionary<string, int>();
        sgoals.Add(s, i);
        remove = r;
    }
}

public class GameAgent : MonoBehaviour
{
    public List<GameAction> actions = new List<GameAction>();
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();

    GamePlanner planner;
    Queue<GameAction> actionQueue;
    public GameAction currentAction;
    SubGoal currentGoal;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        GameAction[] acts = GetComponents<GameAction>();
        foreach (GameAction act in acts)
        {
            actions.Add(act);
        }
    }

    bool invoked = false;

    private void CompleteAction()
    {
        currentAction.performing = false;
        currentAction.PostPerform();
        invoked = false;
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        if(currentAction != null && currentAction.performing)
        {
            float distanceToTarget = Vector3.Distance(currentAction.target.transform.position, this.transform.position);
            if(currentAction.agent.hasPath && distanceToTarget < 2f)
            {
                if(!invoked)
                {
                    Invoke("CompleteAction", currentAction.duration);
                    invoked = true;
                }
            }
            return;
        }

        if(planner == null || actionQueue == null)
        {
            planner = new GamePlanner();

            var sortedGoals = from entry in goals orderby entry.Value descending select entry;
            foreach (KeyValuePair<SubGoal, int> sortedGoal in sortedGoals)
            {
                actionQueue = planner.plan(actions, sortedGoal.Key.sgoals, null);
                if(actionQueue != null)
                {
                    currentGoal = sortedGoal.Key;
                    break;
                }
            }
        }

        if(actionQueue != null && actionQueue.Count == 0)
        {
            if (currentGoal.remove)
            {
                goals.Remove(currentGoal);
            }
            planner = null;
        }

        if (actionQueue != null && actionQueue.Count > 0)
        {
            currentAction = actionQueue.Dequeue();
            if (currentAction.PrePerform())
            {
                if(currentAction.target == null && currentAction.targetTag != "")
                {
                    currentAction.target = GameObject.FindWithTag(currentAction.targetTag);
                }

                if(currentAction.target != null)
                {
                    currentAction.performing = true;
                    currentAction.agent.SetDestination(currentAction.target.transform.position);
                    Debug.Log("Going to destination");
                }
            }
            else
            {
                actionQueue = null;
            }
        }
    }
}
