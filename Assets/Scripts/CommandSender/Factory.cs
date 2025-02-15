using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using HttpCommand;
using Newtonsoft.Json;
using UnityEngine;
using OpenAiRequest = HttpCommand.OpenAiRequest;

namespace Factory
{
    public enum EFactoryType
    {
        LLMRequest = 1,
    }
    public abstract class FactoryMaterials
    {
        public string EnumName;
        public string Input;
    }
    

    public abstract class Factory<T>
    {
        private EFactoryType _factoryType;

        public abstract T CreateInstance(FactoryMaterials materials);
        protected Factory(EFactoryType factoryType)
        {
            _factoryType = factoryType;
        }
    }

    public class LLMFactoryMaterials : FactoryMaterials
    {
        public LLMRequest Request { get; set; }
    }

    public class LLMRequestFactory : Factory<string>
    {
        public LLMRequestFactory() : base(EFactoryType.LLMRequest)
        {
        }

        public override string CreateInstance(FactoryMaterials materials)
        {
            LLMFactoryMaterials llmFactoryMaterials = (LLMFactoryMaterials)materials;
            var requestType = llmFactoryMaterials.Request.GetType();
            if (requestType.IsAssignableFrom(typeof(OpenAiRequest)))
            {
                return JsonConvert.SerializeObject((OpenAiRequest) llmFactoryMaterials.Request);
            }
            else if (requestType.IsAssignableFrom(typeof(DeepSeekRequest)))
            {
                return JsonConvert.SerializeObject((DeepSeekRequest) llmFactoryMaterials.Request);
            }
            else if (requestType.IsAssignableFrom(typeof(GeminiRequest)))
            {
                return JsonConvert.SerializeObject((GeminiRequest) llmFactoryMaterials.Request);
            }
            return string.Empty;
        }
    }
}
