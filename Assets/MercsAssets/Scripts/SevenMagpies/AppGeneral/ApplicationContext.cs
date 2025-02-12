using SevenMagpies.Scenes;
using System.Collections.Generic;
using UnityEngine;


namespace SevenMagpies.AppGeneral
{
    public class ApplicationContext : MonoBehaviour, IContext, IUpdateProcessor
    {
        [SerializeField]
        private PoolService poolServiceOrigin;

        private List<IUpdatable> _updatables;
        
        public void Initialize()
        {
            DontDestroyOnLoad( gameObject );

            _updatables = new List<IUpdatable>();

            DIContainer.Bind<IUpdateProcessor>( this, this );

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
            scenesController.BeforeChangeScene += DIContainer.TryUninitExclusiveContext;
            DIContainer.Bind<IScenesController>( scenesController, this );

            var gameStateService = new GameStateService( DIContainer.Get<IScenesController>() );
            DIContainer.Bind<IGameStateService>( gameStateService, this );

            DIContainer.Bind<IPlayerSaveStateProvider>( new PlayerSaveStateProvider(), this );
        }

        public void Add( IUpdatable updatable )
        {
            _updatables.Add( updatable );
        }

        public void Remove( IUpdatable updatable )
        {
            _updatables.Remove( updatable );
        }

        private void Update()
        {
            for ( var i = 0; i < _updatables.Count; i++ )
            {
                _updatables[ i ].Update( Time.deltaTime );
            }
        }
    }
}