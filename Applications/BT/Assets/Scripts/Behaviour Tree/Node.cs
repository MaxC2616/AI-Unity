using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public enum NodeState { RUNNING, SUCCESS, FAILURE }

public abstract class Node
{
    GameObject closestObject;   //variable for closest obstacle
    private float oldDistance = 9999; //placeholder variable for checking distances
    protected NodeState _nodeState;     //variable to describe the state of the node

    public NodeState nodeState { get { return _nodeState; } }   //getter for nodestate variable

    public abstract NodeState Evaluate();

    public GameObject closestObstacle(PlayerMovement ai, List<GameObject> NearGameobjects)      //Setting up of a closestObstacle function for all child nodes to inherit to reduce repition and increase efficiency
    {
        oldDistance = 9999; //reset distance upon each call

        foreach (GameObject g in NearGameobjects)   //go through each obstacle in the level
        {
            float dist = Vector3.Distance(ai.transform.position, g.transform.position); //get the distance between the AI object and the obstacle currently being searched

            if (dist < oldDistance) //if this distance is the smallest so far,
            {
                closestObject = g;  //set the closest object to the one currently being searched
                oldDistance = dist; //set the distance limit to the current distance, to make sure only objects with distances smaller than this one can enter this if statement
            }
        }

        return closestObject;   //return the closest obstacle
    }
}
