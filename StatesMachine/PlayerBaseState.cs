﻿using UnityEngine;

public abstract class PlayerStateBase
{
    protected Player Player;
    protected Animator Animator;
    protected PlayerStateBase(Player player, Animator animator)
    {
        Player = player;
        Animator = animator;
    }

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public abstract PlayerState? GetNextState(); 
}