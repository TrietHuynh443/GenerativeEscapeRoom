using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClassifyObject : MonoBehaviour
{
    public bool completed = false;
    public AudioClip push;
    public AudioClip incorrect;

    protected abstract void OnTriggerEnter(Collider other);
}
