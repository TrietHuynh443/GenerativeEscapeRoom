using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ButtonBase : MonoBehaviour
{
    public Light buttonLight;
    public AudioClip sound;
    public Animator anim;

    protected bool pTouched = false;
    protected float initialLightIntensity;

    protected virtual void Start()
    {
        initialLightIntensity = buttonLight.intensity;
        pTouched = false;
    }

    protected IEnumerator AnimPlay()
    {
        anim.SetBool("press", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("press", false);
    }

    public virtual void Activate() { }
}