using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    public PlayerMovement movement;

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "TallFreeRight" || collisionInfo.collider.tag == "TallFreeLeft" || collisionInfo.collider.tag == "TallFreeMiddle" || collisionInfo.collider.tag == "Jump" || collisionInfo.collider.tag == "Crouch")
        {
            movement.enabled = false;
            FindObjectOfType<GameManager>().EndGame();
        }
    }

}
