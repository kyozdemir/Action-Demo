using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace ActionDemo
{
    public class CameraManager : MonoBehaviour
    {
        #region Singleton

        public static CameraManager Instance { get; private set; }

        #endregion Singleton

        [SerializeField] private CinemachineVirtualCamera mainVirtualCamera;
        private Camera _mainCamera;
        public Camera MainCamera => _mainCamera;
        public CinemachineVirtualCamera MainVirtualCamera => mainVirtualCamera;

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
        }

        private void Start()
        {
            _mainCamera = Camera.main;
        }
    }
}
