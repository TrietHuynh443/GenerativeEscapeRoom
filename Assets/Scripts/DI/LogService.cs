using Interface.Services;
using UnityEngine;

namespace DI
{
    public class LogService : ILogService
    {
        public void Log(string message)
        {
            Debug.Log(message);
        }
    }
}