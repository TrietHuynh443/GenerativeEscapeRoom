using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class StartGameSequence : MonoBehaviour
// {
//     public float intensityLight;
//     public Light Light;
//     public AudioClip sound;
//     public GameController controller;
//     public Animator anim;

//     private bool pTouched = false;

//     void Start()
//     {
//         intensityLight = Light.intensity;
//         pTouched = false;
//     }

//     void Update()
//     {
// #if UNITY_EDITOR_WIN
//         if (Input.GetKeyDown(KeyCode.Space))
//         {
//             Debug.Log("StartGameSequence started in editor mode");
//             pTouched = true;
//             Activate();
//             pTouched = false;
//         }
// #endif
//     }

//     IEnumerator AnimPlay()
//     {
//         anim.SetBool("press", true);
//         yield return new WaitForSeconds(1);
//         anim.SetBool("press", false);
//     }

//     public void Activate()
//     {
//         Light.intensity = intensityLight;
//         Light.gameObject.SetActive(true);
//         StartCoroutine(AnimPlay());
//         AudioSource.PlayClipAtPoint(sound, Vector3.zero, 1.0f);
//         StartCoroutine(controller.Play());
//     }

//     void OnTriggerEnter()
//     {
//         if (!pTouched)
//         {
//             pTouched = true;
//             Activate();
//             //yield return new WaitForSeconds(3);
//             pTouched = false;
//         }
//     }
// }


public class StartGameSequence : ButtonBase
{
    public TraceGame game;
    void Update()
    {
#if UNITY_EDITOR_WIN
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     Debug.Log("StartGameSequence started in editor mode");
        //     pTouched = true;
        //     Activate();
        //     pTouched = false;
        // }
#endif
    }

    public override void Activate()
    {
        Debug.Log("StartGameSequence started in editor mode");
        if (!pTouched)
        {
            Debug.Log("StartGameSequence started in editor mode 2");
            pTouched = true;
            buttonLight.intensity = initialLightIntensity;
            buttonLight.gameObject.SetActive(true);
            StartCoroutine(AnimPlay());
            AudioSource.PlayClipAtPoint(sound, Vector3.zero, 1.0f);
            StartCoroutine(game.GameControl());
            pTouched = false;
        }
    }

    // void OnTriggerEnter()
    // {
    //     if (!pTouched)
    //     {
    //         pTouched = true;
    //         Activate();
    //         pTouched = false;
    //     }
    // }
}