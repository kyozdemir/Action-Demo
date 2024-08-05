using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    [RequireComponent(typeof(SphereCollider))]
    [DisallowMultipleComponent]
    public class PlayerDetector : MonoBehaviour
    {
        private SphereCollider sphereCollider;

        public event Action<Character> OnPlayerDetected;

        public void Initialize()
        {
            if (sphereCollider is null)
                sphereCollider = GetComponent<SphereCollider>();
        }

        public void ChangeRadius(float radius)
        {
            sphereCollider.radius = radius;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Tags.PLAYER))
            {
                OnPlayerDetected?.Invoke(other.GetComponent<Character>());
            }
            
        }
    }
}
