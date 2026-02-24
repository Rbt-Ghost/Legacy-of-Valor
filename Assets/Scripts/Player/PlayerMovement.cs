using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public int velocity = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        var keyboard = Keyboard.current;

        if (keyboard.wKey.wasPressedThisFrame)
        {
            myRigidBody.linearVelocity = Vector2.up * velocity;
        }

        if (keyboard.aKey.wasPressedThisFrame)
        {
            myRigidBody.linearVelocity = Vector2.left * velocity;
        }

        if (keyboard.dKey.wasPressedThisFrame)
        {
            myRigidBody.linearVelocity = Vector2.right * velocity;
        }

    }
}
