using System;
using Interface.Services;
using UnityEngine;

namespace ModelFactory
{
    public class ModelFactoryMonoService : MonoService
    {
        private Action<GameObject> _onModelCreated;
        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }
    }
}