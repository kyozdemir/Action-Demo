using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace ActionDemo
{
    public class CharacterAnimationHelper
    {
        private Animator _animator;
        private TwoBoneIKConstraint _leftHandIK, _rightHandIK;
        private Vector3 _movementDirection;
        private Transform _transform;

        public CharacterAnimationHelper(Animator animator, Transform transform, TwoBoneIKConstraint leftHandIK, TwoBoneIKConstraint rightHandIK)
        {
            _animator = animator;
            _transform = transform;
            _leftHandIK = leftHandIK;
            _rightHandIK = rightHandIK;
        }

        public void ApplyMovementAnimation(Vector2 dir)
        {
            _animator.SetFloat(Constants.Animations.LOCOMOTION_X, dir.x);
            _animator.SetFloat(Constants.Animations.LOCOMOTION_Y, dir.y);
        }

        public void AttachHandsToWeapon(Transform leftHandTarget, Transform rightHandTarget)
        {
            if (leftHandTarget is not null)
            {
                _leftHandIK.data.target.position = leftHandTarget.position;
                _leftHandIK.data.target.rotation = leftHandTarget.rotation;
                _leftHandIK.weight = 1f;
            }
            if (rightHandTarget is not null)
            {
                _rightHandIK.data.target.position = rightHandTarget.position;
                _rightHandIK.data.target.rotation = rightHandTarget.rotation;
                _rightHandIK.weight = 1f;
            }
        }

        public void Die()
        {
            _animator.SetBool(Constants.Animations.IS_DEATH, true);
        }

        public void Respawned()
        {
            _animator.SetBool(Constants.Animations.IS_DEATH, false);
        }
    }
}
