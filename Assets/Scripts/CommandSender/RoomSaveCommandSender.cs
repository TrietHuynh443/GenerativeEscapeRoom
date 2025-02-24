using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace CommandSender
{
    public class RoomSaveCommandSender : CommandSender<SaveModelRequest, SaveModelResponse>
    {
        public RoomSaveCommandSender()
        {
            Url = $"{BaseUrl}/save_obj_to_room";
        }
        public override async Task Send(SaveModelRequest request)
        {
            try
            {
                UnityWebRequest webRequest = CreateRequest(Url, UnityWebRequest.kHttpVerbPOST,
                    JsonConvert.SerializeObject(request), new Dictionary<string, string>()
                    {
                        {"Param-Query-Type", "id"}
                    });
                var res = await DoPost(webRequest);
                if (res != null)
                {
                    Debug.Log(JsonConvert.SerializeObject(res));
                }
                else
                {
                    Debug.Log(JsonConvert.SerializeObject(request) + " request could not be sent.");
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }
}
