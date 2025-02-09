using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody rb;
    private Transform grabPoint;
    [SerializeField] private float lerpSpeed = 15f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }   

    public void Grab(Transform GrabPoint){
        this.grabPoint = GrabPoint;
        // Debug.Log(grabPoint);
        rb.useGravity = false;
    }

    public void Drop(){
        this.grabPoint = null;
        rb.useGravity = true;
    }

    private void FixedUpdate() {
        if (grabPoint != null) {
            // Debug.Log("Grabbing 2");
            Vector3 newPos = Vector3.Lerp(rb.position, grabPoint.position, lerpSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            // rb.MoveRotation(grabPoint.rotation);
        }
    }
}
