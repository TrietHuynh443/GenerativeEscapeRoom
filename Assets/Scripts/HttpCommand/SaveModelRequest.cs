using System.Collections;
using System.Collections.Generic;
using HttpCommand;
using Newtonsoft.Json;
using UnityEngine;

public class SaveModelRequest : BaseRequest
{
    [JsonProperty("model_param")]
    public string ModelParam { get; set; } // if use model id add header 

    [JsonProperty("room_name")] public string RoomName { get; set; } = "Level2";
    [JsonProperty("room_params")]
    public RoomData RoomParams { get; set; } = new RoomData()
    {
        Name = "the_monkey_king",
        ObjectClass = "normal",
        ObjectPosition = new Vector3S(0, 0, 0),
        ObjectRotation = new Vector3S(0, 0, 0),
    };
}

public class SaveModelResponse : BaseResponse
{
    [JsonProperty("output")]
    public string Output { get; set; }
}
