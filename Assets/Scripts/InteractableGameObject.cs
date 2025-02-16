using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public enum ECategoryType
{
    None = 0,
    Class = 1,
    Color = 2,
    Direction = 3,
    Size = 4,
}

public class InteractableGameObject : MonoBehaviour
{
    // Rotate using DOTween when the mouse is dragged
    [SerializeField] [Range(0f,1f)]private float _sensitivity = 1f;
    private Dictionary<ECategoryType, string> _categoryMap = new();
    

    public void DoRotate(Vector2 originMousePosition, Vector2 targetMousePosition)
    {
        Vector2 delta = targetMousePosition - originMousePosition;

        
        float rotationX = delta.y * _sensitivity;
        float rotationY = -delta.x * _sensitivity;

        Vector3 targetRotation = transform.eulerAngles + new Vector3(rotationX, rotationY, 0);

        transform.DORotate(targetRotation, 0.2f);
    }

    private void OnEnable()
    {
    }

    public void SetConfig(ECategoryType categoryType, string value)
    {
        Debug.Log($"SetConfig {categoryType} {value} {gameObject.name}");
        _categoryMap.TryAdd(categoryType, value);
        _categoryMap[categoryType] = value;
    }

    public string GetConfig(ECategoryType categoryType)
    {
        return _categoryMap.GetValueOrDefault(categoryType, string.Empty);
    }

    public void DoMove(Vector3 worldPos)
    {
        transform.DOMove(worldPos, 0.2f);
    }
}