using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncrouchAction : Node      //inherit from node
{
    private PlayerMovement ai;      //reference to AI object

    private Vector3 currentPosition, uncrouchPosition, uncrouchScale;   //variables for current object position, the desired position for an uncrouch movement and an uncrouch scale

    public UncrouchAction(PlayerMovement ai)    //passing AI object details through
    {
        this.ai = ai;
        
    }

    public override NodeState Evaluate()
    {
        //ai.running = true;
        currentPosition = ai.GetCurrentPosition();  //get current position of AI
        uncrouchPosition = new Vector3(currentPosition.x, 1f, currentPosition.z);   //assigning uncrouch position

        uncrouchScale = new Vector3(1f, 1f, 1f);    //assigning uncrouch scale
        ai.hasJumped = false;   //setting has jumped to false

        if (currentPosition != uncrouchPosition)    //if the object is not currently in the uncrouch position
        {
            ai.transform.localScale = uncrouchScale;    //scale up the object to an uncrouched scale
            ai.transform.position = uncrouchPosition;   //put ai to uncrouch position (allows for smoother transition and less physics interfering with program)

            //ai.running = false;
            return NodeState.SUCCESS;
        }
        else      //if object already in position, return success
        {
            //ai.running = false;
            return NodeState.SUCCESS;
        }
           
    }
}
