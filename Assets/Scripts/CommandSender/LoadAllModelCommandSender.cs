using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Meta.WitAi.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace CommandSender
{
    public class LoadAllModelCommandSender : CommandSender<LoadAllModelRequest, LoadAllModelResponse>
    {
        private List<string> _objIds { get;} = new();
        
        public LoadAllModelCommandSender()
        {
            Url = $"{BaseUrl}/load_all_obj_in_room?room_name=";
        }
        public async Task<List<GameObject>> LoadAll(LoadAllModelRequest request)
        {
            UnityWebRequest webRequest = CreateRequest(Url + request.RoomName, UnityWebRequest.kHttpVerbGET, null);
            var res = await DoGetAll(webRequest);
            List<GameObject> objs = new();
    
            if (res != null)
            {
                ModelCommandSender commandSender = new ModelCommandSender();
        
                for (int i = 0; i < res.Count; i++)
                {
                    var loadAllModelResponse = res[i];
                    Debug.Log($"Loading Model {i + 1}/{res.Count}: {loadAllModelResponse.ObjId.Oid}");
            
                    _objIds.Add(loadAllModelResponse.ObjId.Oid);
            
                    // Ensure each model loads correctly
                    GameObject model = await commandSender.GetModelObj(new()
                    {
                        ModelId = loadAllModelResponse.ObjId.Oid,
                    });

                    if (model == null)
                    {
                        Debug.LogError($"Failed to load model {loadAllModelResponse.ObjId.Oid}");
                        continue;
                    }

                    model.name = loadAllModelResponse.ObjId.Oid;
                    model.transform.SetPositionAndRotation(
                        loadAllModelResponse.Parameters.ObjectPosition.AsVector3, 
                        Quaternion.Euler(loadAllModelResponse.Parameters.ObjectRotation.AsVector3)
                    );
            
                    var interactable = model.GetComponent<InteractableGameObject>();
                    if (interactable != null)
                    {
                        interactable.SetConfig(ECategoryType.Class, loadAllModelResponse.Parameters.ObjectClass);
                    }
                    else
                    {
                        Debug.LogWarning($"Missing InteractableGameObject on {model.name}");
                    }

                    objs.Add(model);
                    await Task.Delay(100);
                }
            }

            return objs;
        }


        public override Task Send(LoadAllModelRequest request)
        {
            throw new System.NotImplementedException();
        }
    }

    public class LoadCategoryCommandSender : CommandSender<GetCategoryRequest, GetCategoryResponse>
    {
        public LoadCategoryCommandSender()
        {
            Url = $"{BaseUrl}/model_cate?obj_id=";
        }
        public async Task<GetCategoryResponse> GetCate(GetCategoryRequest request)
        {
            var query = Url + request.ObjId;
            UnityWebRequest webRequest = CreateRequest(query, UnityWebRequest.kHttpVerbGET, null);
            return await DoGet(webRequest);
        }
        public override Task Send(GetCategoryRequest request)
        {
            return Task.CompletedTask;
        }
    }
}
