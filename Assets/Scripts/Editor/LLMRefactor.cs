using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class LLMStructureGUI : EditorWindow
{
    private string _apiKeyText = "this is api key";
    private int selectedIndex = 0;
    private string[] _apiTypes;


    [MenuItem("Window/Basic Editor GUI")]
    public static void ShowWindow()
    {
        GetWindow<LLMStructureGUI>("LLM Model Settings");
    }

    private void OnEnable()
    {
        _apiTypes = Enum.GetNames(typeof(ELLMApiType));
    }

    private void OnGUI()
    {
        GUILayout.Label("Custom Editor GUI", EditorStyles.boldLabel);
        _apiKeyText = EditorGUILayout.TextField("Api Key", _apiKeyText);
        selectedIndex = EditorGUILayout.Popup("LLM Model: ", selectedIndex, _apiTypes);
        if (GUILayout.Button("Save Settings"))
        {
            Debug.Log("Button clicked with value: " + _apiKeyText + " and selected index: " + _apiTypes[selectedIndex]);

            Addressables.LoadAssetAsync<LLMConfigSO>("config").Completed += (asset) =>
            {
                if (asset.Status == AsyncOperationStatus.Succeeded)
                {
                    asset.Result.ApiKey = _apiKeyText;
                    asset.Result.ApiType = Enum.Parse<ELLMApiType>(_apiTypes[selectedIndex]);
#if UNITY_EDITOR
                    EditorUtility.SetDirty(asset.Result);
                    AssetDatabase.SaveAssets();
#endif
                }
                else
                {
                    throw new System.Exception("Failed to load LLM Config");
                }
            };
            
        }
    }
}
