using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShouldUncrouch : Node  //inherit from Node
{
    private PlayerMovement ai;  //reference to ai object
    public List<GameObject> NearGameobjects = new List<GameObject>();   //reference to list of obstacles
    GameObject closestObject;   //variable for closest obstacle


    public ShouldUncrouch(PlayerMovement ai, List<GameObject> NearGameobjects)  //upon creation, pass through AI object details and nearest game obstacles
    {
        this.ai = ai;
        this.NearGameobjects = NearGameobjects;
    }

    public override NodeState Evaluate()
    {
        //ai.running = true;
        closestObject = closestObstacle(ai, NearGameobjects);   //set the closest object equal to the result of the closest obstacle function. Pass through our AI object and list of game object details

        if (closestObject.tag == "Crouch")  //if the tag of the closest obstacle is "Crouch"
        {
            if (Vector3.Distance(ai.transform.position, closestObject.transform.position) >= 3f)    //if the distance is greater than 3f, we know it is safe to uncrouch 
            {
                //ai.running = false;
                return NodeState.SUCCESS;
            }
            //ai.running = false;
            return NodeState.FAILURE;
        }
        else    //if tag is not crouch, return failure
        {
            //ai.running = false;
            return NodeState.FAILURE;
        }
    }
}
