using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Dummiesman;
using HttpCommand;
using Interface.Services;
using UnityEngine;
using UnityEngine.Networking;

namespace CommandSender
{
    public class ModelCommandSender : CommandSender<CreateModelRequest, CreateModelResponse>
    {
        public ModelCommandSender()
        {
            Url = $"{BaseUrl}/get-model3D-zip";
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
        // public async UniTask<GameObject> Gen3DModel(string prompt, string name)
        // {
        //     // Send a command to generate the model
        //     var response = await SendCommand(new CreateModelRequest() { Prompt = prompt });
        //     if (response != null)
        //     {
        //         var modelBytes = Convert.FromBase64String(response.Model);
        //         var mtlBytes = Convert.FromBase64String(response.MtlData);
        //         var textureBytes = Convert.FromBase64String(response.TextureData);
        //         try
        //         {
        //             // Define the Resources folder path
        //             string resourcesPath = Path.Combine(Application.dataPath, "Resources", "GeneratedModels", name);
        //             Directory.CreateDirectory(resourcesPath);
        //
        //             // Save the .obj file
        //             string objPath = Path.Combine(resourcesPath, $"{name}.obj");
        //             await File.WriteAllBytesAsync(objPath, modelBytes);
        //             
        //             // Save the .png file (if any texture is provided)
        //             string pngPath = string.Empty;
        //             if (textureBytes != null)
        //             {
        //                 pngPath = Path.Combine(resourcesPath, $"{name}.png");
        //                 File.WriteAllBytes(pngPath, textureBytes);
        //             }
        //             MemoryStream modelStream = new MemoryStream(modelBytes);
        //             GameObject model = null;
        //             // Save the .mtl file
        //             if (mtlBytes != null)
        //             {
        //                 string mtlPath = Path.Combine(resourcesPath, $"{name}.mtl");
        //                 await File.WriteAllBytesAsync(mtlPath, mtlBytes);
        //                 var matStream = new MemoryStream(mtlBytes);
        //                 model = new OBJLoader().Load(modelStream, matStream, texturePath: pngPath);
        //             }
        //             else
        //             {
        //                 model = new OBJLoader().Load(modelStream);
        //             }
        //             
        //             // Set model name and add required components
        //             model.name = name;
        //             model.AddComponent<InteractableGameObject>();
        //
        //             return model;
        //         }
        //         catch (Exception ex)
        //         {
        //             Debug.LogError($"Error generating 3D model: {ex.Message}");
        //             return null;
        //         }
        //     }
        //
        //     Debug.LogWarning("Response from SendCommand was null.");
        //     return null;
        // }
        public override async Task Send(CreateModelRequest request)
        {
            byte[] rawData = await DoDownload<CreateModelRequest>(request);
            if (rawData != null)
            {
                await LoadModel(ExtractZipFromMemory(rawData), request.ModelName);
            }
        }
        
        private Dictionary<string, byte[]> ExtractZipFromMemory(byte[] zipBytes)
        {
            Dictionary<string, byte[]> extractedFiles = new Dictionary<string, byte[]>();

            using MemoryStream memoryStream = new MemoryStream(zipBytes);
            using ZipArchive archive = new ZipArchive(memoryStream, ZipArchiveMode.Read);
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                using var entryStream = entry.Open();
                using var ms = new MemoryStream();
                entryStream.CopyTo(ms);
                Debug.Log($"Extracting file {entry.Name}...{entry.FullName}");
                extractedFiles[entry.Name] = ms.ToArray();
            }
            return extractedFiles;
        }

        public async Task<GameObject> LoadModel(Dictionary<string, byte[]> zipFiles, string modelName)
        {
            try
            {
                var textureBytes = zipFiles.GetValueOrDefault("texture.png", null);
                var modelBytes = zipFiles.GetValueOrDefault("mesh.obj", null);
                var mtlBytes = zipFiles.GetValueOrDefault("texture.mtl", null);
                // Define the Resources folder path
                string resourcesPath = Path.Combine(Application.dataPath, "Resources", "GeneratedModels", modelName);
                Directory.CreateDirectory(resourcesPath);

                // Save the .obj file
                string objPath = Path.Combine(resourcesPath, $"{modelName}.obj");
                // await File.WriteAllBytesAsync(objPath, modelBytes);

                // Save the .png file (if any texture is provided)
                string pngPath = string.Empty;
                if (textureBytes != null)
                {
                    pngPath = Path.Combine(resourcesPath, $"{modelName}.png");
                    await File.WriteAllBytesAsync(pngPath, textureBytes);
                }

                MemoryStream modelStream = new MemoryStream(modelBytes);
                GameObject model = null;
                OBJLoader objLoader = new OBJLoader();

                // Save the .mtl file
                if (mtlBytes != null)
                {
                    var matStream = new MemoryStream(mtlBytes);
                    model = objLoader.Load(modelStream);
                    Material material = ParseTexture(mtlBytes, textureBytes);
                    model.GetComponentInChildren<MeshRenderer>().material = material;
                }
                else
                {
                    model = objLoader.Load(modelStream);
                    Debug.Log("Log Model with no texture");
                }

                // Set model name and add required components
                model.name = modelName;
                model.AddComponent<InteractableGameObject>();

                return model;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error generating 3D model: {ex.Message}");
                return null;
            }
        }

        private Texture2D LoadTextureFromMemory(byte[] textureBytes)
        {
            if (textureBytes == null) return null;

            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(textureBytes);
            return tex;
        }
        
        private void ApplyTexture(GameObject obj, Texture2D texture)
        {
            if (texture == null) return;

            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
            foreach (Renderer rend in renderers)
            {
                if (rend.material != null)
                {
                    rend.material.mainTexture = texture;
                }
            }
        }
        
        private Material ParseTexture(byte[] mtlBytes, byte[] textureBytes)
        {
            var mtlData = Encoding.UTF8.GetString(mtlBytes);
            Material material = new Material(Shader.Find("Standard"));
            StringReader reader = new StringReader(mtlData);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] tokens = line.Split(' ');
                if (tokens.Length < 2) continue;

                if (tokens[0] == "Kd") // Diffuse color
                {
                    material.color = new Color(
                        float.Parse(tokens[1]),
                        float.Parse(tokens[2]),
                        float.Parse(tokens[3])
                    );
                }
                else if (tokens[0] == "map_Kd") // Texture map
                {
                    Texture2D texture = LoadTextureFromMemory(textureBytes);
                    if (texture != null)
                    {
                        material.mainTexture = texture;
                    }
                }
            }
            return material;
        }
        
    }
}