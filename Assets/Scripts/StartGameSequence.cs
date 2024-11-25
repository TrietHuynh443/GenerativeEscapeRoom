using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameSequence : MonoBehaviour
{

    public float intensityLight;
    public Light Light;
    public AudioClip sound;
    public GameController controller;
    public Animator anim;

    private bool pTouched = false;

    void Start()
    {
        intensityLight = Light.intensity;
        pTouched = false;
#if UNITY_EDITOR_WIN
    Debug.Log("StartGameSequence started in editor mode");
    Activate();
#endif
    }

    IEnumerator AnimPlay()
    {
        anim.SetBool("press", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("press", false);
    }

    public void Activate() {
        
        Light.intensity = intensityLight;
        Light.gameObject.SetActive(true);
        StartCoroutine(AnimPlay());
        AudioSource.PlayClipAtPoint(sound, Vector3.zero, 1.0f);
        StartCoroutine(controller.Play());
    }

    void OnTriggerEnter()
    {
        if (!pTouched)
        {
            pTouched = true;
            Activate();
            //yield return new WaitForSeconds(3);    
            pTouched = false;    
        }
    }

}
