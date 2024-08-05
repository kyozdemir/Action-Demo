using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    public class KeepRotationStill : MonoBehaviour
    {
        [SerializeField] private Vector3 rotation;

        // Update is called once per frame
        void Update()
        {
            transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
