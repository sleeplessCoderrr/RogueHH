// using UnityEngine;
// using System.Collections.Generic;
//
// public class EnemyController : MonoBehaviour
// {
//     private StateManager<EnemyState, Enemy> _stateManager;
//     private Enemy _enemy; // Reference to the controlled enemy
//
//     public void Initialize(Enemy enemy)
//     {
//         _enemy = enemy;
//
//         // Create the StateManager specific to this enemy
//         _stateManager = new EnemyStateManager(enemy);
//         _stateManager.InitializeStates();
//         _stateManager.Start(); // Manually start the state manager
//     }
//
//     private void Update()
//     {
//         if (_stateManager != null)
//         {
//             _stateManager.Update();
//         }
//     }
//
//     public void SetState(EnemyState state)
//     {
//         if (_stateManager != null)
//         {
//             _stateManager.TransitionState(state);
//         }
//     }
// }
