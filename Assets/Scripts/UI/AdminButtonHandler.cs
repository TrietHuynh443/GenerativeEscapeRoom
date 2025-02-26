using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandSender;
using Cysharp.Threading.Tasks;
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

        private List<TMP_Dropdown.OptionData> _categoryOptions = new();
        private void Awake()
        {
            InitCateClassDropdownList();
        }

        private void InitCateClassDropdownList()
        {
            var cates = Enum.GetValues(typeof(EObjectClass));
            foreach (var cate in cates)
            {
                _categoryOptions.Add(new TMP_Dropdown.OptionData(cate.ToString()));
            }
            _categoryDropdown.options = _categoryOptions;

        }

        private readonly RoomSaveCommandSender _roomSaveCommandSender = new();
    
        [Injector]
        private readonly IEventHandlerService _eventAggregator;

        private GameObject _lastInteract;
        private string _filePath;

        private async void Start()
        {
            _filePath = Path.Combine(Application.dataPath, "Resources/data.json");
            _creatModelButton.onClick.AddListener(RaiseGenModelOnClickEvent);
            _saveModelButton.onClick.AddListener(SaveModelOnClickEvent);
            StartCoroutine(Wait());
        
        }

        private void SaveModelOnClickEvent()
        {
            _ = SaveModel();
        }

        private async UniTaskVoid SaveModel()
        {
            var newData = new RoomData
            {
                Name = _lastInteract != null ? _lastInteract.name : string.Empty,
                ObjectPosition = _lastInteract != null ? new Vector3S(_lastInteract.transform.position) : new Vector3S(Vector3.zero),
                ObjectClass = _categoryDropdown.captionText.text,
                ObjectRotation = _lastInteract != null ? new Vector3S(_lastInteract.transform.eulerAngles) : new Vector3S(Vector3.zero),
            };

            await _roomSaveCommandSender.Send(new()
            {
                ModelParam = _lastInteract != null ? _lastInteract.name : "the_monkey_queen",
                RoomParams = newData
            });

        }

        private IEnumerator Wait()
        {
            yield return new WaitForEndOfFrame();
            _eventAggregator.AddEventListener<OnDraggingInteractableObjEvent>(GetInteractableSuccessAction);
        }

        private void GetInteractableSuccessAction(OnDraggingInteractableObjEvent obj)
        {
            if (obj.InteractableObj)
            {
                _lastInteract = obj.InteractableObj.gameObject;
                _saveModelButton.interactable = true;
                _categoryDropdown.interactable = true;
                Enum.TryParse<EObjectClass>(_lastInteract.GetComponent<InteractableGameObject>().GetConfig(ECategoryType.Class), out EObjectClass type);
                _categoryDropdown.SetValueWithoutNotify((int)type);
            }
        }

        private void OnDestroy()
        {
            _creatModelButton.onClick.RemoveListener(RaiseGenModelOnClickEvent);
            _eventAggregator.RemoveEventListener<OnDraggingInteractableObjEvent>(GetInteractableSuccessAction);
        }

        private void RaiseGenModelOnClickEvent()
        {
            OnCreateNewModelEvent evt = new OnCreateNewModelEvent()
            {
                Prompt = _prompt.text
            };
            Debug.Log(evt.ToString());
            _eventAggregator.RaiseEvent(evt);
        }
    }
}