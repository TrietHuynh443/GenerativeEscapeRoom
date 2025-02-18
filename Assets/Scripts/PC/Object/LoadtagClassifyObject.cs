using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Meta.WitAi;
using UnityEngine;

public class LoadTagClassifyObject : ClassifyObject
{
    InteractableGameObject interactableGameObject;
    void Start()
    {
        interactableGameObject = GetComponent<InteractableGameObject>();
        push = Resources.Load<AudioClip>("Sonidos/positive-beeps");
        incorrect = Resources.Load<AudioClip>("Sonidos/negative-beeps");
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bucket")
        {
            Bucket bucket = other.GetComponent<Bucket>();
            if (interactableGameObject.GetConfig(bucket.bucketTagDictionary.Keys.ElementAt(0)) == bucket.bucketTagDictionary.Values.ElementAt(0))
            {
                Debug.Log("True");
                completed = true;
                AudioSource.PlayClipAtPoint(push, Vector3.zero, 1.0f);
                gameObject.DestroySafely();
            }
            else
            {
                Debug.Log("False");
                completed = false;
                AudioSource.PlayClipAtPoint(incorrect, Vector3.zero, 1.0f);
            }
        } else 
        {
            completed = false;
        }
    }

}
