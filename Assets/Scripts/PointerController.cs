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
                InteractableGameObject interactableGameObject = GetInteractableObject();
                _startMousePosition = Input.mousePosition;
                _isDragging = true;
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
            var hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), _camera.transform.forward, 3f);
        }
    }
}