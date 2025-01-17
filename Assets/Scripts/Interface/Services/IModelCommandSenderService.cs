using Cysharp.Threading.Tasks;
using DI;
using UnityEngine;

namespace Interface.Services
{
    public interface IModelCommandSenderService : IService
    {
        public UniTask<GameObject> Gen3DModel(string prompt, string name);
    }
}