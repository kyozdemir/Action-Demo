using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ActionDemo
{
    [DisallowMultipleComponent]
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private int slotCount;
        [SerializeField] private List<WeaponSlot> weaponSlots;
        [SerializeField] private Transform gunParent;

        public event Action<Transform, Transform> OnWeaponEquipped;

        private WeaponSlot _selectedSlot = null;
        public Weapon CurrentWeapon => _selectedSlot.Weapon;

        public bool CanAttachCurrentWeapon(AttachmentModel attachmentModel)
        {
            if (_selectedSlot is not null && _selectedSlot.Weapon is not null)
                return _selectedSlot.Weapon.CanAttach(attachmentModel);

            return false;
        }

        public bool CanCollectWeapon(WeaponSO weaponSO)
        {
            return weaponSlots.Count < slotCount && !weaponSlots.Any(x => x.Weapon.GetWeaponSO().WeaponIdentity.Name == weaponSO.WeaponIdentity.Name); 
        }

        public bool HasWeapon()
        {
            return _selectedSlot is not null && _selectedSlot.Weapon is not null;
        }

        public void AttachCurrentWeapon(AttachmentModel attachmentModel)
        {
            _selectedSlot.Weapon.Attach(attachmentModel);
        }

        public void EquipWeapon(WeaponSO weaponSO)
        {
            if (weaponSlots.Count == slotCount) return;
            if (weaponSlots.Any(x => x.Weapon.GetWeaponSO().WeaponIdentity.Name == weaponSO.WeaponIdentity.Name)) return;

            if (weaponSlots.Count < slotCount)
                weaponSlots.Add(new WeaponSlot());

            WeaponSlot firstAvaliableSlot = weaponSlots.FirstOrDefault(x => x.Weapon is null);

            if (firstAvaliableSlot is not null)
            {
                firstAvaliableSlot.Weapon = PoolManager.Instance.GetObject<Weapon>(weaponSO.WeaponIdentity.Name);
                firstAvaliableSlot.Weapon.transform.SetParent(gunParent);
                firstAvaliableSlot.Weapon.transform.SetLocalPositionAndRotation(
                    weaponSO.WeaponIdentity.localPosition,
                    Quaternion.Euler(weaponSO.WeaponIdentity.localRotation)
                );
                SelectWeapon(weaponSlots.IndexOf(firstAvaliableSlot));
            }
        }

        public void StowWeapon()
        {
            if (HasWeapon())
            {
                _selectedSlot.Weapon.gameObject.SetActive(false);
            }
        }

        public void DrawWeapon()
        {
            if (HasWeapon())
            {
                _selectedSlot.Weapon.gameObject.SetActive(true);
            }
        }

        public void SelectWeapon(int index)
        {
            if (index >= 0 && index < weaponSlots.Count)
            {
                StowWeapon();
                _selectedSlot = weaponSlots[index];
                DrawWeapon();
                _selectedSlot.Weapon.Initialize();
                OnWeaponEquipped?.Invoke(
                    _selectedSlot.Weapon.LeftHandHoldPoint,
                    _selectedSlot.Weapon.RightHandHoldPoint
                );
            }
        }

        public void Attack()
        {
            if (!HasWeapon()) return;
            _selectedSlot.Weapon.Attack();
        }
    }

    [System.Serializable]
    public class WeaponSlot
    {
        public Weapon Weapon;
    }
}
