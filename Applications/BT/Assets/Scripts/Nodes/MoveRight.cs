using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : Node   //inherit from node
{
    private PlayerMovement ai;  //reference to AI object

    private Vector3 currentPosition, rightPosition; //variables for current object position and the desired position for a right movement

    public MoveRight(PlayerMovement ai) //passing AI object details through
    {
        this.ai = ai;
        
    }

    public override NodeState Evaluate()
    {
        //ai.running = true;
        currentPosition = ai.GetCurrentPosition();  //get current position of AI
        rightPosition = new Vector3(5, currentPosition.y, currentPosition.z);   //assigning right position
        ai.hasJumped = false;   //setting the variable of hasJumped in PlayerMovement class to false

        if (currentPosition != rightPosition)   //if the object isn't already in the correct position,
        {

            ai.transform.position = rightPosition;  //go to correct position

            ai.transform.Translate(Vector3.forward * ai.speed * Time.deltaTime);    //keep moving forward
            //ai.running = false;
            return NodeState.SUCCESS;
        }
        else      //if object is already in correct position, return success also
        {
            //ai.running = false;
            return NodeState.SUCCESS;
        }
    }
}
