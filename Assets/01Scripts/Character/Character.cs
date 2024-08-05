using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace ActionDemo
{
    public class Character : PoolObject
    {
        [Header("Base Character Components")]
        [SerializeField] protected StatsUI statsUI;
        [SerializeField] protected TwoBoneIKConstraint leftHandIK, rightHandIK, spineIK;
        protected Animator animator;
        protected CharacterAnimationHelper characterAnimationHelper;
        protected StatsController statsController;
        protected WeaponController weaponController;

        [Header("Character Fields")]
        [SerializeField] protected float moveSpeed;
        [SerializeField] protected float rotationSpeed;
        protected bool isDeath, isInitialized;

        public bool IsDeath => isDeath;
        public CharacterAnimationHelper CharacterAnimationHelper => characterAnimationHelper;
        public float MoveSpeed => moveSpeed;
        public float RotateSpeed => RotateSpeed;
        public StatsController StatsController => statsController;
        public WeaponController WeaponController => weaponController;

        public Action<Character> OnDeaht;

        public virtual void Initialize()
        {
            if(isInitialized) return;
            
            isInitialized = true;
            animator = GetComponent<Animator>();
            statsController = GetComponent<StatsController>();
            weaponController = GetComponent<WeaponController>();
            characterAnimationHelper = new CharacterAnimationHelper(animator, transform, leftHandIK, rightHandIK);

            weaponController.OnWeaponEquipped += OnWeaponEquipped;

            statsController.OnDeath += OnDeath;
            statsController.OnTakeDamage += OnTakeDamage;
        }

        public virtual void Attack()
        {
            if(isDeath) return;
            weaponController.Attack();
        }

        public virtual void Die()
        {
            isDeath = true;
            weaponController.StowWeapon();
            characterAnimationHelper.Die();
            leftHandIK.weight = rightHandIK.weight = spineIK.weight = 0;
            OnDeaht?.Invoke(this);
        }

        public virtual void Respawn()
        {
            isDeath = false;
            weaponController.DrawWeapon();
            statsController.Respawned();
            characterAnimationHelper.Respawned();
            statsUI.UpdateStat(StatsType.Armor, statsController.MaxArmor, statsController.MaxArmor);
            statsUI.UpdateStat(StatsType.Health, statsController.MaxHealth, statsController.MaxHealth);
            spineIK.weight = 1;
            if (weaponController.HasWeapon())
                leftHandIK.weight = rightHandIK.weight = 1;
        }

        protected virtual void OnWeaponEquipped(Transform leftHandPos, Transform rightHandPos)
        {
            characterAnimationHelper.AttachHandsToWeapon(leftHandPos, rightHandPos);
        }

        protected virtual void OnTakeDamage(StatsType type, float current, float max)
        {
            statsUI.UpdateStat(type, current, max);
        }

        protected virtual void OnDeath()
        {
            Die();
        }

        //No need for logic for this demo
        public override void ResetObject() 
        { 
            gameObject.SetActive(false);
        }
    }
}
