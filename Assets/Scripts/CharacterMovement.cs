using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{

    [Header("Movement Settings")]    
    [SerializeField] float speed = 5.0f;
    [SerializeField] float jumpForce = 2.0f;

    [Header("Ground Check")]
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private LayerMask groundMask;
    private int MaxJump = 2;
    private int jumpsleft;


    bool isGrounded = true;
    bool isJumping = false;
    bool isMidair = false;
    private Vector3 startPosition;

    private Vector3 moveDirection;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody>();
      rb.constraints = RigidbodyConstraints.FreezeRotation;
        startPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position + transform.up * groundDistance / 2, -transform.up, groundDistance, groundMask);

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = transform.right * moveX + transform.forward * moveZ;

         if (isGrounded)
            {
            jumpsleft = MaxJump;
            } 

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            
        }

        if(transform.position.y <= -10)
        {
            Respawn();
        }
    }

    void FixedUpdate()
    {

        MovePlayer();

      if(isJumping)
        {
            Jump();  
            isJumping = false;
        }
             
        // This method is called every fixed framerate frame
    }

    private void MovePlayer()
    {
        Vector3 velocity = moveDirection * speed;
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z); 
    }

    private void Jump()
    {
        if (jumpsleft <= 0) return;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpsleft--;

    }
    
    private void Respawn()
    { 
         
            rb.position = startPosition;
        rb.linearVelocity = Vector3.zero;
        
        
    }



}
