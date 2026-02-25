using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public int speed = 5;
    private Vector2 movement;

    void Update()
    {
        var keyboard = Keyboard.current;

        movement = Vector2.zero;

        if (keyboard.aKey.isPressed)
        {
            movement = Vector2.left * speed;
        }

        if (keyboard.dKey.isPressed)
        {
           movement += Vector2.right * speed;
        }
    }

    private void FixedUpdate()
    {
        myRigidBody.linearVelocity = movement;
    }
}
