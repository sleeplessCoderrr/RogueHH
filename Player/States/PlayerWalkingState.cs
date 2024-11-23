using System;
using UnityEngine;
using States;

public class PlayerWalkingState : BaseState<PlayerState>
{
    private readonly PlayerStateManager _stateManager;
    private Animator _animator;

    public PlayerWalkingState(PlayerStateManager stateManager, PlayerState key) : base(key)
    {
        _stateManager = stateManager;
        _animator = stateManager.GetComponentInChildren<Animator>();
    }

    public override void EnterState()
    {
        Debug.Log("Entered Walking State");
        if (_animator != null)
        {
            _animator.SetBool("isWalking", true); // Trigger walking animation
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Walking State");
        if (_animator != null)
        {
            _animator.SetBool("isWalking", false); // Stop walking animation
        }
    }

    public override void UpdateState()
    {
        // Handle walking logic, e.g., move the player
        Vector3 movement = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );

        if (movement.sqrMagnitude > 0.01f) // Small threshold to ignore minor inputs
        {
            _stateManager.transform.Translate(movement * _stateManager.playerConfig.walkSpeed * Time.deltaTime);
        }
    }

    public override PlayerState GetNextState()
    {
        // Transition back to Idle if no movement input
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            return PlayerState.Idle;
        }

        return StateKey; // Remain in WalkingState
    }

    public override void OnTriggerEnter(Collider other)
    {
        Debug.Log($"WalkingState: Trigger entered by {other.gameObject.name}");
        // Handle any trigger-specific behavior here
    }

    public override void OnTriggerStay(Collider other)
    {
        Debug.Log($"WalkingState: Trigger stay with {other.gameObject.name}");
    }

    public override void OnTriggerExit(Collider other)
    {
        Debug.Log($"WalkingState: Trigger exit by {other.gameObject.name}");
    }
}
