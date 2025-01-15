using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HttpCommand
{
    public class GetModelRequest : BaseRequest
    {
        
    }

    public class GetModelGetResponse : BaseResponse
    {
        
    }

    public class CreateModelRequest : UpdateRequest
    {
        [JsonProperty("prompt")]
        public string Prompt { get; set; }
        public string Name { get; set; }
        public override byte[] ToBody()
        {
            var rawBody = JsonConvert.SerializeObject(this);
            return Encoding.UTF8.GetBytes(rawBody);
        }
    }

    public class CreateModelResponse : BaseResponse
    {
        
    }
}