using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node        //inherit from node class
{
    protected List<Node> nodes = new List<Node>();
    private PlayerMovement ai;

    public Selector(List<Node> nodes, PlayerMovement ai)           //Upon construction, add the nodes from all child nodes to the list
    {
        this.nodes = nodes;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        ai.running = true;
        ai.startTime = Time.time;

        foreach (var node in nodes)     //iterate over each child node
        {
            switch (node.Evaluate())    //evaluate each child node
            {
                case NodeState.RUNNING: //if they are running
                    _nodeState = NodeState.RUNNING;
                    return _nodeState;  //set state to running and return state
                case NodeState.SUCCESS:
                    _nodeState = NodeState.SUCCESS;
                    ai.running = false;
                    return _nodeState;      //if they are a success, return state and don't evaluate any more child nodes
                case NodeState.FAILURE:
                    break;      //if they fail, do nothing
                default:
                    break;
            }
        }
        _nodeState = NodeState.FAILURE; //this code will only get called if all children are a failure
        return _nodeState;
    }
}
