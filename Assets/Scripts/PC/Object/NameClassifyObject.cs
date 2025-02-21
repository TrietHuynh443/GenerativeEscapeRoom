using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameClassifyObject : ClassifyObject
{
    [SerializeField] private ClassifyGame classifyGame;
    // bool isSuccess = false;

    public override void Start()
    {
        base.Start();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bucket")
        {
            Debug.Log("Bucket");
            if (other.name == name)
            {
                Success();
            }
            else
            {
                completed = false;
                AudioSource.PlayClipAtPoint(incorrect, Vector3.zero, 1.0f);
            }
        } else 
        {
            completed = false;
        }
    }

    private void Success()
    {
        AudioSource.PlayClipAtPoint(push, Vector3.zero, 1.0f);
        completed = true;
        if (classifyGame.isSuccess)
            return;
        Debug.Log("Correct2");
        _soundManager.PlaySound("Ins4L1");
        StartCoroutine(_soundManager.ChangeScreenInstruction("", "4Ins", "", 0, 2, 0));
        classifyGame.isSuccess = true;
        // ++_successSubmissionAmount;
        // _eventHandlerService.RaiseEvent(new OnSubmisionSuccess()
        // {
        //     Level = _level,
        //     Count = _successSubmissionAmount
        // });
    }
}
