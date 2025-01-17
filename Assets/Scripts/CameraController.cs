using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DI;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _oculusCamera;

    [SerializeField] private GameObject _cheatCamera;
    
    [Injector] private LogService _logService;
    private void Start()
    {
#if UNITY_EDITOR_WIN
        _cheatCamera.SetActive(true);
        _logService.Log("Camera opened");
#else
         _cheatCamera.SetActive(false);
         _oculusCamera.SetActive(true);
#endif
    }
}
