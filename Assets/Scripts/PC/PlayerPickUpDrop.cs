using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Platform;
using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform playerCamTransform;
    [SerializeField] private Transform grabPoint;
    [SerializeField] private float pickUpRange = 1.3f;
    [SerializeField] private float buttonHitRange = 1.3f;
    [SerializeField] private LayerMask pickUpLayer;
    [SerializeField] private LayerMask buttonLayer;

    private ObjectGrabbable objectGrabbable;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectGrabbable != null)
            {
                DropObject();
            }
            else
            {
                PickUp();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("Space pressed");
            HitButton();
        }
    }

    private void HitButton()
    {
        if (Physics.Raycast(playerCamTransform.position, playerCamTransform.forward, out RaycastHit hit, buttonHitRange, buttonLayer))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.TryGetComponent(out ButtonBase btn))
            {
                Debug.Log("Button hit");
                btn.Activate();
            }
        }
    }

    private void PickUp()
    {
        if (Physics.Raycast(playerCamTransform.position, playerCamTransform.forward, out RaycastHit hit, pickUpRange, pickUpLayer))
        {
            if (hit.transform.TryGetComponent(out objectGrabbable))
            {
                Debug.Log("Picking up");
                objectGrabbable.Grab(grabPoint);
            }
        }
    }

    private void DropObject()
    {
        objectGrabbable.Drop();
        objectGrabbable = null;
    }
}
