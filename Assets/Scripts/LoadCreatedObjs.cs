using System.Collections.Generic;
using Newtonsoft.Json;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
[System.Serializable]
    public class Data
    {
        [JsonProperty("object_name")]
        public string Name { get; set; }
        [JsonProperty("position")]
        public Vector3S ObjectPosition { get; set; }
        [JsonProperty("cate")]
        public string ObjectClass { get; set; }
        [JsonProperty("rotation")]
        public Vector3S ObjectRotation { get; set; }
    }

    [System.Serializable]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public struct Vector3S
    {
        #region Constructor

        public Vector3S(Vector3 vector3)
        {
            _x = vector3.x;
            _y = vector3.y;
            _z = vector3.z;
        }
       
        public Vector3S(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        #endregion

        #region Inspector Fields
        [SerializeField, JsonProperty]
        public float _x;

        [SerializeField, JsonProperty]
        public float _y;

        [SerializeField, JsonProperty]
        public float _z;

        #endregion

        #region Properties
        public float X { get => _x; set => _x = value; }
        public float Y { get => _y; set => _y = value; }
        public float Z { get => _z; set => _z = value; }

        /// <summary>
        /// Returns the Vector3S as a Unity Vector3.
        /// </summary>
        public Vector3 AsVector3 => new(_x, _y, _z);

        #endregion
    }

public class LoadCreatedObjs : MonoBehaviour
{
    private string _filePath;
    private List<ClassifyObject> _classifyObjects;
    public ClassifyGame classifyGame;
    public GameObject AudioGrabbableOVRObject;
    private void Start()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("data"); // No .json extension needed
        if (jsonFile == null)
        {
            Debug.LogError("JSON file not found in Resources!");
            return;
        }
        Debug.Log(jsonFile.text);
        var dataList = JsonConvert.DeserializeObject<List<Data>>(jsonFile.text);
        _classifyObjects = new List<ClassifyObject>();
        foreach (Data data in dataList)
        {
            Debug.Log(data.Name);
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/{data.Name}"); // Load by class name
            if (prefab == null)
            {
                Debug.LogError($"Prefab {data.ObjectClass} not found in Resources/Prefabs/");
                continue;
            }

            GameObject instance = Instantiate(prefab);
            instance.transform.SetPositionAndRotation(data.ObjectPosition.AsVector3,
                Quaternion.Euler(data.ObjectRotation.X, data.ObjectRotation.Y, data.ObjectRotation.Z));
            instance.name = data.Name;

            var interactable = instance.AddComponent<InteractableGameObject>();
            interactable.SetConfig(ECategoryType.Class, data.ObjectClass);

            var colliderRidgid = instance.AddComponent<MeshCollider>();
            colliderRidgid.convex = true;

            var rb = instance.AddComponent<Rigidbody>();
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.drag = 5f;

            instance.AddComponent<GrabbableObject>();
            instance.layer = 12;

            instance.AddComponent<LoadTagClassifyObject>();

            AttachOculusInstance(instance);
            
            _classifyObjects.Add(instance.GetComponent<LoadTagClassifyObject>());
        }

        classifyGame.SetObjectList(_classifyObjects);
    }

    public List<ClassifyObject> GetClassifyObjects()
    {
        return _classifyObjects;
    }

    private void AttachOculusInstance(GameObject instance)
    {
        // Audio
        GameObject audioGrabbableOvrObjectInstance = Instantiate(AudioGrabbableOVRObject);
        instance.AddComponent<Grabbable>();
        audioGrabbableOvrObjectInstance.transform.SetParent(instance.transform);
        audioGrabbableOvrObjectInstance.transform.localPosition = Vector3.zero;
        audioGrabbableOvrObjectInstance.transform.localRotation = Quaternion.identity;

        var pointableUEW = instance.AddComponent<PointableUnityEventWrapper>();
        pointableUEW.InjectPointable(instance.GetComponent<Grabbable>());

        // Add audio to PointableEvent when Select
        // var audioSource = audioGrabbableOvrObjectInstance.GetComponent<AudioSource>();
        // pointableUEW.WhenSelect.AddListener(evt =>
        // {
        //     audioSource.Play();
        // });

        // Visual
        GameObject visual = new GameObject("Visual");
        visual.transform.SetParent(instance.transform);
        visual.transform.localPosition = Vector3.zero;
        visual.transform.localRotation = Quaternion.identity;
        visual.transform.localScale = Vector3.one;

        GameObject root = new GameObject("Root");
        root.transform.SetParent(visual.transform);
        root.transform.localPosition = Vector3.zero;
        root.transform.localRotation = Quaternion.identity;
        root.transform.localScale = Vector3.one;

        var rootMeshFilter = root.AddComponent<MeshFilter>();    // chua duoc
        rootMeshFilter.sharedMesh = instance.GetComponent<MeshFilter>().sharedMesh;

        var rootMeshRenderer = root.AddComponent<MeshRenderer>();  // chua duoc
        rootMeshRenderer.sharedMaterials = instance.GetComponent<MeshRenderer>().sharedMaterials;

        var rootCollider = root.AddComponent<MeshCollider>();
        rootCollider.convex = true;

        var visualMaterialPropertyBlockEditor = visual.AddComponent<MaterialPropertyBlockEditor>();
        List<Renderer> materials = new List<Renderer>();
        foreach (var renderer in instance.GetComponentsInChildren<Renderer>())
        {
            materials.AddRange(renderer.GetComponentsInChildren<Renderer>());
        }
        visualMaterialPropertyBlockEditor.Renderers = materials;

        // HandGrab
        GameObject handGrabInteractable = new GameObject("HandGrabInteractable");
        handGrabInteractable.transform.SetParent(instance.transform);
        handGrabInteractable.transform.localPosition = Vector3.zero;
        handGrabInteractable.transform.localRotation = Quaternion.identity;
        handGrabInteractable.transform.localScale = Vector3.one;

        var handGrabInteractableScript = handGrabInteractable.AddComponent<HandGrabInteractable>();
        handGrabInteractableScript.InjectOptionalPointableElement(instance.GetComponent<Grabbable>());
        handGrabInteractableScript.InjectRigidbody(instance.GetComponent<Rigidbody>());
        handGrabInteractableScript.HandAlignment = HandAlignType.None;

        var interactGroupView = visual.AddComponent<InteractableGroupView>();
        List<IInteractable> interactables = new List<IInteractable>();
        interactables.Add(handGrabInteractableScript);
        interactGroupView.InjectInteractables(interactables);

        var colorVisual = visual.AddComponent<InteractableColorVisual>();
        colorVisual.InjectInteractableView(interactGroupView);
        colorVisual.InjectMaterialPropertyBlockEditor(visualMaterialPropertyBlockEditor);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
