using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Meta.WitAi;
using UnityEngine;

public class LoadTagClassifyObject : ClassifyObject
{
    public ClassifyGame classifyGame;
    InteractableGameObject interactableGameObject;
    public override void Start()
    {
        base.Start();
        interactableGameObject = GetComponent<InteractableGameObject>();
        push = Resources.Load<AudioClip>("Sonidos/positive-beeps");
        incorrect = Resources.Load<AudioClip>("Sonidos/negative-beeps");
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player") && !classifyGame.isGrabobjectFirstTime)
        {
            _soundManager.PlaySound("Ins2L3");
            classifyGame.isGrabobjectFirstTime = true;
        }

        if (other.tag == "Bucket")
        {
            Bucket bucket = other.GetComponent<Bucket>();
            if (interactableGameObject.GetConfig(bucket.bucketTagDictionary.Keys.ElementAt(0)) == bucket.bucketTagDictionary.Values.ElementAt(0))
            {
                // Debug.Log("True");
                Success();
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

    private void Success()
    {
        AudioSource.PlayClipAtPoint(push, Vector3.zero, 1.0f);
        completed = true;
        if (classifyGame.isSuccess)
            return;
        _soundManager.PlaySound("Ins3L3");
        StartCoroutine(_soundManager.ChangeScreenInstruction("", "4Ins", "", 0, 2, 0));
        classifyGame.isSuccess = true;
    }

}
