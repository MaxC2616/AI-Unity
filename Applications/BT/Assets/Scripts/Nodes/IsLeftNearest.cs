using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsLeftNearest : Node       //inherit from node
{
    private PlayerMovement ai;  //reference to object
    public List<GameObject> NearGameobjects = new List<GameObject>();   //reference to list of obstacles
    GameObject closestObject;   //variable for closest obstacle
    

    public IsLeftNearest(PlayerMovement ai, List<GameObject> NearGameobjects)   //upon creation, pass through AI object details and nearest game obstacles
    {
        this.ai = ai;
        this.NearGameobjects = NearGameobjects;
    }

    public override NodeState Evaluate()
    {
        //ai.running = true;
        closestObject = closestObstacle(ai, NearGameobjects);   //set the closest object equal to the result of the closest obstacle function. Pass through our AI object and list of game object details

        if (closestObject.tag == "TallFreeLeft")    //if the tag of the closest obstacle is "TallFreeLeft"
        {
            if (Vector3.Distance(ai.transform.position, closestObject.transform.position) < 5f) //we want to check distance is okay before swapping to success and executing the move node
            {
                //ai.running = false;
                return NodeState.SUCCESS;
            }
            //ai.running = false;
            return NodeState.FAILURE;
        }
        else                   //if the closest object is not TallFreeLeft, we return failure and move to next node.
        {
            //ai.running = false;
            return NodeState.FAILURE;
        }
    }
}
