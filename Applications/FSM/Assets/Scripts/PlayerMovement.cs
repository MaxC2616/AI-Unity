using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //This is a ref. to the Rigidbody component called "rb"
    public Rigidbody rb;

    private float speed = 10f;
    private Vector3 currentPosition;
    private Vector3 crouch, revert;
    public List<float> times = new List<float>();

    private enum State { Middle = 0, Left = 1, Right = 2, Jump = 3, Crouch = 4, Uncrouch = 5};
    private State aiState = State.Middle;

   //List of all gameobjects in scene
    public List<GameObject> NearGameobjects = new List<GameObject>();
    GameObject closestObject;
    private float oldDistance = 9999;
    private bool hasJumped = false;
    public float timerTime, startTime, stopTime;

    // Update is called once per frame
    //Fixed Update is better for dealing with physics
    void Update()
    {
        //Get current position for each frame
        currentPosition = transform.position;
        

        
        //Our Switch for each State
        switch (aiState)
        {
            //MIDDLE STATE
            case State.Middle:
                startTime = Time.time;
                
                hasJumped = false;  //set has jumped back to false
                

                transform.position = new Vector3(0, currentPosition.y, currentPosition.z);  //Move "player" object to middle
                

                transform.Translate(Vector3.forward * speed * Time.deltaTime);  //Keep the object moving forward
                

                CheckForObstacle();     //Run our check for nearest obstacle to initiate state swap, if applicable
                break;

            //LEFT STATE
            case State.Left:
                startTime = Time.time;
                hasJumped = false; //set hasJumped back to false

                transform.position = new Vector3(-5, currentPosition.y, currentPosition.z); //Move object to the left
                

                transform.Translate(Vector3.forward * speed * Time.deltaTime);  //Keep the object moving forward
               

              
                CheckForObstacle(); //Run nearest obstacle check
                break;

            //RIGHT STATE
            case State.Right:
                startTime = Time.time;
                hasJumped = false;
                transform.position = new Vector3(5, currentPosition.y, currentPosition.z);  //Move object to the right
                

                transform.Translate(Vector3.forward * speed * Time.deltaTime);  //Keep object moving forward
                

                
                CheckForObstacle(); //Run check for nearest obstacle
                break;

            //JUMP STATE
            case State.Jump:
                startTime = Time.time;
                
                if (rb.transform.position.y == 1f && hasJumped == false)    //if the object is not currently in the middle of a jump and hasn't already completed a jump while in this state. We don't want our object to constantly be jumping while in the state, just once is fine
                {
                    transform.position = new Vector3(currentPosition.x, 4, currentPosition.z); //then jump
                    hasJumped = true;   //set has jumped to true
                }
                

                transform.Translate(Vector3.forward * speed * Time.deltaTime);  //keep the object moving
                

                
                CheckForObstacle(); //Run nearest obstacle check
                break;

            //CROUCH STATE
            case State.Crouch:
                startTime = Time.time;
                hasJumped = false;
                

                transform.Translate(Vector3.forward * speed * Time.deltaTime);  //Keep object moving
                

                

                Crouch();   //Run our crouch function
                CheckForObstacle(); //Run our nearest obstacle check
                break;

            //UNCROUCH STATE
            case State.Uncrouch:
                startTime = Time.time;
                hasJumped = false;
                
                transform.Translate(Vector3.forward * speed * Time.deltaTime);  //Keep object moving
               

                Uncrouch(); //Run our uncrouch function
                CheckForObstacle(); //Run our nearest obstacle check
                break;
            default:
                break;
        }


        if (rb.position.y < -8f)    //if for some reason our AI falls off the map, the game will end
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    void Crouch()
    {
            currentPosition = transform.position;   //We get the current position
            crouch = new Vector3(1f, 0.5f, 1f);     //Set the scale for how we want the object to "crouch" 
         
            rb.transform.localScale = crouch;   //change the scale of the object to crouching

            transform.position = new Vector3(currentPosition.x, 0.8f, currentPosition.z);   //set a new Y coordinate for the object, to prevent any physics problems when the object begins to fall from a height due to scale change
        
    }

    void Uncrouch()
    {
        currentPosition = transform.position;   //Get current position
        revert = new Vector3(1f, 1f, 1f);   //Set the scale to revert the object back to original size

        rb.transform.localScale = revert;   //transform the object back to original size

        transform.position = new Vector3(currentPosition.x, 1f, currentPosition.z); //Set new Y coord for object so it fits back onto the plane nicely without any unusual physics occurring
    }

    void CheckForObstacle()
    {
        oldDistance = 9999; //Set this variable to 9999 after each call
        
        foreach (GameObject g in NearGameobjects)   //For every Obstacle GameObject we have in our list
        {
            float distance = Vector3.Distance(rb.transform.position, g.transform.position); //Get the real distance between the "player" object and obstacle objects
            
            if (distance < oldDistance) //if this distance is smaller than oldDistance
            {
                closestObject = g;  //Set the closestObject variable to the current object being checked
                oldDistance = distance; //set oldDistance to the distance of current object being checked. Now if there are any objects further away from "player" object than this one is, it won't enter this if statement
            }
        }


        if (closestObject.tag == "TallFreeLeft")    //if the closest object is one with a "left position is free" tag
        {
            if (Vector3.Distance(rb.transform.position, closestObject.transform.position) < 5f) //we wait until the distance between the two is appropriate and close, then 
            {
                aiState = State.Left;   //we enter the left state
            }
            stopTime = Time.time;
           
            timerTime = stopTime - startTime;
            //Debug.Log("Timer time is: " + timerTime);
            times.Add(timerTime);
                return;
            
        }
        else if (closestObject.tag == "TallFreeRight")  //if the closest object is one with a "right position is free" tag
        {
            
            if (Vector3.Distance(rb.transform.position, closestObject.transform.position) < 5f) //we wait until the distance between the two is appropriate and close, then 
            {
                
                aiState = State.Right;  //we enter the right state
            }
            stopTime = Time.time;
            
            timerTime = stopTime - startTime;
            //Debug.Log("Timer time is: " + timerTime);
            times.Add(timerTime);
            return;
        }
        else if (closestObject.tag == "TallFreeMiddle") //if the closest object is one with a "middle position is free" tag
        {
            if (Vector3.Distance(rb.transform.position, closestObject.transform.position) < 5f) //we wait until the distance between the two is appropriate and close, then
            {
                aiState = State.Middle; //we enter the middle state
            }
            stopTime = Time.time;
            
            timerTime = stopTime - startTime;
            //Debug.Log("Timer time is: " + timerTime);
            times.Add(timerTime);
            return;
            
        }
        else if (closestObject.tag == "Jump")   //if the closest object is one with a "jump" tag
        {
            if (Vector3.Distance(rb.transform.position, closestObject.transform.position) < 6f) // we wait until the distance between the two is appropriate and close, which is especially important for the jump action, then
            {
                aiState = State.Jump;   //we enter the jump state
            }
            stopTime = Time.time;
            
            timerTime = stopTime - startTime;
            //Debug.Log("Timer time is: " + timerTime);
            times.Add(timerTime);
            return;
        }
        else if (closestObject.tag == "Crouch") //if the closest object is one with the crouch tag
        {
            if (Vector3.Distance(rb.transform.position, closestObject.transform.position) < 3f) //we wait until the distance between the two is appropriate and close, then
            {
                aiState = State.Crouch; //we enter the crouch state
            }
            else           //Once the distance is safe to uncrouch and closest object is still the crouch obstacle, we can safely uncrouch
            {
                aiState = State.Uncrouch;
            }
            stopTime = Time.time;
            
            timerTime = stopTime - startTime;
            //Debug.Log("Timer time is: " + timerTime);
            times.Add(timerTime);
            return;
        }
        else         
        {
            float avg = Queryable.Average(times.AsQueryable());
            Debug.Log("Average time is " + avg);
            return;
        }
    }
}
 