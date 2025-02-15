using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Factory;
using HttpCommand;
using Newtonsoft.Json;
using UnityEngine;

namespace CommandSender
{
    public class LLMStructureCommand : CommandSender<LLMRequest, LLMResponse>
    {
        private LLMRequestFactory _llmRequestFactory = new LLMRequestFactory();
        public override void Send(LLMRequest request)
        {
            string json = _llmRequestFactory.CreateInstance(new LLMFactoryMaterials()
            {
                EnumName = "",
                Input = "Hello con vo",
                Request = request
            });
            Debug.Log("Sending LLM structure request:\n " + json);
            // var res = await DoPost(request);
        }
    }    
}

