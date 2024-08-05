using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    [RequireComponent(typeof(StatsController))]
    public class ShowDamageNumbers : MonoBehaviour
    {
        [SerializeField] private DamageNumbers damageNumbersPrefab;
        private StatsController _statsController;

        // Start is called before the first frame update
        void Start()
        {
            _statsController = GetComponent<StatsController>();
            PoolManager.Instance.CreatePool<DamageNumbers>(Constants.PoolKeys.DAMAGE_NUMBERS, damageNumbersPrefab, 1);
            _statsController.OnTotalDamage += OnTotalDamage;
        }

        private void OnTotalDamage(float armorDamage, float healthDamage)
        {
            DamageNumbers damageNumbers = PoolManager.Instance.GetObject<DamageNumbers>(
                Constants.PoolKeys.DAMAGE_NUMBERS,
                transform.position - (Vector3.forward * .5f) + Vector3.up
            );
            damageNumbers.SetValues(armorDamage, healthDamage);
            damageNumbers.PlayAnimation();
        }

        private void OnDisable()
        {
            if (_statsController is not null)
                _statsController.OnTotalDamage -= OnTotalDamage;
        }
    }
}
