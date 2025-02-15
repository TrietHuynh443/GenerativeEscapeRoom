using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace HttpCommand
{
    public class GetModelRequest : BaseRequest
    {
        
    }

    public class GetModelGetResponse : BaseResponse
    {
        
    }

    public class CreateModelRequest : BaseRequest
    {
        [JsonProperty("prompt")]
        public string Prompt { get; set; }
        // public override byte[] ToBody()
        // {
        //     var rawBody = JsonConvert.SerializeObject(this);
        //     return Encoding.UTF8.GetBytes(rawBody);
        // }
    }

    public class CreateModelResponse : BaseResponse
    {
        [JsonProperty("model")]
        public string Model { get; set; }
        [JsonProperty("textureData")]
        public string TextureData { get; set; }
        [JsonProperty("mtlData")]
        public string MtlData { get; set; }
    }

    public class LLMRequest : UpdateRequest
    {
        public string Prompt { get; set; }
    }

    public class LLMResponse : BaseResponse
    {
        
    }
    
    public class OpenAiRequest : LLMRequest
    {
        [JsonProperty("model")]
        public string Model { get; set; }
        [JsonProperty("messages")]
        public List<Dictionary<string, string>> Messages { get; set; }
        [JsonProperty("response_format")]
        public string ResponseFormat { get; set; }
    }

    public class OpenAiResponse : LLMResponse
    {
        
    }

    public class DeepSeekRequest : LLMRequest
    {
        
    }

    public class DeepSeekResponse : LLMResponse
    {
        
    }

    public class GeminiRequest : LLMRequest
    {
        
    }

    public class GeminiResponse : LLMResponse
    {
        
    }
}