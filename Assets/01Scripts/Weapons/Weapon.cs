using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ActionDemo
{
    public abstract class Weapon : PoolObject
    {
        [SerializeField] protected WeaponSO weaponSO;
        [SerializeField] protected AttachmentType[] acceptedAttachments;
        [SerializeField] private Transform leftHandHoldPoint, rightHandHoldPoint;

        protected float bonusAreaOfEffect = 0f;
        protected float bonusArmorPenetration = 0f;
        protected float bonusDamage = 0f;
        //In many games, melee weapons have range. For example: Warframe.
        //Maybe your melee weapons throws projectiles such as "Black Talon" exotic sword from Destiny 2.
        protected float bonusRange = 0f;

        protected List<AttachmentModel> attachments;

        public Transform LeftHandHoldPoint => leftHandHoldPoint;
        public Transform RightHandHoldPoint => rightHandHoldPoint;

        public bool CanAttach(AttachmentModel attachmentModel)
        {
            return acceptedAttachments.Contains(attachmentModel.AttachmentType) && !attachments.Contains(attachmentModel);
        }

        public void Attach(AttachmentModel attachmentModel)
        {
            //Now you will ask me why I followed such a practice. 
            //In the case study document, each attachment was asked to change only one value of the weapon, but I wanted it to be more modular. 
            //Because the weapon barrel can change the recoil value while changing the damage value of the weapon. 
            //According to the design of the game, it may be desired for the scope to increase the range and increase the damage at the same time.

            attachments.Add(attachmentModel);
            bonusAreaOfEffect += attachmentModel.DamageAttributes.AreaOfEffect;
            bonusArmorPenetration += attachmentModel.DamageAttributes.ArmorPenetration;
            bonusDamage += attachmentModel.DamageAttributes.Damage;
            bonusRange += attachmentModel.DamageAttributes.Range;
        }

        protected float GetTotalAreaOfEffect()
        {
            return weaponSO.DamageAttributes.AreaOfEffect + bonusAreaOfEffect;
        }

        protected float GetTotalArmorPiercing()
        {
            return weaponSO.DamageAttributes.ArmorPenetration + bonusArmorPenetration;
        }

        protected float GetTotalDamage()
        {
            return weaponSO.DamageAttributes.Damage + bonusDamage;
        }

        protected float GetTotalRange()
        {
            return weaponSO.DamageAttributes.Range + bonusRange;
        }

        public WeaponSO GetWeaponSO()
        {
            return weaponSO;
        }

        protected bool IsInitialized = false;

        public abstract void Initialize();
        public abstract void Attack();
    }
}
