using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Factory;
using HttpCommand;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CommandSender
{
    public class LLMStructureCommand : CommandSender<LLMRequest, LLMResponse>
    {
        private LLMRequestFactory _llmRequestFactory = new LLMRequestFactory();
        public override Task Send(LLMRequest request)
        {
            string json = _llmRequestFactory.CreateInstance(new LLMFactoryMaterials()
            {
                Request = request
            });
            Addressables.LoadAssetAsync<LLMConfigSO>("config").Completed += async (asset) =>
            {
                if (asset.Status != AsyncOperationStatus.Succeeded)
                    return;
                var webRequest = CreateRequest(asset.Result.Url, UnityWebRequest.kHttpVerbPOST, json, new Dictionary<string, string>()
                {
                    {"Authorization", $"Bearer {asset.Result.ApiKey}"}
                });
                var res = await DoPost(webRequest);
                Debug.Log("test response: " + res);

            };
            return Task.CompletedTask;
        }
    }    
}

