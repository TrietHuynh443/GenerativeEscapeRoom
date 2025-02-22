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
        private readonly IEventHandlerService _eventHandlerService;
        private readonly ModelCommandSender _modelCommandSender;

        private void OnEnable()
        {
            _eventHandlerService.AddEventListener<OnCreateNewModelEvent>(SendCreateNewModelCommand);
        }

        private async void SendCreateNewModelCommand(OnCreateNewModelEvent evt)
        {
            try
            {
                Debug.Log("Send CreateNewModelCommand");
                await _modelCommandSender.Send(new ()
                {
                    ModelName = "monkey"
                });
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        private void OnDisable()
        {
            _eventHandlerService?.RemoveEventListener<OnCreateNewModelEvent>(SendCreateNewModelCommand);
        }
    }
}
