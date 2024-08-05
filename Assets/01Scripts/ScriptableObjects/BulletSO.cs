using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    [CreateAssetMenu (fileName = "BulletSO", menuName = "Bullet", order = 0)]
    //The game designer may want to add a new bullet type.
    //Since he/she is not asked to write code, we should provide him/her with easy solutions.
    //He/she can add as many new bullets as he/she wants and attach them to weapons.
    public class BulletSO : ScriptableObject
    {
        [Header("Bullet Attributes")]
        public string Name;
        public Bullet BulletPrefab;
        public float MoveSpeed;

        private DamageAttributes _damageAttributesFromWeapon;
        public DamageAttributes DamageAttributesFromWeapon => _damageAttributesFromWeapon;

        public void SetDamageAttributes(DamageAttributes damageAttibutesModel)
        {
            _damageAttributesFromWeapon = damageAttibutesModel;
        }
    }
}
