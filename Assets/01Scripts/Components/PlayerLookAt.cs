using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    public class PlayerLookAt : ILookAt
    {
        private float _rotateSpeed;
        private Transform _transform;
        private Vector3 _direction;

        public PlayerLookAt(float rotateSpeed, Transform transform)
        {
            _rotateSpeed = rotateSpeed;
            _transform = transform;
        }

        public void LookAtDirection(Vector3 direction)
        {
            direction.y = _transform.position.y;
            _direction = direction - _transform.position;

           /* _transform.rotation = Quaternion.Slerp(
                _transform.rotation,
                Quaternion.LookRotation(direction),
                Time.deltaTime * _rotateSpeed
            );*/

            _transform.rotation = Quaternion.Euler(
                0,
                Quaternion.LookRotation(_direction).eulerAngles.y,
                0
            );
        }
    }
}
