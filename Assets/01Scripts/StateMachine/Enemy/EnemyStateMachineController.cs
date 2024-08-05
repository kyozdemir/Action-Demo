using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    //Just a simple state machine controller. It may extent to hfsm.
    //It's not game designer friendly. I have to use scriptable objects but time is ticking.
    public class EnemyStateMachineController
    {
        private Enemy _enemy;
        private AIStateBase _currentState;
        private List<AIStateBase> _states;

        public AIStateBase CurrentState => _currentState;

        public EnemyStateMachineController(Enemy enemy)
        {
            _enemy = enemy;
            InitializeStateMachine();
        }

        private void InitializeStateMachine()
        {
            _states = new List<AIStateBase>
            {
                new EnemyIdleState(_enemy, this),
                new EnemyPatrolState(_enemy, this),
                new EnemyChaseState(_enemy, this),
                new EnemyDieState(_enemy, this)
            };

            _currentState = _states[0];
            _currentState.EnterState();
        }

        public void ChangeStateTo(EnemyStates state, bool force = false)
        {
            if (_currentState != _states[(int)state] || force)
            {
                _currentState.ExitState();
                _currentState = _states[(int)state];
                _currentState.EnterState();
            }
        }

        public void UpdateState()
        {
            _currentState.UpdateState();
            _currentState.CheckSwitchStates();
        }
    }
}
