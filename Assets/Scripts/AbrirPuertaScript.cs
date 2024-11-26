using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPuertaScript : MonoBehaviour
{
    public bool completada = false;
    public AudioClip push;
    public AudioClip incorrecto;

    public PlayAnimationAbrirPuerta playAnimationAbrirPuerta;

    // public GameObject colExample;  // This is not used in the script

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            CheatSuccess();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.name == this.name)
        {
            AudioSource.PlayClipAtPoint(push, Vector3.zero, 1.0f);
            completada = true;
            StartCoroutine(playAnimationAbrirPuerta.AnimPlay());
            Destroy(col.gameObject);
        }
        else
        {
            if (!(col.tag == "mano"))
            {
                AudioSource.PlayClipAtPoint(incorrecto, Vector3.zero, 1.0f);
            }
        }
    }

    void CheatSuccess()
    {
        AudioSource.PlayClipAtPoint(push, Vector3.zero, 1.0f);
        completada = true;
        StartCoroutine(playAnimationAbrirPuerta.AnimPlay());
    }

}
