using Leopotam.EcsLite;
using SevenMagpies.ECS.Common;
using System;

namespace SevenMagpies.ECS
{
    public class EcsService : IDisposable
    {
        private EcsWorld _world;
        private IEcsSystems _systems;
        private int _utilityEntity;


        public EcsService( EcsWorld world )
        {
            _world = world;

            _utilityEntity = _world.NewEntity();
            ref var timer = ref _world.GetPool<GlobalTimer>().Add( _utilityEntity );
            timer.Time = 0;


            _systems = new EcsSystems( _world );
            _systems
#if UNITY_EDITOR
         .Add( new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem( "events" ) )
#endif
        .Init();
        }




        public void UpdateLoop( float deltaTime ) { }


        public void Dispose()
        {
            if ( _systems != null )
            {
                _systems.Destroy();
                _systems = null;
            }

            if ( _world != null )
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}
