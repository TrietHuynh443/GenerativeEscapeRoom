using System;
using System.Collections;
using System.Collections.Generic;
using DI;
using EventProcessing;
using Interface.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _prompt;
    [SerializeField] private Button _creatModelButton;
    [Injector]
    private readonly IEventHandlerService _eventAggregator;

    private void OnEnable()
    {
        _creatModelButton.onClick.AddListener(RaiseGenModelOnClickEvent);
    }

    private void OnDisable()
    {
        _creatModelButton.onClick.RemoveListener(RaiseGenModelOnClickEvent);
    }

    public void RaiseGenModelOnClickEvent()
    {
        OnCreateNewModelEvent evt = new OnCreateNewModelEvent()
        {
            Prompt = _prompt.text
        };
        _eventAggregator.RaiseEvent(evt);
    }
}
