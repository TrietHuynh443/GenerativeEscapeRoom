using HttpCommand;

namespace CommandSender
{
    public class ModelCommandSender : CommandSender<CreateModelRequest, CreateModelResponse>
    {
        public ModelCommandSender()
        {
            Url = $"{BaseUrl}/create-model3D";
        }

        // protected override async UniTask<CreateModelResponse> DoGet(CreateModelRequest request)
        // {
        //     using var unityWebRequest = new UnityWebRequest(Url, UnityWebRequest.kHttpVerbGET);
        //     unityWebRequest.SetRequestHeader("Content-Type", "application/json");
        //     try
        //     {
        //         await unityWebRequest.SendWebRequest().ToUniTask();
        //         if (unityWebRequest.result != UnityWebRequest.Result.Success)
        //         {
        //             return null;
        //         }
        //         // MemoryStream textStream  = new MemoryStream(Encoding.UTF8.GetBytes(unityWebRequest.downloadHandler.text));
        //         return JsonUtility.FromJson<CreateModelResponse>(unityWebRequest.downloadHandler.text);
        //     }
        //     catch (Exception e)
        //     {
        //         Debug.LogError(e.Message);
        //         return null;
        //     }
        // }
    }
}