using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ActionDemo
{
    public class EnemyPatrolState : AIStateBase
    {
        private Vector3 _patrolTargetPos;

        public EnemyPatrolState(Enemy enemy, EnemyStateMachineController enemyStateMachineController) : base(enemy, enemyStateMachineController)
        {
        }

        public override void CheckSwitchStates()
        {
            if (HasReached())
            {
                enemyStateMachineController.ChangeStateTo(EnemyStates.Idle);
            }
        }

        public override void EnterState()
        {
            enemy.NavMeshAgent.speed = enemy.MoveSpeed;
            SetRandomDestination();
        }

        public override void ExitState()
        {
            _patrolTargetPos = Vector3.zero;
        }

        public override void UpdateState()
        {
            enemy.CharacterAnimationHelper.ApplyMovementAnimation(new Vector2(0, enemy.NavMeshAgent.velocity.magnitude));
        }

        private bool HasReached()
        {
            return !enemy.NavMeshAgent.pathPending && enemy.NavMeshAgent.remainingDistance < 0.1f;
        }

        private void SetRandomDestination()
        {
            Vector2 randomSphere = Random.insideUnitSphere * 5f;
            _patrolTargetPos = new Vector3(
                enemy.transform.position.x + randomSphere.x,
                enemy.transform.position.y,
                enemy.transform.position.z + randomSphere.y
            );
            NavMeshHit hit;
            NavMesh.SamplePosition(_patrolTargetPos, out hit, 10f, 1);
            Vector3 finalPosition = hit.position;
            enemy.NavMeshAgent.SetDestination(finalPosition);
        }
    }
}
