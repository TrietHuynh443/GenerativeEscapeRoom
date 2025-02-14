using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class BtnPressedInteraction : MonoBehaviour
// {
//     public int btnID;
//     public float intensidadLuz;
//     public Light Luz;
//     public bool desactivando;
//     public bool desactivado;
//     public AudioClip sonido;
//     public GameController controlador;
//     public Animator anim;

//     private bool pTouched = false;



//     void Start()
//     {
//         intensidadLuz = Luz.intensity;
//         pTouched = false;
//     }

//     IEnumerator AnimPlay()
//     {
//         anim.SetBool("press", true);
//         yield return new WaitForSeconds(1);
//         anim.SetBool("press", false);
//     }

//     public void Activar() {

//         if(controlador.playable)
//         {
//             //print("btnID "+btnID);
//             desactivado = false;
//             desactivando = false;
//             Luz.intensity = intensidadLuz;
//             Luz.gameObject.SetActive(true);
//             StartCoroutine(AnimPlay());

//             if (controlador.userShift)
//             {
//                 Debug.Log("Hi from Activar");
//                 print("mi turno ");
//                 controlador.PlayUser(btnID);
//             }

//             AudioSource.PlayClipAtPoint(sonido, Vector3.zero, 1.0f);
//             Invoke("Desactivar", 0.1f);
//         }

//     }

//     public void Desactivar() {
//         desactivando = true;
//     }

//     void Update()
//     {
//         if (desactivando && !desactivado) {
//             Luz.intensity = Mathf.Lerp(Luz.intensity, 0, 0.065f);
//         }

//         if (Luz.intensity <= 0.02)
//         {
//             Luz.intensity = 0;
//             desactivado = true;
//         }
//     }

//     IEnumerator OnTriggerEnter()
//     {
//         if (!pTouched && (controlador.userShift))
//         {
//             pTouched = true;
//             Activar();
//             yield return new WaitForSeconds(2);    
//             pTouched = false;    
//             print("Off "+ pTouched);
//         }
//     }

//     /**
//     void OnMouseDown()
//     {
//         Activar();
//     }*/




// }


public class BtnPressedInteraction : ButtonBase
{
    public int btnID;
    public bool desactivando;
    public bool desactivado;

    public TraceGame game;

    public override void Activate()
    {
        if (!pTouched && (game.userShift))
        {
            pTouched = true;
            if (game.playable)
            {
                desactivado = false;
                desactivando = false;
                buttonLight.intensity = initialLightIntensity;
                buttonLight.gameObject.SetActive(true);
                StartCoroutine(AnimPlay());

                if (game.userShift)
                {
                    game.PlayUser(btnID);
                }

                AudioSource.PlayClipAtPoint(sound, Vector3.zero, 1.0f);
                Invoke("Desactivar", 0.1f);
            }
            // yield return new WaitForSeconds(2);
            pTouched = false;
        }

    }

    public void Activar()
    {
        if (game.playable)
        {
            desactivado = false;
            desactivando = false;
            buttonLight.intensity = initialLightIntensity;
            buttonLight.gameObject.SetActive(true);
            StartCoroutine(AnimPlay());

            if (game.userShift)
            {
                game.PlayUser(btnID);
            }

            AudioSource.PlayClipAtPoint(sound, Vector3.zero, 1.0f);
            Invoke("Desactivar", 0.1f);
        }
    }

    public void Desactivar()
    {
        desactivando = true;
    }

    void Update()
    {
        if (desactivando && !desactivado)
        {
            buttonLight.intensity = Mathf.Lerp(buttonLight.intensity, 0, 0.065f);
        }

        if (buttonLight.intensity <= 0.02)
        {
            buttonLight.intensity = 0;
            desactivado = true;
        }
    }

    IEnumerator OnTriggerEnter()
    {
        if (!pTouched && game.userShift)
        {
            pTouched = true;
            Activate();
            yield return new WaitForSeconds(2);
            pTouched = false;
        }
    }
}
