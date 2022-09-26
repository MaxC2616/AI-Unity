using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //This is a ref. to the Rigidbody component called "rb"
    public Rigidbody rb;

    
    public float speed = 10f;   //speed variable
    private Vector3 currentPosition;    //current position variable
    public List<float> times = new List<float>();
    public Vector3 endLocation;
   
    public List<GameObject> NearGameobjects = new List<GameObject>();   //list of in game obstacles
    public bool hasJumped = false;  //hasJumped boolean for checking if we've already jumped over an obstacle while in the same "state"


    private Node topNode;   //variable with ref. to Node class

    public bool running = false;
    public float timerTime, startTime, stopTime;

    public Vector3 GetCurrentPosition() //function to obtain current position which other classes can call
    {
        currentPosition = transform.position;
        return currentPosition;
    }

    private void Start()    //Upon starting, we construct the Behaviour Tree
    {
        endLocation = new Vector3(0, 3, 112);
        //This is setting up all of our child nodes
        IsLeftNearest leftNearestNode = new IsLeftNearest(this, NearGameobjects);
        MoveLeft moveLeftNode = new MoveLeft(this);

        IsRightNearest rightNearestNode = new IsRightNearest(this, NearGameobjects);
        MoveRight moveRightNode = new MoveRight(this);

        IsMiddleNearest middleNearestNode = new IsMiddleNearest(this, NearGameobjects);
        MoveMiddle moveMiddleNode = new MoveMiddle(this);

        IsJumpNearest jumpNearestNode = new IsJumpNearest(this, NearGameobjects);
        JumpAction jumpNode = new JumpAction(this);

        IsCrouchNearest crouchNearestNode = new IsCrouchNearest(this, NearGameobjects);
        CrouchAction crouchNode = new CrouchAction(this);

        ShouldUncrouch uncrouchNearestNode = new ShouldUncrouch(this, NearGameobjects);
        UncrouchAction uncrouchNode = new UncrouchAction(this);


        //This is setting up all of our sequence nodes and their children
        Sequence UncrouchSequence = new Sequence(new List<Node> { uncrouchNearestNode, uncrouchNode });
        Sequence CrouchSequence = new Sequence(new List<Node> { crouchNearestNode, crouchNode });
        Sequence JumpSequence = new Sequence(new List<Node> { jumpNearestNode, jumpNode });
        Sequence RightSequence = new Sequence(new List<Node> { rightNearestNode, moveRightNode });
        Sequence MiddleSequence = new Sequence(new List<Node> { middleNearestNode, moveMiddleNode });
        Sequence LeftSequence = new Sequence(new List<Node> { leftNearestNode, moveLeftNode });

        //Setting up the "Root" node for the program, containing all our sequence nodes
        topNode = new Selector(new List<Node> { LeftSequence, MiddleSequence, RightSequence, JumpSequence, CrouchSequence, UncrouchSequence }, this);
    }

    void Update()
    {
        
        if (running == false)
        {

            stopTime = Time.time;

            //timerTime = stopTime + (Time.time - startTime);
            timerTime = stopTime - startTime;

            //Debug.Log("Timer time - " + timerTime);

            times.Add(timerTime);

        }

        

        //startTime = Time.time;

        topNode.Evaluate(); //every frame update, we evalute the BT

        if (transform.position.z > 105)
        {
            float avg = Queryable.Average(times.AsQueryable());
            Debug.Log("Average time is: " + avg);
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);  //keep moving forward

        if (rb.position.y < -8f)    //if for some reason the object falls off the plane,
        {
            FindObjectOfType<GameManager>().EndGame();  //end game
        }
    }
    

}
 