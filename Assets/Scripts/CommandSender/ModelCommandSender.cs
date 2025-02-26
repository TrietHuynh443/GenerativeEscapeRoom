using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using Dummiesman;
using HttpCommand;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.Screen;

namespace CommandSender
{
    public class ModelCommandSender : CommandSender<CreateModelRequest, CreateModelResponse>
    {
        private readonly string _createModelUrl;
        private readonly string _getModelUrl;
        public ModelCommandSender()
        {
            Url = $"{BaseUrl}/get-model3D-zip";
            _getModelUrl = Url;
            _createModelUrl = $"{BaseUrl}/create-model3D";
            //Test
            // Url = "https://people.sc.fsu.edu/~jburkardt/data/obj/lamp.obj";
        }

        public override async Task Send(CreateModelRequest request)
        {
            byte[] rawData = await DoDownload<CreateModelRequest>(_getModelUrl, request);
            if (rawData != null)
            {
                await LoadModel(ExtractZipFromMemory(rawData), null);
            }
        }

        public async Task<GameObject> GetModelObj(GetModelRequest request)
        {
            int maxRetries = 3; // Maximum retry attempts
            int delay = 1000;   // 1 second delay between retries
    
            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    Debug.Log($"Attempt {attempt}: Downloading model {request.ModelId}");
                    byte[] rawData = await DoDownload<GetModelRequest>(_getModelUrl, request);
            
                    if (rawData != null)
                    {
                        return (await LoadModel(ExtractZipFromMemory(rawData), request.ModelId))?.transform.GetChild(0).gameObject;
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Failed to download model {request.ModelId} (Attempt {attempt}): {ex.Message}");
                }
        
                await Task.Delay(delay); // Wait before retrying
            }

            Debug.LogError($"Model {request.ModelId} failed to download after {maxRetries} attempts.");
            return null;
        }


        public async Task<CreateModelResponse> CreateModel(CreateModelRequest request)
        {
            try
            {
                UnityWebRequest webRequest = CreateRequest(_createModelUrl, UnityWebRequest.kHttpVerbPOST,
                    JsonConvert.SerializeObject(request));
                var res = await DoPost(webRequest);
                return res;
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return null;
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
                entryStream.Close();
                ms.Close();
            }
            memoryStream.Close();
            return extractedFiles;
        }

        public async Task<GameObject> LoadModel(Dictionary<string, byte[]> zipFiles, string modelId)
        {
            try
            {
                var textureBytes = zipFiles.GetValueOrDefault("texture.png", null);
                var modelBytes = zipFiles.GetValueOrDefault("mesh.obj", null);
                var mtlBytes = zipFiles.GetValueOrDefault("texture.mtl", null);
                // Define the Resources folder path
                MemoryStream modelStream = new MemoryStream(modelBytes);
                GameObject model = null;
                OBJLoader objLoader = new OBJLoader();

                // Save the .mtl file
                if (mtlBytes != null)
                {
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
                if (model != null)
                {
                    model.name = modelId;
                    InitModelComponents(model);
                    var cateCommand = new LoadCategoryCommandSender();
                    var res = await cateCommand.GetCate(new()
                    {
                        ObjId = modelId
                    });
                    Debug.Log($"Cate load {res.Category}");
                    model.GetComponentInChildren<InteractableGameObject>().SetConfig(ECategoryType.Class, res.Category);
                }
                modelStream.Close();
                return model;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error generating 3D model: {ex.Message}");
                return null;
            }
        }

        private void InitModelComponents(GameObject model)
        {
            Vector3 initPos = Vector3.zero;
            var camera = Camera.current;

            if (camera != null)
            {
                initPos = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2,
                    camera.nearClipPlane + 1f));
            }

            var child = model.transform.GetChild(0);
            if (child == null)
                return;
            child.transform.position = initPos;
            Debug.Log($"init position: {initPos}");

            var meshCollider = child.gameObject.AddComponent<MeshCollider>();
            meshCollider.convex = true;
            child.gameObject.AddComponent<InteractableGameObject>();
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
                else if (tokens[0] == "Ka") // Ambient color
                {
                    Color ambientColor = new Color(
                        float.Parse(tokens[1]),
                        float.Parse(tokens[2]),
                        float.Parse(tokens[3])
                    );
                    material.SetColor("_EmissionColor", ambientColor); // Unity uses emission for ambient light
                }
                else if (tokens[0] == "Ks") // Specular color
                {
                    Color specularColor = new Color(
                        float.Parse(tokens[1]),
                        float.Parse(tokens[2]),
                        float.Parse(tokens[3])
                    );
                    material.SetColor("_SpecColor", specularColor); // Standard shader specular
                }
            }
            
            Texture2D texture = LoadTextureFromMemory(textureBytes);
            if (texture != null)
            {
                material.mainTexture = texture;
            }

            return material;
        }
        
    }
}