using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    [RequireComponent(typeof(Bullet))]
    public class BulletImpactParticlePlayer : MonoBehaviour
    {
        [SerializeField] private ParticleSO particleSO;
        private Bullet _bullet;

        private void OnDamageDealth(List<DamageDealtConfig> list, Bullet bullet)
        {
            Play();
        }

        private void Play()
        {
            PoolParticleObject poolParticleObject = PoolManager.Instance.GetObject<PoolParticleObject>(particleSO.Name, transform.position);
            poolParticleObject.OnParticleFinished += OnParticleFinished;
        }

        private void OnParticleFinished(PoolParticleObject poolParticleObject)
        {
            poolParticleObject.OnParticleFinished -= OnParticleFinished;
            PoolManager.Instance.ReturnObject<PoolParticleObject>(particleSO.Name, poolParticleObject);
        }

        private void OnEnable()
        {
            if (!PoolManager.Instance.ContainsKey(particleSO.Name))
            {
                _bullet = GetComponent<Bullet>();
                PoolManager.Instance.CreatePool<PoolParticleObject>(particleSO.Name, particleSO.PoolParticleObject, 1);
                _bullet.OnDamageDealt += OnDamageDealth;
            }
        }
    }
}
