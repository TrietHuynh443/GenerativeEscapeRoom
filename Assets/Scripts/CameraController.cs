using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _oculusCamera;

    [SerializeField] private GameObject _cheatCamera;

    private void Start()
    {
// #if UNITY_EDITOR_WIN
//         _cheatCamera.SetActive(true);
// #else
//         _cheatCamera.SetActive(false);
//         _oculusCamera.SetActive(true);
// #endif
    }
}
