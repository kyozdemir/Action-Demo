using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    public class Follower : MonoBehaviour
    {
        public Transform transformToFollow;

        private void Update()
        {
            if (transformToFollow == null) return;

            transform.position = transformToFollow.position;
        }
    }
}
