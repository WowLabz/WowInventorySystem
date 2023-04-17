using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WowInventorySystem
{
    public class CustomPlayerInputs : MonoBehaviour
    {
        [SerializeField] private InputActionAsset customInputActions;
        private InputActionMap _uiActionMap;
        private InputAction _toggleInventoryOpen;
        private bool _isInventoryActive;

        //Input Events being broadcast
        public static event Action<bool> OnRequestToggleInventoryCanvas;
        private void Awake()
        {
            _uiActionMap = customInputActions.FindActionMap("UI");
            _toggleInventoryOpen = _uiActionMap.FindAction("Inventory");
            _toggleInventoryOpen.performed += ctx => RequestToggleInventoryCanvas();
        }
    
        private void OnEnable() => _uiActionMap.Enable();
        private void OnDisable() => _uiActionMap.Disable();

        public void RequestToggleInventoryCanvas()
        {
            // Your custom action logic here
            Debug.Log("Custom UI action performed!");
            _isInventoryActive = !_isInventoryActive;
            OnRequestToggleInventoryCanvas?.Invoke(_isInventoryActive);
            SetCursorState(!_isInventoryActive); //Basically inversely sets mouse active when canvas is on
        }
        private void SetCursorState(bool newState) => Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        
    }
}