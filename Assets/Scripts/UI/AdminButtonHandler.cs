using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DI;
using EventProcessing;
using Interface.Services;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    
    public class AdminButtonHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _prompt;
        [SerializeField] private Button _creatModelButton;
        [SerializeField] private Button _saveModelButton;
        [SerializeField] private TMP_Dropdown _categoryDropdown;

        private List<TMP_Dropdown.OptionData> _categoryOptions = new()
        {
            new TMP_Dropdown.OptionData("Normal"),
            new TMP_Dropdown.OptionData("Reuse"),
            new TMP_Dropdown.OptionData("Recycle"),
        };
    
        [Injector]
        private readonly IEventHandlerService _eventAggregator;

        private GameObject _lastInteract;
        private string _filePath;

        private void Start()
        {
            _filePath = Path.Combine(Application.dataPath, "Resources/data.json");
            _creatModelButton.onClick.AddListener(RaiseGenModelOnClickEvent);
            _saveModelButton.onClick.AddListener(SaveModelOnClickEvent);
            StartCoroutine(Wait());
        
        }

        private void SaveModelOnClickEvent()
        {
            List<Data> dataList = new List<Data>();
            var newData = new Data
            {
                Name = _lastInteract != null ? _lastInteract.name : string.Empty,
                ObjectPosition = _lastInteract != null ? new Vector3S(_lastInteract.transform.position) : new Vector3S(Vector3.zero),
                ObjectClass = _categoryDropdown.captionText.text,
                ObjectRotation = _lastInteract != null ? new Vector3S(_lastInteract.transform.eulerAngles) : new Vector3S(Vector3.zero),
            };
            // Load existing data if file exists
            if (File.Exists(_filePath))
            {
                string existingJson = File.ReadAllText(_filePath);
                dataList = JsonConvert.DeserializeObject<List<Data>>(existingJson) ?? new List<Data>();
            }

            foreach (Data data in dataList.ToList())
            {
                if (data.Name == newData.Name)
                {
                    dataList.Remove(data);
                    break;
                }
            }
            // Add new data and write back to file
            dataList.Add(newData);
            File.WriteAllText(_filePath, JsonConvert.SerializeObject(dataList, Formatting.Indented));

        }

        private IEnumerator Wait()
        {
            yield return new WaitForEndOfFrame();
            _eventAggregator.AddEventListener<OnDraggingInteractableObjEvent>(GetInteractableSuccessAction);
        }

        private void GetInteractableSuccessAction(OnDraggingInteractableObjEvent obj)
        {
            if (obj.InteractableObj == null)
            {
                // _saveModelButton.interactable = false;
                // _categoryDropdown.interactable = false;
            }
            else
            {
                _lastInteract = obj.InteractableObj.gameObject;
                _saveModelButton.interactable = true;
                _categoryDropdown.interactable = true;
                _categoryDropdown.options = _categoryOptions;
            }
        }

        private void OnDestroy()
        {
            _creatModelButton.onClick.RemoveListener(RaiseGenModelOnClickEvent);
            _eventAggregator.RemoveEventListener<OnDraggingInteractableObjEvent>(GetInteractableSuccessAction);
        }

        public void RaiseGenModelOnClickEvent()
        {
            OnCreateNewModelEvent evt = new OnCreateNewModelEvent()
            {
                Prompt = _prompt.text
            };
            _eventAggregator.RaiseEvent(evt);
        }
    }
}