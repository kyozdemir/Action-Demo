using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ActionDemo
{
    public class Enemy : Character
    {
        [Header("Enemy Specific Components")]
        [SerializeField] private CapsuleCollider capsuleCollider;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private PlayerDetector playerDetector;
        [SerializeField] private WeaponSO weaponReference;

        [Header("Enemy Specific Fields")]
        [SerializeField] private float chaseRange;
        [SerializeField] private float idleDuration;
        [SerializeField] private float playerDetectionRadius;

        private EnemyStateMachineController _enemyStateMachineController;
        private Character _chasingTarget;

        public float ChaseRange => chaseRange;
        public Character ChasingTarget => _chasingTarget;
        public float IdleDuration => idleDuration;
        public NavMeshAgent NavMeshAgent => navMeshAgent;
        public WeaponSO WeaponReference { get => weaponReference; set => weaponReference = value; }

        public override void Initialize()
        {
            base.Initialize();

            navMeshAgent.Warp(transform.position);
            navMeshAgent.speed = moveSpeed;

            playerDetector.OnPlayerDetected += OnPlayerDetected;
            playerDetector.Initialize();
            playerDetector.ChangeRadius(playerDetectionRadius);
            
            _enemyStateMachineController = new EnemyStateMachineController(this);

            weaponController.EquipWeapon(weaponReference);
        }

        void Update()
        {
            _enemyStateMachineController.UpdateState();
        }

        public override void Die()
        {
            capsuleCollider.enabled = false;
            navMeshAgent.enabled = false;
            _enemyStateMachineController.ChangeStateTo(EnemyStates.Die, true);
            playerDetector.OnPlayerDetected -= OnPlayerDetected;
            base.Die();
        }

        public override void Respawn()
        {
            capsuleCollider.enabled = true;
            navMeshAgent.enabled = true;
            _enemyStateMachineController.ChangeStateTo(EnemyStates.Idle, true);
            playerDetector.OnPlayerDetected += OnPlayerDetected;
            base.Respawn();
        }

        private void OnPlayerDetected(Character player)
        {
            _chasingTarget = player;
            _enemyStateMachineController.ChangeStateTo(EnemyStates.Chase, true);
        }

    }
}
