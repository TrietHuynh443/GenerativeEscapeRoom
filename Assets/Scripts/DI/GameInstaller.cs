using Interface.MonoBehaviorServices;
using Interface.Services;
using UnityEngine;

namespace DI
{
    [DefaultExecutionOrder(-100)] //Make this execute first
    public class GameInstaller : MonoBehaviour
    {
        private DependenciesProvider _dependenciesProvider;
        private static GameInstaller _instance;
        private void Awake()
        {
            _dependenciesProvider = gameObject.AddComponent<DependenciesProvider>();
            _dependenciesProvider.Register<ILogService>(() => new LogService());
            _dependenciesProvider.Register<ExampleMonoServices>(null);
            //Add order dependencies here
        }
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            // Ensure only one instance of GameInstaller exists
            if (_instance != null) return;

            // Create a new GameObject for the GameInstaller
            var gameInstallerGameObject = new GameObject("GameInstaller");
            _instance = gameInstallerGameObject.AddComponent<GameInstaller>();

            DontDestroyOnLoad(gameInstallerGameObject);
        }

        private void Start()
        {
            _dependenciesProvider.AutoInjectAll();
        }
    } 
    // public class DIExample : MonoBehaviour
// {
//     private void Start()
//     {
//         var provider = new DependenciesProvider();
//         provider.Register<IService>(() => new Service());
//         provider.Register<Consumer>(() => new Consumer());
//
//         var consumer = provider.Resolve<Consumer>();
//         provider.Inject(consumer);
//         consumer.UseService();
//     }
// }
}