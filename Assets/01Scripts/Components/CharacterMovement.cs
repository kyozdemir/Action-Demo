using UnityEngine;

namespace ActionDemo
{
    public class CharacterMovement
    {
        private bool _isMovementEnabled = true;
        private CharacterController _characterController;
        private Vector3 _movementDirection = Vector3.zero;

        public CharacterMovement(CharacterController characterController)
        {
            _characterController = characterController;
        }

        //Change movement state
        public void SetMovementState(bool state)
        {
            _isMovementEnabled = state;
        }

        public void MoveTowards(Vector3 direction, float moveSpeed)
        {
            if(!_isMovementEnabled) return;

            //No need for "new" operation. causes load
            _movementDirection.x = direction.x;
            _movementDirection.z = direction.y;

            //Move the character controller
            _characterController.SimpleMove(_movementDirection * moveSpeed);
        }
    }
}
