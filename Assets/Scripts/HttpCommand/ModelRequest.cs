using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace HttpCommand
{
    public class GetModelRequest : BaseRequest
    {
        [JsonProperty("model_id")]
        public string ModelId { get; set; }
    }

    public class GetModelGetResponse : BaseResponse
    {
        
    }

    public class CreateModelRequest : BaseRequest
    {
        [JsonProperty("prompt")]
        public string Prompt { get; set; }
    }

    public class CreateModelResponse : BaseResponse
    {
        [JsonProperty("output")]
        public string Id { get; set; }
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