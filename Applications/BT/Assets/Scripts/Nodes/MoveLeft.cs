using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : Node        //inherit from Node
{
    private PlayerMovement ai;  //reference to AI object
    
    private Vector3 currentPosition, leftPosition;  //variables for current object position and the desired position for a left movement

    public MoveLeft(PlayerMovement ai)  //passing AI object details through
    {
        this.ai = ai;
        
    }

    public override NodeState Evaluate()
    {
        //ai.running = true;
        currentPosition = ai.GetCurrentPosition();  //get current position of AI
        leftPosition = new Vector3(-5, currentPosition.y, currentPosition.z);   //assigning left position 
        ai.hasJumped = false;   //setting the variable of hasJumped in PlayerMovement class to false

        if (currentPosition != leftPosition)    //if the object isn't already in the left position,
        {

            ai.transform.position = leftPosition;   //go to left position

            ai.transform.Translate(Vector3.forward * ai.speed * Time.deltaTime);    //keep moving forward
            //ai.running = false;
            return NodeState.SUCCESS;   //return success
        }
        else     //if object is already in left position, return success also
        {
            //ai.running = false;
            return NodeState.SUCCESS;
        }
    }
}
