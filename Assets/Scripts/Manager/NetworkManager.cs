using CommandSender;
using Factory;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class NetworkManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string Input { get; set; } = "";

    private LLMConfigSO _config;
    private LLMStructureCommand _llmStructureCommand;
    private LLMRequestFactory _llmRequestFactory = new();
    private void OnEnable()
    {
        Addressables.LoadAssetAsync<LLMConfigSO>("config").Completed += (asset) =>
        {
            if (asset.Status == AsyncOperationStatus.Succeeded)
            {
                _config = asset.Result;
                _llmStructureCommand = new LLMStructureCommand();

                _llmStructureCommand.Send(_config.Request);
            }
            else
            {
                throw new System.Exception("Failed to load config");
            }
        };
    }
    
}
