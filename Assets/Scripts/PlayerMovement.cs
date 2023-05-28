using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float airMultiplier;
    public float rotationSpeed;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Terrain Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;


    [Header("Character Assignment")]
    public Transform playerObj;

    public Transform orientation;
    public Animator animator;
    public CharacterController controller;

    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;

    [Header("Knockback")]
    public float knockBackForce;
    public float knockBackTime;
    private float knockBackCounter;

    [Header("ClimbDetection")]
    public LayerMask whatIsClimbable;
    public float detectionLength;
    public float sphereCastRadius;
    public float maxWallLookAngle;
    private float wallLookAngle;

    private RaycastHit frontWallHit;
    private bool wallFront;
    private bool climbing;

    [Header("FallDamage")]
    public float fallThresholdVelocity;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
        climbing = false;
    }

    private void Update()
    {
        bool previousGrounded = grounded;
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        if (!previousGrounded && grounded && rb.velocity.y < -fallThresholdVelocity) {
            int damage = Mathf.FloorToInt(rb.velocity.y * 1.5f / -fallThresholdVelocity); // custom formula :)
            FindObjectOfType<HealthManager>().Hurt(damage, new Vector3(0, 0, 0));
        }

        if (knockBackCounter <= 0){
            MyInput();
            SpeedControl();
        }else{
            knockBackCounter -= Time.deltaTime;
        }
        

        // Variabel animasi
        animator.SetBool("isGround", grounded);
        animator.SetFloat("Speed", rb.velocity.magnitude);

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        if (knockBackCounter <= 0){
            MovePlayer();
        }else{
            knockBackCounter -= Time.deltaTime;
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
        }

        if (Input.GetKeyUp(jumpKey)) {
            readyToJump = true;
        }

        if (wallFront && wallLookAngle < maxWallLookAngle)
            climbing = true;
        else if (climbing)
            climbing = false;
    }

    private void MovePlayer()
    {
        WallCheck();
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (moveDirection != Vector3.zero)
            transform.forward = Vector3.Slerp(transform.forward, moveDirection.normalized, Time.deltaTime * rotationSpeed);

        
        if(climbing)
            rb.velocity = new Vector3(rb.velocity.x, moveSpeed * airMultiplier, rb.velocity.z);
        // on ground
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    public void Knockback(Vector3 direction){
        knockBackCounter = knockBackTime;
        moveDirection = direction * knockBackForce;
        moveDirection.y = knockBackForce * 0.2f;
        rb.AddForce(moveDirection.normalized * knockBackForce * 10f, ForceMode.Force);
    }
    private void WallCheck(){
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, moveDirection, out frontWallHit, detectionLength, whatIsClimbable);
        wallLookAngle = Vector3.Angle(transform.forward, -frontWallHit.normal);
    }
}