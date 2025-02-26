using System.Collections;
using System.Collections.Generic;
using HttpCommand;
using Newtonsoft.Json;
using UnityEngine;

public class LoadAllModelRequest : BaseRequest
{
    [JsonProperty("room_name")] public string RoomName { get; set; } = "Level2";
}

public class LoadAllModelResponse : BaseResponse
{
    [JsonProperty("_id")]
    public Id Id { get; set; }

    [JsonProperty("room_name")]
    public string RoomName { get; set; }

    [JsonProperty("obj_id")]
    public Id ObjId { get; set; }

    [JsonProperty("parameters")]
    public RoomData Parameters { get; set; }
}

public class Id
{
    [JsonProperty("$oid")]
    public string Oid { get; set; }
}

public class GetCategoryRequest : BaseRequest
{
    [JsonProperty("obj_id")] 
    public string ObjId { get; set; } = "";
}

public class GetCategoryResponse : BaseResponse
{
    [JsonProperty("output")]
    public string Category { get; set; } = "";
}