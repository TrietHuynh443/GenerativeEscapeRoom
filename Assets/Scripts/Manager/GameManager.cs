using System;
using CommandSender;
using Cysharp.Threading.Tasks;
using DI;
using EventProcessing;
using Interface.Services;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoService
    {
        [Injector]
        private readonly IModelCommandSenderService _commandSender;
        [Injector]
        private readonly IEventHandlerService _eventHandlerService;

        private void Start()
        {
            _eventHandlerService.AddEventListener<OnCreateNewModelEvent>(SendCreateNewModelCommand);
        }

        private async void SendCreateNewModelCommand(OnCreateNewModelEvent evt)
        {
            try
            {
                var model = await _commandSender.Gen3DModel(evt.Prompt, "test");
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        private void OnDestroy()
        {
            _eventHandlerService.RemoveEventListener<OnCreateNewModelEvent>(SendCreateNewModelCommand);
        }
    }
}
