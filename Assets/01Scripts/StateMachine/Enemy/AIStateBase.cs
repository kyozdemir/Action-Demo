using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    public abstract class AIStateBase
    {
        protected Enemy enemy;
        protected EnemyStateMachineController enemyStateMachineController;

        public AIStateBase(Enemy enemy, EnemyStateMachineController enemyStateMachineController)
        {
            this.enemy = enemy;
            this.enemyStateMachineController = enemyStateMachineController;
        }

        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();
        public abstract void CheckSwitchStates();
    }
}
