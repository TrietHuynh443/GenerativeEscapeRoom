using CommandSender;
using Interface.MonoBehaviorServices;
using Interface.Services;
using Manager;
using UnityEngine;

namespace DI
{
    [DefaultExecutionOrder(-100)] //Make this execute first
    public class GameInstaller : MonoBehaviour
    {
        private DependenciesProvider _dependenciesProvider;
        private static GameInstaller _instance;
        [Injector]
        private readonly PointerController _pointerController;
        [Injector]
        private readonly GameManager _gameManager;
        private void Awake()
        {
            _dependenciesProvider = gameObject.AddComponent<DependenciesProvider>();
            _dependenciesProvider.Register<ILogService>(() => new LogService());
            _dependenciesProvider.Register<IModelCommandSenderService>(() => new ModelCommandSender());
            _dependenciesProvider.Register<IEventHandlerService>(() => new EventAggregator());
            
            _dependenciesProvider.Register<ExampleMonoServices>(null);
            _dependenciesProvider.Register<PointerController>(null);
            _dependenciesProvider.Register<GameManager>(null);
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
            LoadAllNetworkObjects();
        }

        private static void LoadAllNetworkObjects()
        {
            //call to server get data and model
            //load to scene
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