using UnityEngine;
using UnityEngine.InputSystem;

namespace PokemonJason.Player
{
    public sealed class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActions;
        [SerializeField] private Camera playerCamera;
        [SerializeField, Min(0.1f)] private float interactionRange = 3f;
        [SerializeField] private LayerMask interactionLayers = ~0;

        private InputAction interactAction;

        private void Awake()
        {
            if (playerCamera == null) playerCamera = GetComponentInChildren<Camera>();
            if (inputActions == null)
            {
                Debug.LogError("PlayerInteractor requires an InputActionAsset.", this);
                enabled = false;
                return;
            }
            interactAction = inputActions.FindActionMap("Player", true).FindAction("Interact", true);
        }

        private void OnEnable()
        {
            if (interactAction != null) interactAction.performed += OnInteractPerformed;
        }

        private void OnDisable()
        {
            if (interactAction != null) interactAction.performed -= OnInteractPerformed;
        }

        private void OnInteractPerformed(InputAction.CallbackContext context)
        {
            if (TryGetInteractionTarget(out RaycastHit hit))
                Debug.Log($"Interact target: {hit.collider.name}", hit.collider);
        }

        public bool TryGetInteractionTarget(out RaycastHit hit)
        {
            hit = default;
            if (playerCamera == null) return false;
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            return Physics.Raycast(ray, out hit, interactionRange, interactionLayers, QueryTriggerInteraction.Ignore);
        }
    }
}
