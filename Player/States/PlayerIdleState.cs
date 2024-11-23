using System;
using UnityEngine;
using States;

public class PlayerIdleState : BaseState<PlayerState>
{
    private readonly PlayerStateManager _stateManager;
    private Animator _animator;

    public PlayerIdleState(PlayerStateManager stateManager, PlayerState key) : base(key)
    {
        _stateManager = stateManager;
        _animator = stateManager.GetComponentInChildren<Animator>();
    }

    public override void EnterState()
    {
        Debug.Log("Entered Idle State");
        if (_animator != null)
        {
            _animator.SetBool("isWalking", false); // Ensure walking animation is stopped
            _animator.SetBool("isIdle", true);     // Trigger idle animation
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Idle State");
        if (_animator != null)
        {
            _animator.SetBool("isIdle", false); // Stop idle animation when exiting
        }
    }

    public override void UpdateState()
    {
        // No movement in idle state, can add any behaviors such as checking for input or idle activities
    }

    public override PlayerState GetNextState()
    {
        // Transition to WalkingState if there is input for movement
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            return PlayerState.Walking;
        }

        return StateKey; // Stay in IdleState if no movement
    }

    public override void OnTriggerEnter(Collider other)
    {
        Debug.Log($"IdleState: Trigger entered by {other.gameObject.name}");
        // Handle any trigger-specific behavior here, like interactions
    }

    public override void OnTriggerStay(Collider other)
    {
        Debug.Log($"IdleState: Trigger stay with {other.gameObject.name}");
    }

    public override void OnTriggerExit(Collider other)
    {
        Debug.Log($"IdleState: Trigger exit by {other.gameObject.name}");
    }
}