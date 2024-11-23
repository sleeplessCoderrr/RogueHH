using System;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
    {
        protected Dictionary<EState, BaseState<EState>> States = new Dictionary <EState, BaseState<EState>>();
        protected BaseState<EState> CurrentState;
        protected bool IsTransitioning = false;

        void Start()
        {
            CurrentState.EnterState();
        }

        void Update()
        {
            EState nextState = CurrentState.GetNextState();
            if (nextState.Equals(CurrentState.StateKey) && !IsTransitioning)
            {
                CurrentState.UpdateState();
            }
            else
            {
                TransitionState(nextState);       
            }
        }

        private void TransitionState(EState stateKey)
        {
            IsTransitioning = true;
            CurrentState.ExitState();
            CurrentState = States[stateKey];
            CurrentState.EnterState();
            IsTransitioning = false;
        }

        void OnTriggerEnter(Collider other){}

        void OnTriggerStay(Collider other){}

        void OnTriggerExit(Collider other){}
    }
}