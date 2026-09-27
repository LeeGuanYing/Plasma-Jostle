using UnityEngine;
using UnityEngine.InputSystem;

namespace PokemonJason.Player
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class PlayerController : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField] private InputActionAsset inputActions;
        [Header("Movement")]
        [SerializeField, Min(0f)] private float walkSpeed = 5f;
        [SerializeField, Min(0f)] private float crouchSpeed = 2.5f;
        [SerializeField, Min(0f)] private float jumpHeight = 1.2f;
        [SerializeField] private float gravity = -20f;
        [Header("Crouch")]
        [SerializeField, Min(0.1f)] private float standingHeight = 1.8f;
        [SerializeField, Min(0.1f)] private float crouchingHeight = 1.1f;
        [SerializeField, Min(0f)] private float crouchTransitionSpeed = 12f;
        [SerializeField] private Transform cameraRoot;
        [SerializeField] private float standingCameraHeight = 1.62f;
        [SerializeField] private float crouchingCameraHeight = 0.95f;

        private CharacterController characterController;
        private InputActionMap playerMap;
        private InputAction moveAction;
        private InputAction jumpAction;
        private InputAction crouchAction;
        private Vector3 verticalVelocity;
        private bool crouchRequested;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            if (inputActions == null)
            {
                Debug.LogError("PlayerController requires an InputActionAsset.", this);
                enabled = false;
                return;
            }
            playerMap = inputActions.FindActionMap("Player", true);
            moveAction = playerMap.FindAction("Move", true);
            jumpAction = playerMap.FindAction("Jump", true);
            crouchAction = playerMap.FindAction("Crouch", true);
            characterController.height = standingHeight;
            characterController.center = new Vector3(0f, standingHeight * 0.5f, 0f);
        }

        private void OnEnable()
        {
            if (playerMap == null) return;
            playerMap.Enable();
            jumpAction.performed += OnJumpPerformed;
            crouchAction.started += OnCrouchStarted;
            crouchAction.canceled += OnCrouchCanceled;
        }

        private void OnDisable()
        {
            if (playerMap == null) return;
            jumpAction.performed -= OnJumpPerformed;
            crouchAction.started -= OnCrouchStarted;
            crouchAction.canceled -= OnCrouchCanceled;
            playerMap.Disable();
        }

        private void Update()
        {
            UpdateCrouch();
            Move();
        }

        private void Move()
        {
            Vector2 input = moveAction.ReadValue<Vector2>();
            Vector3 move = Vector3.ClampMagnitude(transform.right * input.x + transform.forward * input.y, 1f);
            float speed = crouchRequested ? crouchSpeed : walkSpeed;
            characterController.Move(move * (speed * Time.deltaTime));
            if (characterController.isGrounded && verticalVelocity.y < 0f)
                verticalVelocity.y = -2f;
            characterController.Move(verticalVelocity * Time.deltaTime);
            verticalVelocity.y += gravity * Time.deltaTime;
        }

        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            if (!characterController.isGrounded || crouchRequested) return;
            verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        private void OnCrouchStarted(InputAction.CallbackContext context) => crouchRequested = true;
        private void OnCrouchCanceled(InputAction.CallbackContext context) => crouchRequested = false;

        private void UpdateCrouch()
        {
            float targetHeight = crouchRequested ? crouchingHeight : standingHeight;
            characterController.height = Mathf.MoveTowards(characterController.height, targetHeight, crouchTransitionSpeed * Time.deltaTime);
            characterController.center = new Vector3(0f, characterController.height * 0.5f, 0f);
            if (cameraRoot == null) return;
            Vector3 localPosition = cameraRoot.localPosition;
            float targetCameraHeight = crouchRequested ? crouchingCameraHeight : standingCameraHeight;
            localPosition.y = Mathf.MoveTowards(localPosition.y, targetCameraHeight, crouchTransitionSpeed * Time.deltaTime);
            cameraRoot.localPosition = localPosition;
        }

        public bool IsCrouching => crouchRequested;
    }
}
