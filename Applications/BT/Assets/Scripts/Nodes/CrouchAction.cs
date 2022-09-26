using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchAction : Node        //inherit from node
{
    private PlayerMovement ai;      //reference to AI object

    private Vector3 currentPosition, crouchPosition, crouchScale;   //variables for current object position, the desired position for a crouch movement and a crouch scale

    public CrouchAction(PlayerMovement ai)  //passing AI object details through
    {
        this.ai = ai;
        
    }

    public override NodeState Evaluate()
    {
        //ai.running = true;
        currentPosition = ai.GetCurrentPosition();  //get current position of AI
        crouchPosition = new Vector3(currentPosition.x, 0.8f, currentPosition.z);   //assigning crouch position
        crouchScale = new Vector3(1f, 0.5f, 1f);    //assigning crouch scale
        ai.hasJumped = false;   //setting has jumped to false

        if (currentPosition != crouchPosition)  //if the object is not currently in the crouch position
        {
            ai.transform.localScale = crouchScale;  //scale down the object to a crouch scale
            ai.transform.position = crouchPosition; //put ai to crouch position (allows for smoother transition and less physics interfering with program)

            //ai.running = false;
            return NodeState.SUCCESS;
        }
        else   //if object already in position, return success
        {
            //ai.running = false;
            return NodeState.SUCCESS;
        }
           
    }
}
