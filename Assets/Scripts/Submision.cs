using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submision : MonoBehaviour
{
    public bool completed = false;
    public AudioClip push;
    public AudioClip incorrect;

    public Submision submission1;
    public Submision submission2;

    private SoundManager soundManager;

    public bool inst4 = false;

    private void Awake()
    {
        soundManager = GameObject.FindObjectOfType<SoundManager>();
    }

    public void Update()
    {
#if UNITY_EDITOR_WIN
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheatSuccess();
        }
#endif
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.name == name)
        {
            Success();
        }
        else
        {
            AudioSource.PlayClipAtPoint(incorrect, Vector3.zero, 1.0f);
        }
    }

    private void Success()
    {
        AudioSource.PlayClipAtPoint(push, Vector3.zero, 1.0f);
        completed = true;
        if ((!inst4) && (!submission1.inst4) && (!submission2.inst4))
        {
            soundManager.PlaySound("Ins4L1");
            StartCoroutine(soundManager.ChangeScreenInstruction("", "4Ins", "", 0, 2, 0));
            inst4 = true;
        }
    }

    private void CheatSuccess()
    {
        completed = true;
        // soundManager.PlaySound("Ins4L1");
        // StartCoroutine(soundManager.ChangeScreenInstruction("", "4Ins", "", 0, 2, 0));
        // inst4 = true;
    }
}
