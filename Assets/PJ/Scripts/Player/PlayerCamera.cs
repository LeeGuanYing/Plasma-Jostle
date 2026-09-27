using UnityEngine;
using UnityEngine.InputSystem;

namespace PokemonJason.Player
{
    public sealed class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActions;
        [SerializeField, Min(0f)] private float sensitivity = 0.08f;
        [SerializeField] private float minPitch = -89f;
        [SerializeField] private float maxPitch = 89f;
        [SerializeField] private bool lockCursorOnStart = true;

        private InputActionMap playerMap;
        private InputAction lookAction;
        private Transform playerBody;
        private float pitch;

        private void Awake()
        {
            playerBody = transform.root;
            if (inputActions == null)
            {
                Debug.LogError("PlayerCamera requires an InputActionAsset.", this);
                enabled = false;
                return;
            }
            playerMap = inputActions.FindActionMap("Player", true);
            lookAction = playerMap.FindAction("Look", true);
        }

        private void Start()
        {
            if (lockCursorOnStart) SetCursorLocked(true);
        }

        private void Update()
        {
            if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
                SetCursorLocked(false);
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame && Cursor.lockState != CursorLockMode.Locked)
                SetCursorLocked(true);
        }

        private void LateUpdate()
        {
            if (Cursor.lockState != CursorLockMode.Locked) return;
            Vector2 look = lookAction.ReadValue<Vector2>();
            playerBody.Rotate(Vector3.up * (look.x * sensitivity));
            pitch = Mathf.Clamp(pitch - look.y * sensitivity, minPitch, maxPitch);
            transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        }

        private static void SetCursorLocked(bool locked)
        {
            Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !locked;
        }
    }
}
