using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAction : Node      //inherit from node
{
    private PlayerMovement ai;  //reference to AI object

    private Vector3 currentPosition, jumpPosition;  //variables for current object position and the desired position for a jump movement

    public JumpAction(PlayerMovement ai)    //passing AI object details through
    {
        this.ai = ai;
        
    }

    public override NodeState Evaluate()
    {
        //ai.running = true;
        currentPosition = ai.GetCurrentPosition();  //get current position of AI
        jumpPosition = new Vector3(currentPosition.x, 4, currentPosition.z);   //assigning jump position

            if (ai.transform.position.y > 0.9f && ai.transform.position.y < 1.1f && ai.hasJumped == false) //if the object is not in the air and hasJumped is set to false
            {
                ai.transform.position = jumpPosition; //go to jump position
                ai.hasJumped = true;    //set has jumped to true
            //ai.running = false;
            return NodeState.SUCCESS;   //return running
            }
            else     //if ai has jumped or is in the air already, return success
            {
            //ai.running = false;
            return NodeState.SUCCESS;   
            }
    }
}
