using Leopotam.EcsLite;
using SevenMagpies.AppGeneral;
using SevenMagpies.ECS;
using System;
using System.Threading.Tasks;

namespace SevenMagpies.Scenes
{
    public class MatchSceneContext : SceneContext, IUpdatable, IDisposable
    {
        private EcsService _ecsService;
        private IUpdateProcessor _updateProcessor;

        public override async Task Init( Action<float> updateProgressCallback )
        {
            await base.Init( updateProgressCallback );

            await PrepareMarkup( 0.5f, updateProgressCallback );

            _updateProcessor = DIContainer.Get<IUpdateProcessor>();

            Register(_updateProcessor);

            DIContainer.Bind<EcsWorld>( new EcsWorld(), this );

            _ecsService = new EcsService(DIContainer.Get<EcsWorld>());
        }

        public void Update( float deltaTime )
        {
            if ( !_inited )
            {
                return;
            }

            if ( _ecsService == null )
            {
                return;
            }

            _ecsService.UpdateLoop( deltaTime );
        }

        public void Register( IUpdateProcessor updateProcessor )
        {
            updateProcessor.Add( this );
        }

        public void Dispose()
        {
            Unregister( _updateProcessor );
        }

        public void Unregister( IUpdateProcessor updateProcessor )
        {
            updateProcessor.Remove( this );
        }
    }
}