using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public abstract class ClassifyObject : MonoBehaviour
{
    public bool completed = false;
    public AudioClip push;
    public AudioClip incorrect;

    protected SoundManager _soundManager;

    protected abstract void OnTriggerEnter(Collider other);

    public virtual void Start()
    {
        _soundManager = SoundManager.Instance;
    }
}
