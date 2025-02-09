using EventProcessing;
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
        private bool _isMove;
        private float _horizontalInput;
        private float _verticalInput;
        private InteractableGameObject _curInteractObject;
        private int _curMousePressed = -1;

        [Injector]
        private readonly IEventHandlerService _eventAggregateService;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _curMousePressed = 0;//rotate
            }
            else if (Input.GetMouseButtonDown(1))
            {
                _curMousePressed = 1;
            }
            // else
            // {
            //     _curMousePressed = -1;
            // }
            if (_curMousePressed >= 0 && !_isDragging) // mouse pressed
            {
                _interactableGameObject = GetInteractableObject();
                if (_interactableGameObject != null)
                {
                    _camera.gameObject.GetComponent<CameraMoveHandler>().enabled = false;
                    _startMousePosition = Input.mousePosition;
                    _isDragging = true;
                }
                _eventAggregateService.RaiseEvent(new OnDraggingInteractableObjEvent()
                {
                    InteractableObj = _interactableGameObject,
                });
            }
            else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) // Left mouse button released
            {
                _isDragging = false;
                _curMousePressed = -1;
                _camera.GetComponent<CameraMoveHandler>().enabled = true;
            }
            
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
            _isMove = Mathf.Abs(_horizontalInput) >= 0.1f || Mathf.Abs(_verticalInput) >=  0.1f;
  
            if (_isDragging)
            {
                Vector2 currentMousePosition = Input.mousePosition;
                
                // Call the DoRotate method
                if (_curMousePressed == 0)
                {
                    _interactableGameObject.DoRotate(_startMousePosition, currentMousePosition);
                }
                else
                {
                    Vector3 GetMouseWorldPosition()
                    {
                        Vector3 mousePos = Input.mousePosition;
                        mousePos.z = _camera.WorldToScreenPoint(_interactableGameObject.transform.position).z; // Maintain depth
                        return _camera.ScreenToWorldPoint(mousePos);
                    }
                    _interactableGameObject.DoMove(GetMouseWorldPosition());
                }
                // Update the start position for continuous rotation
                _startMousePosition = currentMousePosition;
            }
            
        }

        private InteractableGameObject GetInteractableObject()
        {
            // Cast a ray from the camera to the mouse position
            if (_camera == null)
            {
                _camera = Camera.current;
            }

            if (_camera != null)
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                
                // Perform the raycast
                if (Physics.Raycast(ray, out hit, _camera.farClipPlane))
                {

                    // Return the InteractableGameObject component if the object is interactable
                    return hit.collider.GetComponent<InteractableGameObject>();
                }
            }

            return null;
        }
    }
}