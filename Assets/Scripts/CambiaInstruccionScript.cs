using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiaInstruccionScript : MonoBehaviour
{
    bool ins3 = false;
    private SoundManager soundManager;

    void Awake()
    {
        soundManager = GameObject.FindObjectOfType<SoundManager>();
    }   

    void OnTriggerEnter(Collider col)
    {
        if (col.name.StartsWith("submision") && !ins3) 
        {
            soundManager.PlaySound("Ins3L1");
            StartCoroutine(soundManager.ChangeScreenInstruction("Ins3L1", "3Ins", "", 0, 2, 0));
            ins3 = true;
        }
    }
}
