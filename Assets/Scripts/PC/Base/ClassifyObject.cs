using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClassifyObject : MonoBehaviour
{
    public bool completed = false;
    public AudioClip push;
    public AudioClip incorrect;

    protected abstract void OnTriggerEnter(Collider other);
    // {
    //     if (other.tag == "Bucket")
    //     {
    //         Debug.Log("Bucket");
    //         if (other.name == name)
    //         {
    //             completed = true;
    //             AudioSource.PlayClipAtPoint(push, Vector3.zero, 1.0f);
    //         }
    //         else
    //         {
    //             completed = false;
    //             AudioSource.PlayClipAtPoint(incorrect, Vector3.zero, 1.0f);
    //         }
    //     } else 
    //     {
    //         completed = false;
    //     }
    // }
}
