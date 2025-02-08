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
}