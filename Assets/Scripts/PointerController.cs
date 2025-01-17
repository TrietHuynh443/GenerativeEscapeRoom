using Interface.Services;
using UnityEngine;
namespace DI
{
    public class PointerController : MonoService
    {
        private Vector2 _startMousePosition;
        private bool _isDragging = false;
        private InteractableGameObject _interactableGameObject;
        private Camera _camera;
        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
            {
                _interactableGameObject = GetInteractableObject();
                if (_interactableGameObject != null)
                {
                    _startMousePosition = Input.mousePosition;
                    _isDragging = true;
                }
            }
            else if (Input.GetMouseButtonUp(0)) // Left mouse button released
            {
                _isDragging = false;
            }

            if (_isDragging)
            {
                Vector2 currentMousePosition = Input.mousePosition;

                // Call the DoRotate method
                _interactableGameObject.DoRotate(_startMousePosition, currentMousePosition);

                // Update the start position for continuous rotation
                _startMousePosition = currentMousePosition;
            }
        }

        private InteractableGameObject GetInteractableObject()
        {
            // Cast a ray from the camera to the mouse position
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // Draw the ray in the scene view for debugging
                Debug.DrawLine(ray.origin, hit.point, Color.red, 0.2f);

                // Debug log the hit object
                Debug.Log($"Hit: {hit.collider.name}");

                // Return the InteractableGameObject component if the object is interactable
                return hit.collider.GetComponent<InteractableGameObject>();
            }

            // If no object is hit, draw the ray to infinity
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100f, Color.red, 0.2f);
            Debug.Log("No hit detected");

            return null;
        }
    }
}