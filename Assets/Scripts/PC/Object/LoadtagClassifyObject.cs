using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoadtagClassifyObject : ClassifyObject
{
    InteractableGameObject interactableGameObject;
    void Start()
    {
        interactableGameObject = GetComponent<InteractableGameObject>();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bucket")
        {
            Bucket bucket = other.GetComponent<Bucket>();
            if (interactableGameObject.GetConfig(bucket._bucketTagDictionary.Keys.ElementAt(0)) == bucket._bucketTagDictionary.Values.ElementAt(0))
            {
                completed = true;
                AudioSource.PlayClipAtPoint(push, Vector3.zero, 1.0f);
            }
            else
            {
                completed = false;
                AudioSource.PlayClipAtPoint(incorrect, Vector3.zero, 1.0f);
            }
        } else 
        {
            completed = false;
        }
    }

}
