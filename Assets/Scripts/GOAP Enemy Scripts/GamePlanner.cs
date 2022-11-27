using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Node
{
    public Node parent;
    public float cost;
    public Dictionary<string, int> state;
    public GameAction action;

    public Node(Node parent, float cost, Dictionary<string, int> allStates, GameAction action)
    {
        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(allStates);
        this.action = action;
    }
}

public class GamePlanner
{
    public Queue<GameAction> plan(List<GameAction> actions, Dictionary<string, int> goal, WorldStates states)
    {
        List<GameAction> usableActions = new List<GameAction>();
        foreach (GameAction action in actions)
        {
            if (action.IsDoable())
            {
                usableActions.Add(action);
            }
        }

        List<Node> leaves = new List<Node>();
        Node start = new Node(null, 0, GameWorld.Instance.GetWorld().GetStates(), null);

        bool success = BuildGraph(start, leaves, usableActions, goal);

        if (!success)
        {
            Debug.Log("No Plan");
            return null;
        }

        Node cheapest = null;
        foreach(Node leaf in leaves)
        {
            if(cheapest == null)
            {
                cheapest = leaf;
            }
            else
            {
                if(leaf.cost < cheapest.cost)
                {
                    cheapest = leaf;
                }
            }
        }

        List<GameAction> result = new List<GameAction>();
        Node n = cheapest;
        while(n != null)
        {
            if(n.action != null)
            {
                result.Insert(0, n.action);
            }
            n = n.parent;
        }

        Queue<GameAction> queue = new();
        foreach(GameAction action in result)
        {
            queue.Enqueue(action);
        }

        Debug.Log("The plan is: ");
        foreach(GameAction action in queue)
        {
            Debug.Log("Queue: " + action.actionName);
        }

        return queue;

    }

    private bool BuildGraph(Node parent, List<Node> leaves, List<GameAction> usableActions, Dictionary<string, int> goal)
    {
        bool foundPath = false;

        foreach(GameAction action in usableActions)
        {
            if (action.IsDoableGiven(parent.state))
            {
                Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);
                foreach(KeyValuePair<string, int> pair in action.effects)
                {
                    if (!currentState.ContainsKey(pair.Key))
                    {
                        currentState.Add(pair.Key, pair.Value);
                    }
                }

                Node node = new Node(parent, parent.cost + action.cost, currentState, action);

                if(GoalAchieved(goal, currentState))
                {
                    leaves.Add(node);
                    foundPath = true;
                }
                else
                {
                    List<GameAction> subset = ActionSubset(usableActions, action);
                    bool found = BuildGraph(node, leaves, subset, goal);
                    if (found)
                    {
                        foundPath = true;
                    }
                }
            }
        }
        return foundPath;
    }

    private bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state)
    {
        foreach(KeyValuePair<string, int> pair in goal)
        {
            if (!state.ContainsKey(pair.Key))
            {
                return false;
            }
        }
        return true;
    }

    private List<GameAction> ActionSubset(List<GameAction> actions, GameAction removeMe)
    {
        List<GameAction> subset = new List<GameAction>();
        foreach(GameAction action in actions)
        {
            if (!action.Equals(removeMe))
            {
                subset.Add(action);
            }
        }
        return subset;
    }
}
