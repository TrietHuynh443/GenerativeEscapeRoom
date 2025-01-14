using Interface.Services;
using UnityEngine;

namespace DI
{
    public class ExampleDI: MonoBehaviour
    {
        [Injector]
        private readonly ILogService _logger;

        private void Start()
        {
            _logger.Log($"Start {_logger.GetHashCode()}");
        }
    }
}