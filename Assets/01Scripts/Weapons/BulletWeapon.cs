using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    public class BulletWeapon : Weapon, IShootable
    {
        [Header("Bullet Config")]
        [SerializeField] protected BulletSO BulletSO;

        [Header("Weapon Properties")]
        [SerializeField] private ParticleSystem[] muzzleParticles;
        private float _lastShootTime;

        public override void Initialize()
        {
            if (!IsInitialized)
            {
                IsInitialized = true;
                attachments = new List<AttachmentModel>();
                CreateBulletPool();
            }
        }
        public override void ResetObject()
        {
            gameObject.SetActive(false);
        }

        public override void Attack()
        {
            Shoot();
        }

        public void Shoot()
        {
            if (Time.time >= _lastShootTime + GetWeaponSO().DamageAttributes.FireRate)
            {
                for (int i = 0; i < muzzleParticles.Length; i++)
                {
                    muzzleParticles[i].Play();
                    _lastShootTime = Time.time;
                    Bullet bullet = PoolManager.Instance.GetObject<Bullet>(
                        BulletSO.Name,
                        muzzleParticles[i].transform.position,
                        muzzleParticles[i].transform.eulerAngles
                    );
                    bullet.transform.forward = muzzleParticles[i].transform.forward;
                    bullet.OnDamageDealt += OnDamageDealt;
                    bullet.OnNoImpactTriggered += OnNoImpactTriggered;
                    bullet.Send(BulletSO.MoveSpeed, GetTotalRange(), GetTotalAreaOfEffect());
                }
            }
        }

        protected void CreateBulletPool()
        {
            PoolManager.Instance.CreatePool<Bullet>(BulletSO.name, BulletSO.BulletPrefab, 10);
        }

        private void ReturnBulletToPool(Bullet bullet)
        {
            bullet.OnDamageDealt -= OnDamageDealt;
            bullet.OnNoImpactTriggered -= OnNoImpactTriggered;
            PoolManager.Instance.ReturnObject<Bullet>(BulletSO.Name, bullet);
        }

        private void OnDamageDealt(List<DamageDealtConfig> damageDealtConfigs, Bullet bullet)
        {
            foreach (DamageDealtConfig item in damageDealtConfigs)
            {
                if (item.DamagedCollider.TryGetComponent<IDamagable>(out IDamagable damagable))
                {
                    damagable.TakeDamage(GetTotalDamage(), GetTotalArmorPiercing());
                }
            }
            ReturnBulletToPool(bullet);
        }

        private void OnNoImpactTriggered(Bullet bullet)
        {
            ReturnBulletToPool(bullet);
        }
    }
}
