using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInstruction : MonoBehaviour
{
    public bool instruction = false;
    private SoundManager soundManager;

    public CheckInstruction submision1;
    public CheckInstruction submision2;

    void Awake()
    {
        soundManager = GameObject.FindObjectOfType<SoundManager>();
    }   

    void OnTriggerEnter(Collider collision)
    {
        //mano is hand
        if ((collision.tag.Equals("Player")) && !instruction && (!submision1.instruction) && (!submision2.instruction)) 
        {
            soundManager.PlaySound("Ins3L1");
            StartCoroutine(soundManager.ChangeScreenInstruction("Ins3L1", "3Ins", "", 0, 2, 0));
            instruction = true;
        }

        Debug.Log(collision.name + "========================================================");
    }
}
