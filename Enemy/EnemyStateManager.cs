// using UnityEditor.VersionControl;
// using System.Threading.Tasks;
// using UnityEngine;
// using Task = System.Threading.Tasks.Task;
//
// public enum EnemyState
// {
//     Idle,
//     Alert,
//     Aggro,
//     Attack
// }
//
// public class EnemyStateManager : StateManager<EnemyState, Enemy>
// {
//     public EnemyStateManager(Enemy entity, Enemy enemy) : base(entity)
//     {
//         InitializeStates();
//     }
//
//     protected override void InitializeStates()
//     {
//         States[EnemyState.Idle] = new EnemyIdleState(this, Entity.Animator, EnemyState.Idle, Entity);
//         States[EnemyState.Alert] = new EnemyAlertState(this, Entity.Animator, EnemyState.Alert, Entity);
//     }
// }