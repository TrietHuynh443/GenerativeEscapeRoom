using UnityEngine;

namespace DI
{
    public class LogService : IService
    {
        public void Log(string message)
        {
            Debug.Log(message);
        }
    }
}