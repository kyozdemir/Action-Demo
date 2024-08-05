using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    public interface IDamagable
    {
        public float CurrentArmor {get;}
        public float MaxArmor {get;}
        public float CurrentHealth { get; }
        public float MaxHealth {get;}

        public event Action<StatsType, float, float> OnTakeDamage;
        public event Action<float, float> OnTotalDamage;
        public event Action OnDeath; 

        public void TakeDamage(float totalDamage, float armorPiercing);
        public void ApplyDamageToArmor(float damage);
        public void ApplyDamageToHealth(float damage);
    }
}
