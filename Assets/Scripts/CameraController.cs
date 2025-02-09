using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DI;
using Interface.Services;
using UnityEngine;

public class CameraController : UnitySingleton<CameraController>
{
    [SerializeField] private GameObject _oculusCamera;

    [SerializeField] private GameObject _cheatCamera;
    
    // [Injector] private LogService _logService;
    private void Start()
    {
#if UNITY_EDITOR_WIN
        _cheatCamera.SetActive(true);
        Camera.SetupCurrent(_cheatCamera.GetComponent<Camera>());
        // _logService.Log("Camera opened");
#else
         _cheatCamera.SetActive(false);
         _oculusCamera.SetActive(true);
#endif
    }

    public static void Disable()
    {
    }
}
