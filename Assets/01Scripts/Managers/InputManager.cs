using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionDemo
{
    public class InputManager : MonoBehaviour
    {
        #region Singleton

        public static InputManager Instance { get; private set; }

        #endregion Singleton

        [SerializeField] private LayerMask layerMask;

        private PlayerControl playerControl;
        private RaycastHit _raycastHit;
        private Vector3 _mousePosition;

        public event Action<int> OnWeaponSelectionPressed;
        public event Action OnWeaponFireInput;
        public event Action<Vector2> OnMovementInputChanged;
        public event Action<Vector3> OnMouseWorldPositionChanged;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            playerControl = new PlayerControl();
            //Assing the weapon switching input
            playerControl.Weapon.Switch.performed += ctx => OnWeaponSelectionPressed?.Invoke(int.Parse(ctx.control.name));
        }


        private void Update()
        {
            HandleMovementInput();
            HandleMouseInput();
            HandleFireInput();
        }

        private void HandleFireInput()
        {
            if (playerControl.Weapon.Fire.ReadValue<float>() > 0.1f)
            {
                OnWeaponFireInput?.Invoke();
            }
        }

        private void HandleMouseInput()
        {
            //Get mouse position from input system
            _mousePosition = playerControl.Mouse.MousePosition.ReadValue<Vector2>();
            if (Physics.Raycast(CameraManager.Instance.MainCamera.ScreenPointToRay(_mousePosition), out _raycastHit, Mathf.Infinity, layerMask))
            {
                OnMouseWorldPositionChanged?.Invoke(_raycastHit.point);
            }
        }

        private void HandleMovementInput()
        {
            OnMovementInputChanged?.Invoke(playerControl.Movement.Move.ReadValue<Vector2>());
        }

        private void OnEnable()
        {
            playerControl.Weapon.Enable();
            playerControl.Movement.Enable();
            playerControl.Mouse.Enable();
        }

        private void OnDisable()
        {
            playerControl.Weapon.Enable();
            playerControl.Movement.Disable();
            playerControl.Mouse.Disable();
        }
    }
}
