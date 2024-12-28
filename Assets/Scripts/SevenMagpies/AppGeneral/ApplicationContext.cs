using SevenMagpies.AppGeneral;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;


namespace SevenMagpies.AppGeneral
{

    public class ApplicationContext : MonoBehaviour, IContext
    {

        public void Initialize()
        {

            DontDestroyOnLoad( gameObject );

            var resourceService = new AddressableResourceService();
            DIContainer.Bind<IResourcesService>( resourceService, this );

            var poolServiceGO = new GameObject();
            poolServiceGO.name = "PoolService";
            var poolService = poolServiceGO.AddComponent<PoolService>();
            poolService.Construct( DIContainer.Get<IResourcesService>() );
            DIContainer.Bind<IPoolService>( poolService, this );
            DontDestroyOnLoad( poolServiceGO );

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