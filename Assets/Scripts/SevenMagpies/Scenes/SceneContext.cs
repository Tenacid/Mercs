using SevenMagpies.AppGeneral;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace SevenMagpies.Scenes
{
    public abstract class SceneContext: IExclusiveContext
    {
        protected bool _inited;
        protected bool _sceneMarkupFounded;
        protected SceneMarkup _sceneMarkup;

        protected async Task PrepareMarkup( float progressWeight, Action<float> updateProgressCallback ) 
        {
            _sceneMarkupFounded = false;
            _sceneMarkup = null;

            while ( !_sceneMarkupFounded )
            {
                _sceneMarkup = GameObject.FindFirstObjectByType<SceneMarkup>();

                if ( _sceneMarkup != null )
                {
                    _sceneMarkupFounded = true;
                }
                else
                {
                    await Task.Delay( 100 );
                }
            }

            updateProgressCallback?.Invoke( progressWeight/2f );

            await _sceneMarkup.Prewarm();

            updateProgressCallback?.Invoke( progressWeight );
        }

        public virtual async Task Init( Action<float> updateProgressCallback )
        {
            if ( _inited )
            {
                throw new Exception( $"{this.GetType().Name} already inited" );
            }

            _inited = true;

            DIContainer.Bind<IExclusiveContext>( this, this );
        }
    }
}
