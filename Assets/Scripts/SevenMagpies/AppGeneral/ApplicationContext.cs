using SevenMagpies.Scenes;
using UnityEngine;


namespace SevenMagpies.AppGeneral
{

    public class ApplicationContext : MonoBehaviour, IContext
    {
        [SerializeField]
        private PoolService poolServiceOrigin;


        public void Initialize()
        {

            DontDestroyOnLoad( gameObject );

            var resourceService = new AddressableResourceService();
            DIContainer.Bind<IResourcesService>( resourceService, this );

            var poolService = Instantiate( poolServiceOrigin );
            poolService.gameObject.name = "PoolService";
            poolService.Construct( DIContainer.Get<IResourcesService>() );
            DIContainer.Bind<IPoolService>( poolService, this );
            DontDestroyOnLoad( poolService.gameObject );

            var processObserver = new ProcessObserver();
            DIContainer.Bind<IProcessObserver>( processObserver, this );

            var scenesController = new ScenesController();
            scenesController.Construct( DIContainer.Get<IProcessObserver>() );
            DIContainer.Bind<IScenesController>( scenesController, this );

            var gameStateService = new GameStateService( DIContainer.Get<IScenesController>() );
            DIContainer.Bind<IGameStateService>( gameStateService, this );

            DIContainer.Bind<IPlayerSaveStateProvider>( new PlayerSaveStateProvider(), this );
        }
    }
}