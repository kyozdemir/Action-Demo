using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    [CreateAssetMenu(fileName = "WeaponSO", menuName = "Weapon", order = 0)]
    public class WeaponSO : ScriptableObject
    {
        [Header("Weapon Identity")]
        public WeaponIdentity WeaponIdentity;

        [Header("Damage Attributes")]
        public DamageAttributes DamageAttributes;
    }

    [System.Serializable]
    public struct DamageAttributes
    {
        [Range(0, 100)]
        public float ArmorPenetration;
        public float AreaOfEffect, Damage, FireRate, Range;
    }

    [System.Serializable]
    public struct WeaponIdentity
    {
        public string Name;
        public Vector3 localPosition, localRotation;
    }
}
