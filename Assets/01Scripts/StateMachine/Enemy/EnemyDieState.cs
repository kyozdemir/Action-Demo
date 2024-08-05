using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    public class EnemyDieState : AIStateBase
    {
        public EnemyDieState(Enemy enemy, EnemyStateMachineController enemyStateMachineController) : base(enemy, enemyStateMachineController)
        {
        }

        public override void CheckSwitchStates()
        {
            
        }

        public override void EnterState()
        {
            enemy.CharacterAnimationHelper.Die();
        }

        public override void ExitState()
        {
            
        }

        public override void UpdateState()
        {
            
        }
    }
}
