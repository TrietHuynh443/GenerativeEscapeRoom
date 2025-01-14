using DI;
using Interface.Services;
using UnityEngine;

namespace Interface.MonoBehaviorServices
{
    public class ExampleMonoServices : MonoService
    {
        [Injector]
        private readonly ILogService _logService;
        private void Start()
        {
            Debug.Log($"this is {this.GetType().Name}");
            // _logService.Log($"Start {_logService.GetHashCode()}");
        }
    }
}