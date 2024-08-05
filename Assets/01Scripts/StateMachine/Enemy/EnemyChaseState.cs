using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ActionDemo
{
    public class EnemyChaseState : AIStateBase
    {
        private Vector3 _chasingStartPos;
        private bool _isReturning = false;

        public EnemyChaseState(Enemy enemy, EnemyStateMachineController enemyStateMachineController) : base(enemy, enemyStateMachineController)
        {
        }

        public override void CheckSwitchStates()
        {
            if ((HasTargetEscaped() || enemy.ChasingTarget.IsDeath) && !_isReturning)
            {
                _isReturning = true;
                enemy.StartCoroutine(GoStartPos());
            }
        }

        public override void EnterState()
        {
            _chasingStartPos = enemy.transform.position;
            enemy.NavMeshAgent.stoppingDistance = enemy.WeaponController.CurrentWeapon.GetWeaponSO().DamageAttributes.Range * .35f;
        }

        public override void ExitState()
        {
            _isReturning = false;
            enemy.NavMeshAgent.stoppingDistance = 0f;
        }

        public override void UpdateState()
        {
            ChaseTarget();
        }

        private bool HasTargetEscaped()
        {
            return Vector3.Distance(enemy.transform.position, enemy.ChasingTarget.transform.position) >= enemy.ChaseRange;
        }

        private void ChaseTarget()
        {
            if(_isReturning || enemy.ChasingTarget.IsDeath) return;
            enemy.CharacterAnimationHelper.ApplyMovementAnimation(new Vector2(0, enemy.NavMeshAgent.velocity.magnitude));
            
            if (!HasTargetEscaped())
            {
                enemy.NavMeshAgent.SetDestination(enemy.ChasingTarget.transform.position);
                enemy.transform.LookAt(enemy.ChasingTarget.transform, Vector3.up);

                if (enemy.NavMeshAgent.enabled && enemy.NavMeshAgent.remainingDistance <= enemy.WeaponController.CurrentWeapon.GetWeaponSO().DamageAttributes.Range * .75f)
                {
                    enemy.Attack();
                }
            }
        }

        private IEnumerator GoStartPos()
        {
            enemy.NavMeshAgent.stoppingDistance = 0f;

            while (enemy.NavMeshAgent.enabled && enemy.NavMeshAgent.remainingDistance > .5f)
            {
                enemy.NavMeshAgent.SetDestination(_chasingStartPos);
                yield return null;
            }
            
            enemyStateMachineController.ChangeStateTo(EnemyStates.Idle);
        }
    }
}
