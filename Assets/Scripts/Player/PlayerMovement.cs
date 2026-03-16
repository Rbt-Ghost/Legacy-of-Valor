using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer mySpriteRenderer;
    [SerializeField] private Rigidbody2D myRigidBody;
    [SerializeField] private int speed = 5;
    [SerializeField] private int jumpForce = 7;
    [SerializeField] private PlayerMovementState playerMovementState;

    private Vector2 movement;
    private float playerHalfHeight;
    private bool isGrounded;

    private void Start()
    {
        playerHalfHeight = mySpriteRenderer.bounds.extents.y /2;
    }

    void Update()
    {
        isGrounded = CheckGrounded();
        MovementHandler();

        // DEbug only
        //Vector2 origin = (Vector2)transform.position + Vector2.down * (playerHalfHeight - 0.02f);
        //Debug.DrawRay(origin, Vector2.down * 0.1f, Color.red);
    }

    private void FixedUpdate()
    {
        myRigidBody.linearVelocity = new Vector2(
            movement.x,
            myRigidBody.linearVelocity.y
        );
    }

    /// <summary>
    /// 
    /// Handle the player input for character movement
    /// 
    /// </summary>
    private void MovementHandler()
    {
        var keyboard = Keyboard.current;

        movement = Vector2.zero;

        if (keyboard.aKey.isPressed)
        {
            movement = Vector2.left * speed;
            mySpriteRenderer.flipX = true;
        }

        if (keyboard.dKey.isPressed)
        {
            movement += Vector2.right * speed;
            mySpriteRenderer.flipX = false;
        }

        if (keyboard.wKey.wasPressedThisFrame && isGrounded)
        {
            Jump();
        }

    }

    /// <summary>
    /// 
    /// Apply an impulse ( (0,X), x >= 1)  to the player's RigidBody2D
    /// 
    /// </summary>
    private void Jump()
    {
        myRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }


    /// <summary>
    /// 
    /// Checks if the player is currently touching the ground.
    /// It casts a short ray downwards from an offset near the bottom of the player's bounds
    /// to ensure detection only occurs at the feet.
    /// 
    /// </summary>
    /// <returns>
    /// 
    /// True if the ray hits a collider on the "Ground" layer within the specified distance; 
    /// otherwise, false.
    /// 
    /// </returns>
    private bool CheckGrounded()
    {
        Vector2 origin = (Vector2)transform.position + Vector2.down * (playerHalfHeight - 0.02f);
        float rayDistance = 0.1f;

        return Physics2D.Raycast(origin, Vector2.down, rayDistance, LayerMask.GetMask("Ground"));
    }
}
