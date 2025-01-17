using UnityEngine;

public class InteractableGameObject : MonoBehaviour
{
    private Vector2 startMousePosition;
    private bool isDragging = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        {
            startMousePosition = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0)) // Left mouse button released
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector2 currentMousePosition = Input.mousePosition;
            GetComponent<InteractableGameObject>().DoRotate(startMousePosition, currentMousePosition);
            startMousePosition = currentMousePosition; // Update for continuous rotation
        }
    }
}