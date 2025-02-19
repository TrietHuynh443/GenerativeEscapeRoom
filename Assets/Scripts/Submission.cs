using System;
using System.Collections;
using System.Collections.Generic;
using DI;
using EventProcessing;
using Interface.Services;
using UnityEngine;
using UnityEngine.Serialization;

public class Submission : MonoBehaviour
{
    public bool completed = false;
    public AudioClip push;
    public AudioClip incorrect;

    // public Submision submission1;
    // public Submision submission2;
    [SerializeField] private int _level = 1;
    
    private static int _successSubmissionAmount = 0;
    private SoundManager soundManager;

    public bool isSuccess = false;
    
    [Injector]
    private IEventHandlerService _eventHandlerService;


    private void Awake()
    {
        soundManager = GameObject.FindObjectOfType<SoundManager>();
    }

    public void Update()
    {
#if UNITY_EDITOR_WIN
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     CheatSuccess();
        // }
#endif
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.name == name)
        {
            Debug.Log("Correct===========================================================================");
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
        if (isSuccess)
            return;
        Debug.Log("Correct2");
        soundManager.PlaySound("Ins4L1");
        StartCoroutine(soundManager.ChangeScreenInstruction("", "4Ins", "", 0, 2, 0));
        isSuccess = true;
        ++_successSubmissionAmount;
        _eventHandlerService.RaiseEvent(new OnSubmisionSuccess()
        {
            Level = _level,
            Count = _successSubmissionAmount
        });
    }

    // private void CheatSuccess()
    // {
    //     completed = true;
    //     // soundManager.PlaySound("Ins4L1");
    //     // StartCoroutine(soundManager.ChangeScreenInstruction("", "4Ins", "", 0, 2, 0));
    //     // inst4 = true;
    // }
}
