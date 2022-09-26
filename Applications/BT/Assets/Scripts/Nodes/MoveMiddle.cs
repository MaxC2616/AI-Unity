using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMiddle : Node      //inherit from Node
{
    private PlayerMovement ai;  //reference to AI object

    private Vector3 currentPosition, middlePosition;    //variables for current object position and the desired position for a middle movement

    public MoveMiddle(PlayerMovement ai)    //passing AI object details through
    {
        this.ai = ai;
        
    }

    public override NodeState Evaluate()
    {
        //ai.running = true;
        currentPosition = ai.GetCurrentPosition();  //get current position of AI
        middlePosition = new Vector3(0, currentPosition.y, currentPosition.z); //assigning middle position
        ai.hasJumped = false;   //setting the variable of hasJumped in PlayerMovement class to false

        if (currentPosition != middlePosition)  //if the object isn't already in the left position,
        {

            ai.transform.position = middlePosition; //go to middle position

            ai.transform.Translate(Vector3.forward * ai.speed * Time.deltaTime);    //keep moving forward
            //ai.running = false;
            return NodeState.SUCCESS;
        }
        else     //if object is already in correct position, return success also
        {
            //ai.running = false;
            return NodeState.SUCCESS;
        }
    }
}
