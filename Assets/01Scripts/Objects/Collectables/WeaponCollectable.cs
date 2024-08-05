using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    public class WeaponCollectable : CollectableBase
    {
        [SerializeField] private WeaponSO weaponSO;
        private Weapon currentWeapon;

        public override ScriptableObject CollectableData { get => weaponSO; set => weaponSO = (WeaponSO)value; }

        public override void Collect(PlayerController player)
        {
            if (player.CanCollectWeapon(weaponSO))
            {
                player.CollectWeapon(currentWeapon.GetWeaponSO());
                gameObject.SetActive(false);
            }
        }

        public override void Setup()
        {
            currentWeapon = PoolManager.Instance.GetObject<Weapon>(weaponSO.WeaponIdentity.Name, Vector3.zero, Vector3.zero, transform);
        }

        public override void OnDestroy()
        {
            PoolManager.Instance.ReturnObject<Weapon>(weaponSO.WeaponIdentity.Name, currentWeapon);
            currentWeapon = null;
        }
    }
}
