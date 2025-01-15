using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using HttpCommand;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace CommandSender
{
    public class CommandSender<TRequest, TResponse>
        where TRequest : BaseRequest
        where TResponse : BaseResponse
    {
        protected readonly string BaseUrl  = "https://localhost:5000/";
        protected string Url { get; set; }

        public async UniTask<TResponse> SendCommand(TRequest request)
        {
            //Post
            if (typeof(UpdateRequest).IsAssignableFrom(typeof(TRequest)))
            {
                return await DoPost(request as UpdateRequest);
            }
            else
            {
                return await DoGet(request);
            }
        }

        private async UniTask<TResponse> DoGet(TRequest request)
        {
            var queryString = request.ToQuery();
            var url = $"{Url}?{queryString}";
            using var webRequest = UnityWebRequest.Get(url);
            try
            {
                await webRequest.SendWebRequest().ToUniTask();

                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    return JsonConvert.DeserializeObject<TResponse>(webRequest.downloadHandler.text);
                }
                else
                {
                    Debug.LogError($"Request failed with error: {webRequest.error}");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Request failed: {e.Message}");
            }

            return null;
        }

        private async UniTask<TResponse> DoPost(UpdateRequest request)
        {
            var bodyRaw = request.ToBody();
            using var webRequest = new UnityWebRequest(Url, UnityWebRequest.kHttpVerbPOST);
            try
            {
                webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
                await webRequest.SendWebRequest().ToUniTask();
                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    return JsonConvert.DeserializeObject<TResponse>(webRequest.downloadHandler.text);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Request failed: {e.Message}");
            }

            return null;
        }
    }
}