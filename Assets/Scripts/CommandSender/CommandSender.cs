using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Best.HTTP;
using Best.HTTP.Request.Upload;
using Cysharp.Threading.Tasks;
using HttpCommand;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace CommandSender
{
    public abstract class CommandSender<TRequest, TResponse>
        where TRequest : BaseRequest
        where TResponse : BaseResponse
    {
        protected readonly string BaseUrl  = "http://localhost:8000";
        protected string Url { get; set; }
        public abstract Task Send(TRequest request);
        protected async UniTask<TResponse> DoGet(TRequest request)
        {
            var queryString = request.ToQuery();
            var url = $"{Url}?{queryString}";
            using var webRequest = UnityWebRequest.Get(url);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

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
        
        protected UnityWebRequest CreateRequest(string url, string method, string jsonData, Dictionary<string, string> headers = null)
        {
            UnityWebRequest request = new UnityWebRequest();
            if (method == UnityWebRequest.kHttpVerbGET)
            {
                request = UnityWebRequest.Get(url);
            }
            else if (method == UnityWebRequest.kHttpVerbPOST)
            {
                request = new UnityWebRequest(url, method);
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
            }
            request.SetRequestHeader("Content-Type", "application/json");
            if (headers != null)
            {
                foreach (var (key, val) in headers)
                {
                    request.SetRequestHeader(key, val);
                }
            }

            return request;
        }

        protected async UniTask<List<TResponse>> DoGet(UnityWebRequest webRequest)
        {
            try
            {
                await webRequest.SendWebRequest().ToUniTask();
                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    return JsonConvert.DeserializeObject<List<TResponse>>(webRequest.downloadHandler.text);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Request failed: {e.Message}");
            }

            return null;
        }
        
        protected async UniTask<TResponse> DoPost(UnityWebRequest webRequest)
        {
            try
            {
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

        // ReSharper disable Unity.PerformanceAnalysis
        protected async UniTask<byte[]> DoDownload<T>(string url, T payload, HTTPMethods method = HTTPMethods.Post) where T : BaseRequest
        {
            try
            {
                UnityWebRequest request = CreateRequest(url, UnityWebRequest.kHttpVerbPOST, JsonConvert.SerializeObject(payload), null);
                Debug.Log("Send " + JsonConvert.SerializeObject(payload));
                await request.SendWebRequest().ToUniTask();
                var res = request.downloadHandler.data;
                if (res == null)
                {
                    Debug.LogError("Request failed");
                    return null;
                }

                return res;
            }
            catch (Exception e)
            {
                Debug.LogError($"Request failed: {e.Message}");
                return null;
            }
        }
        
    }
}