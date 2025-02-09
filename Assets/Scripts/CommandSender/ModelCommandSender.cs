using System;
using System.IO;
using Cysharp.Threading.Tasks;
using Dummiesman;
using HttpCommand;
using Interface.Services;
using UnityEngine;
using UnityEngine.Networking;

namespace CommandSender
{
    public class ModelCommandSender : CommandSender<CreateModelRequest, CreateModelResponse>, IModelCommandSenderService
    {
        public ModelCommandSender()
        {
            Url = $"{BaseUrl}/create-model";
            //Test
            // Url = "https://people.sc.fsu.edu/~jburkardt/data/obj/lamp.obj";
        }

        // protected override async UniTask<CreateModelResponse> DoGet(CreateModelRequest request)
        // {
        //     using var unityWebRequest = new UnityWebRequest(Url, UnityWebRequest.kHttpVerbGET);
        //     unityWebRequest.downloadHandler = new DownloadHandlerBuffer(); // Ensure a download handler is set
        //     unityWebRequest.SetRequestHeader("Content-Type", "application/json");
        //     try
        //     {
        //         Debug.Log(Url);
        //         await unityWebRequest.SendWebRequest().ToUniTask();
        //         if (unityWebRequest.result != UnityWebRequest.Result.Success)
        //         {
        //             return null;
        //         }
        //         Debug.Log(unityWebRequest.downloadHandler.text);
        //         // MemoryStream textStream  = new MemoryStream(Encoding.UTF8.GetBytes(unityWebRequest.downloadHandler.text));
        //         return JsonUtility.FromJson<CreateModelResponse>(unityWebRequest.downloadHandler.text);
        //     }
        //     catch (Exception e)
        //     {
        //         Debug.LogError(e.Message);
        //         return null;
        //     }
        // }
        public async UniTask<GameObject> Gen3DModel(string prompt, string name)
        {
            // Send a command to generate the model
            var response = await SendCommand(new CreateModelRequest() { Prompt = prompt });
            if (response != null)
            {
                var modelBytes = Convert.FromBase64String(response.Model);
                var mtlBytes = Convert.FromBase64String(response.MtlData);
                var textureBytes = Convert.FromBase64String(response.TextureData);
                try
                {
                    // Define the Resources folder path
                    string resourcesPath = Path.Combine(Application.dataPath, "Resources", "GeneratedModels", name);
                    Directory.CreateDirectory(resourcesPath);

                    // Save the .obj file
                    string objPath = Path.Combine(resourcesPath, $"{name}.obj");
                    await File.WriteAllBytesAsync(objPath, modelBytes);
                    
                    // Save the .png file (if any texture is provided)
                    string pngPath = string.Empty;
                    if (textureBytes != null)
                    {
                        pngPath = Path.Combine(resourcesPath, $"{name}.png");
                        File.WriteAllBytes(pngPath, textureBytes);
                    }
                    MemoryStream modelStream = new MemoryStream(modelBytes);
                    GameObject model = null;
                    // Save the .mtl file
                    if (mtlBytes != null)
                    {
                        string mtlPath = Path.Combine(resourcesPath, $"{name}.mtl");
                        await File.WriteAllBytesAsync(mtlPath, mtlBytes);
                        var matStream = new MemoryStream(mtlBytes);
                        model = new OBJLoader().Load(modelStream, matStream, texturePath: pngPath);
                    }
                    else
                    {
                        model = new OBJLoader().Load(modelStream);
                    }
                    
                    // Set model name and add required components
                    model.name = name;
                    model.AddComponent<InteractableGameObject>();

                    return model;
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error generating 3D model: {ex.Message}");
                    return null;
                }
            }

            Debug.LogWarning("Response from SendCommand was null.");
            return null;
        }
    }
}