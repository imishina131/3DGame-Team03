using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    public float groundDrag;
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    bool crouching;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        MyInput();
        SpeedControl();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

        Debug.Log(grounded);
    }

    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && crouching == false)
        {
            moveSpeed = moveSpeed * 2;
            animator.SetBool("isSprinting", true);
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift) && crouching == false)
        {
            moveSpeed = moveSpeed / 2;
            animator.SetBool("isSprinting", false);
        }

        if (Input.GetKey(KeyCode.Space) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Debug.Log("jumping");
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if(Input.GetKeyDown(KeyCode.LeftControl) && crouching == false)
        {
            animator.SetBool("isCrouching", true);
            crouching = true;
            moveSpeed = moveSpeed / 2;

        }
        else if(Input.GetKeyDown(KeyCode.LeftControl) && crouching == true)
        {
            animator.SetBool("isCrouching", false);
            crouching = false;
            moveSpeed = moveSpeed * 2;
        }
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void Jump()
    {
        animator.SetBool("isJumping", true);
        animator.SetBool("isGrounded", false);
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        animator.SetBool("isJumping", false);
        animator.SetBool("isGrounded", true);
        readyToJump = true;
    }
}
