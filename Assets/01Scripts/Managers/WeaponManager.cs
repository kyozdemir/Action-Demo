using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] private Weapon[] weapons;

        public static WeaponManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            foreach (Weapon item in weapons)
            {
                PoolManager.Instance.CreatePool<Weapon>(item.GetWeaponSO().WeaponIdentity.Name, item, 1);
            }
        }
    }
}
