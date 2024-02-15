using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Vector2 moveDirection;
    InputManager inputManager;
    Rigidbody2D rigidbody;
    private bool isGrounded;
    
    public LayerMask ground;

    public float moveSpeed;
    public float jumpForce;
    public float groundRaycast;

    private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            rigidbody = GetComponent<Rigidbody2D>();
        }

    private void FixedUpdate()
        {
            PlayerMovement();
            GroundCheck();
            PlayerJump();
            //playerActions...
        }

    private void PlayerMovement()
        {
            moveDirection = inputManager.movementInput;
            moveDirection.Normalize();
            moveDirection.y = 0;

            rigidbody.velocity = new Vector2(moveDirection.x * moveSpeed, rigidbody.velocity.y);
        
        }

    private void GroundCheck()
    {
        Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y);

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, groundRaycast, ground);

        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void PlayerJump()
        {
            if (inputManager.isJump && isGrounded)
                {
                    rigidbody.velocity = Vector2.up * jumpForce;

                    inputManager.isJump = false;
                }
        }
}
