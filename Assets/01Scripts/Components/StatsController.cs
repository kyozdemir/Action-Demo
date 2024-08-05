using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    [DisallowMultipleComponent]
    public class StatsController : MonoBehaviour, IDamagable
    {
        [SerializeField] private float _maxArmor, _maxHealth;
        private float _currentArmor, _currentHealth;

        public float CurrentArmor => _currentArmor;
        public float MaxArmor => _maxArmor;
        public float CurrentHealth => _currentHealth;
        public float MaxHealth => _maxHealth;

        public event Action<StatsType, float, float> OnTakeDamage;
        public event Action OnDeath;
        public event Action<float, float> OnTotalDamage;

        public void ApplyDamageToArmor(float damage)
        {
            _currentArmor = Mathf.Clamp(_currentArmor - damage, 0, _maxArmor);
            OnTakeDamage?.Invoke(StatsType.Armor, _currentArmor, _maxArmor);
        }

        public void ApplyDamageToHealth(float damage)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
            OnTakeDamage?.Invoke(StatsType.Health, _currentHealth, _maxHealth);

            if (CurrentHealth == 0 && damage > 0)
            {
                OnDeath?.Invoke();
            }
        }

        public void TakeDamage(float totalDamage, float armorPiercing)
        {
            float damageToArmor = 0f, damageToHealth = 0f;
            if (_currentArmor > 0)
            {
                damageToHealth = totalDamage * (armorPiercing * 0.01f);
                damageToArmor = totalDamage - damageToHealth;
            }
            else
            {
                damageToHealth = totalDamage;
            }

            ApplyDamageToArmor(damageToArmor);
            ApplyDamageToHealth(damageToHealth);
            OnTotalDamage?.Invoke(damageToArmor, damageToHealth);
        }

        public void Respawned()
        {
            _currentArmor = _maxArmor;
            _currentHealth = _maxHealth;
        }

        private void OnEnable()
        {
            Respawned();   
        }

    }
}
