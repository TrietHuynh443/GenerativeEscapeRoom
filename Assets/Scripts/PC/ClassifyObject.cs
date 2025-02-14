using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassifyObject : MonoBehaviour
{
    public bool completed = false;
    public AudioClip push;
    public AudioClip incorrect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bucket")
        {
            Debug.Log("Bucket");
            if (other.name == name)
            {
                Debug.Log("Correct");
                completed = true;
                AudioSource.PlayClipAtPoint(push, Vector3.zero, 1.0f);
            }
            else
            {
                completed = false;
                Debug.Log("Incorrect");
                AudioSource.PlayClipAtPoint(incorrect, Vector3.zero, 1.0f);
            }
        }

        completed = false;
    }

}
