using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;

        private void Update()
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
    }
}
