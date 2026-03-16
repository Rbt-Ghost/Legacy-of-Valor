using UnityEngine;
using System;

public class PlayerMovementState : MonoBehaviour
{
    public enum MovementState
    {
        Idle,
        Running,
        Jumping,
        Defending,
        Attacking
    }

    public MovementState CurrentMoveState { get; private set; }

    public static Action<MovementState> OnPlayerMovementStateChanged;

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D myRigidBody;
    private const string isIdle = "Idle";
    private const string isRunning = "Run";
    private const string isJumping = "Jump";
    private const string isDefending = "Defend";
    private const string isAttacking = "Attack";

    private void Update()
    {
        // If player is in the air
        if (Mathf.Abs(myRigidBody.linearVelocity.y) > 0.1f)
        {
            SetMovementState(MovementState.Jumping);
        }
        // If player is moving on ground
        else if (Mathf.Abs(myRigidBody.linearVelocity.x) > 0.1f)
        {
            SetMovementState(MovementState.Running);
        }
        // Otherwise idle
        else
        {
            SetMovementState(MovementState.Idle);
        }
    }

    public void SetMovementState(MovementState moveState)
    {
        if (moveState == CurrentMoveState) return;

        switch (moveState)
        {
            case MovementState.Idle:
                HandleIdle();
                break;
            case MovementState.Running:
                HandleRunning();
                break;
            case MovementState.Jumping:
                HandleJumping();
                break;
            case MovementState.Defending:
                HandleDefending();
                break;
            case MovementState.Attacking:
                HandleAttacking();
                break;
            default:
                Debug.LogError("Unhandled movement state: " + moveState);
                break;
        }

        OnPlayerMovementStateChanged?.Invoke(moveState);
        CurrentMoveState = moveState;
    }

    private void HandleIdle()
    {
        animator.Play(isIdle);
    }

    private void HandleRunning()
    {
        animator.Play(isRunning);
    }

    private void HandleJumping()
    {
        animator.Play(isJumping);
    }

    private void HandleDefending()
    {
        animator.Play(isDefending);
    }

    private void HandleAttacking()
    {
        animator.Play(isAttacking);
    }
}
