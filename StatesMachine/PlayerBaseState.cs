using UnityEngine;

public abstract class PlayerStateBase
{
    protected Player Entity;
    protected Animator Animator;
    protected PlayerStateBase(Player entity, Animator animator)
    {
        Entity = entity;
        Animator = animator;
    }

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public abstract PlayerState? GetNextState(); 
}