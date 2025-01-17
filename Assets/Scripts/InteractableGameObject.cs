using DG.Tweening;
using UnityEngine;

public class InteractableGameObject : MonoBehaviour
{
    // Rotate using DOTween when the mouse is dragged
    public void DoRotate(Vector2 originMousePosition, Vector2 targetMousePosition)
    {
        Vector2 delta = targetMousePosition - originMousePosition;

        float sensitivity = 0.1f;
        float rotationX = delta.y * sensitivity;
        float rotationY = -delta.x * sensitivity;

        Vector3 targetRotation = transform.eulerAngles + new Vector3(rotationX, rotationY, 0);

        transform.DORotate(targetRotation, 0.2f);
    }
}