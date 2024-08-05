using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    public class EnemyIdleState : AIStateBase
    {
        float timePassed = 0;
        public EnemyIdleState(Enemy enemy, EnemyStateMachineController enemyStateMachineController) : base(enemy, enemyStateMachineController)
        {
        }

        public override void CheckSwitchStates()
        {
            if (timePassed >= enemy.IdleDuration)
            {
                enemyStateMachineController.ChangeStateTo(EnemyStates.Patrol);
            }
        }

        public override void EnterState()
        {
            timePassed = 0;
            enemy.CharacterAnimationHelper.ApplyMovementAnimation(Vector2.zero);
            enemy.NavMeshAgent.enabled = false;
        }

        public override void ExitState()
        {
            enemy.NavMeshAgent.enabled = true;
        }

        public override void UpdateState()
        {
            timePassed += Time.deltaTime;
        }
    }
}
