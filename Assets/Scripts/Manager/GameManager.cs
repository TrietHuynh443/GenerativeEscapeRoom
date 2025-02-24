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
        private readonly ModelCommandSender _modelCommandSender = new ModelCommandSender();

        private void Start()
        {
            _eventHandlerService.AddEventListener<OnCreateNewModelEvent>(SendCreateNewModelCommand);
        }

        private async void SendCreateNewModelCommand(OnCreateNewModelEvent evt)
        {
            try
            {
                Debug.Log("Send CreateNewModelCommand");
                var res = await _modelCommandSender.CreateModel(new ()
                {
                    Prompt = evt.Prompt,
                });

                if (res == null)
                {
                    Debug.Log("Send CreateNewModelCommand failed");
                }
                else
                {
                    Debug.Log("Send CreateNewModelCommand succeeded");
                    GameObject model = await _modelCommandSender.GetModelObj(new()
                    {
                        ModelId = res.Id
                    });
                    if (model == null)
                    {
                        Debug.Log($"Load {res.Id} failed");
                        return;
                    }
                    model.gameObject.name = res.Id;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        private void OnDestroy()
        {
            _eventHandlerService?.RemoveEventListener<OnCreateNewModelEvent>(SendCreateNewModelCommand);
        }
    }
}
