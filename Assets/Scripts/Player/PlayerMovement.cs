using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public SpriteRenderer mySpriteRenderer;
    public Rigidbody2D myRigidBody;
    public Animator myAnimator;
    public int speed = 5;

    private Vector2 movement;

    void Update()
    {
        MovementHandler();
        SpriteFlipX();
    }

    private void FixedUpdate()
    {
        myRigidBody.linearVelocity = movement;
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
    }

    /// <summary>
    /// 
    /// Automaticaly flip the SpriteRenderer on the Y axis
    /// 
    /// </summary>
    private void SpriteFlipX()
    {
        if (movement == Vector2.zero)
            myAnimator.SetBool("isRunning", false);
        else
            myAnimator.SetBool("isRunning", true);
    }
}
