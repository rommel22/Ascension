using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbMovement : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Rigidbody rigidBody;
    public LayerMask whatIsClimbable;

    [Header("Climbing")]
    public float climbSpeed;
    // public float maxClimbTime;
    // private float climbTimer;

    private bool climbing;

    [Header("Detection")]
    public float detectionLength;
    public float sphereCastRadius;
    public float maxWallLookAngle;
    private float wallLookAngle;

    private RaycastHit frontWallHit;
    private bool wallFront;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        WallCheck();
        StateMachine();

        if(climbing) ClimbingMovement();
    }

    private void StateMachine(){
        if (wallFront && Input.GetKey(KeyCode.UpArrow) && wallLookAngle < maxWallLookAngle){
            StartClimbing();
        }
        else{
            if (climbing) StopClimbing();
        }
    }

    private void WallCheck(){
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, whatIsClimbable);
        wallLookAngle = Vector3.Angle(orientation.forward, -frontWallHit.normal);
    }

    private void StartClimbing(){
        climbing = true;
    }

    private void ClimbingMovement(){
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, climbSpeed, rigidBody.velocity.z);
    }

    private void StopClimbing(){
        climbing = false;
    }
}
