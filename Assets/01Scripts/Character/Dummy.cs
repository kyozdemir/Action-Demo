using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace ActionDemo
{
    public class Dummy : MonoBehaviour
    {
        [SerializeField] private StatsController statsController;
        [SerializeField] private StatsUI statsUI;

        void Start()
        {
            statsController.OnTakeDamage += OnTakeDamage;
        }

        private void OnTakeDamage(StatsType type, float current, float max)
        {
            statsUI.UpdateStat(type, current, max);
        }

        private void OnDisable()
        {
            statsController.OnTakeDamage -= OnTakeDamage;
        }
    }
}
