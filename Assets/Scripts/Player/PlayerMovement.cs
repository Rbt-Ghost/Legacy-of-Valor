using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer mySpriteRenderer;
    [SerializeField] private Rigidbody2D myRigidBody;
    [SerializeField] private Animator myAnimator;
    [SerializeField] private int speed = 5;
    [SerializeField] private int jumpForce = 7;

    private Vector2 movement;
    private bool isGrounded;

    void Update()
    {
        MovementHandler();
        UpdateAnimmationState();
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
            myAnimator.SetBool("isJumping", true);
        }
    }

    /// <summary>
    /// 
    /// This function handles the aniation state of the player character based on the input
    /// 
    /// </summary>
    private void UpdateAnimmationState()
    {
        if (!isGrounded)
        {
            myAnimator.SetBool("isRunning", false);
            myAnimator.SetBool("isJumping", true);
            return;
        }

        myAnimator.SetBool("isJumping", false);

        if (movement.x != 0)
            myAnimator.SetBool("isRunning", true);
        else
            myAnimator.SetBool("isRunning", false);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
