using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace ActionDemo
{
    [RequireComponent(typeof(CharacterController), typeof(Animator), typeof(WeaponController))]
    public class PlayerController : Character
    {
        private bool _isInputEnabled;
        private CharacterController _characterController;
        private CharacterMovement _characterMovement;
        private PlayerLookAt _playerLookAt;

        private void Awake()
        {
            InputManager.Instance.OnMovementInputChanged += OnMovementInputChanged;
            InputManager.Instance.OnMouseWorldPositionChanged += OnMouseWorldPositionChanged;
            InputManager.Instance.OnWeaponFireInput += OnWeaponFireInput;
            InputManager.Instance.OnWeaponSelectionPressed += OnWeaponSelectionPressed;
        }

        void Start()
        {
            Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();
            _isInputEnabled = true;
            _characterController = GetComponent<CharacterController>();
            _characterMovement = new CharacterMovement(_characterController);
            _playerLookAt = new PlayerLookAt(rotationSpeed, transform);
        }

        #region Weapon & Attachment

        public bool CanCollectWeapon(WeaponSO weaponSO)
        {
            return weaponController.CanCollectWeapon(weaponSO);
        }

        public void CollectWeapon(WeaponSO weaponSO)
        {
            weaponController.EquipWeapon(weaponSO);
        }

        public bool CanAttach(AttachmentModel attachmentModel)
        {
            return weaponController.CanAttachCurrentWeapon(attachmentModel);
        }

        public void AttachCurrentWeapon(AttachmentModel attachmentModel)
        {
            weaponController.AttachCurrentWeapon(attachmentModel);
        }

        #endregion Weapon & Attachment

        #region Movement Input Actions

        private void OnMovementInputChanged(Vector2 direction)
        {
            if (!_isInputEnabled) return;
            _characterMovement.MoveTowards(direction.normalized, moveSpeed);
            characterAnimationHelper.ApplyMovementAnimation(direction);
        }

        private void OnMouseWorldPositionChanged(Vector3 position)
        {
            if (!_isInputEnabled) return;
            _playerLookAt.LookAtDirection(position);
        }

        #endregion Movement Input Actions

        #region Weapon Input Actions

        #region Stats

        protected override void OnDeath()
        {
            Die();
        }

        public override void Die()
        {
            base.Die();
            _isInputEnabled = false;
            _characterController.enabled = false;
        }
        public override void Respawn()
        {
            base.Respawn();
            _isInputEnabled = true;
            _characterController.enabled = true;
        }

        #endregion Stats

        private void OnWeaponSelectionPressed(int index)
        {
            if (!_isInputEnabled) return;
            weaponController.SelectWeapon(index - 1);
        }

        private void OnWeaponFireInput()
        {
            Attack();
        }

        #endregion Weapon Input Actions

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Tags.COLLECTABLE))
            {
                other.GetComponent<ICollectable>()?.Collect(this);
            }
        }

        void OnDisable()
        {
            InputManager.Instance.OnMovementInputChanged -= OnMovementInputChanged;
            InputManager.Instance.OnMouseWorldPositionChanged -= OnMouseWorldPositionChanged;
            InputManager.Instance.OnWeaponFireInput -= OnWeaponFireInput;
            InputManager.Instance.OnWeaponSelectionPressed -= OnWeaponSelectionPressed;
        }
    }
}
