using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node        //inherit from node class
{
    protected List<Node> nodes = new List<Node>();

    public Sequence(List<Node> nodes)  //Upon construction, add the nodes from all child nodes to the list
    {
        this.nodes = nodes;
    }
    public override NodeState Evaluate()
    {
        bool anyChildRunning = false;  //variable to check if there are any child nodes currently running
        foreach (var node in nodes) //iterate over each child node
        {
            switch (node.Evaluate())    //evaluate each child node
            {
                case NodeState.RUNNING: //if they are running
                    anyChildRunning = true;
                    break;
                case NodeState.SUCCESS: //if they are a success, do nothing
                    break;
                case NodeState.FAILURE:
                    _nodeState = NodeState.FAILURE; //if any child is a failure, we assign the sequence class as a failure
                    return _nodeState;
                default:
                    break;
            }
        }
        _nodeState = anyChildRunning ? NodeState.RUNNING : NodeState.SUCCESS;   //if any nodes are still running, set state to running, otherwise we know they have all succeeded
        return _nodeState;
    }
}
