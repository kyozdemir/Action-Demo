using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : PoolObject
    {
        [SerializeField] private TrailRenderer trailRenderer;

        private bool _isMovementEnabled = false;
        private float _moveSpeed, _range;
        private Vector3 _startPosition;
        protected float areaOfEffect;
        protected List<DamageDealtConfig> damageDealtConfigs = new List<DamageDealtConfig>();

        //DamageTypes is useful for logging etc.
        public Action<List<DamageDealtConfig>, Bullet> OnDamageDealt;
        public event Action<Bullet> OnNoImpactTriggered;

        public void Send(float moveSpeed, float range, float areaOfEffect)
        {
            this.areaOfEffect = areaOfEffect;
            _moveSpeed = moveSpeed;
            _range = range;
            _startPosition = transform.position;
            _isMovementEnabled = true;
        }

        public override void ResetObject()
        {
            damageDealtConfigs.Clear();
            trailRenderer.Clear();
        }

        protected virtual void Impacted(Collider other)
        {
            _isMovementEnabled = false;
            damageDealtConfigs.Add(new DamageDealtConfig(DamageTypes.Impact, other));
            OnDamageDealt?.Invoke(damageDealtConfigs, this);
        }

        private void Update()
        {
            if (!_isMovementEnabled) return;

            if ((transform.position - _startPosition).sqrMagnitude < _range)
            {
                transform.Translate(transform.forward * _moveSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                _isMovementEnabled = false;
                OnNoImpactTriggered?.Invoke(this);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Impacted(other);
        }
    }

    [System.Serializable]
    public struct DamageDealtConfig
    {
        public DamageTypes DamageType;
        public Collider DamagedCollider;

        public DamageDealtConfig(DamageTypes damageTypes, Collider damagedCollider)
        {
            DamageType = damageTypes;
            DamagedCollider = damagedCollider;
        }
    }
}
