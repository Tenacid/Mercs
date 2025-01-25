using System;
using System.Threading.Tasks;
using UnityEngine;

namespace SevenMagpies.Scenes
{
    public abstract class SceneStarter
    {
        protected bool _sceneMarkupFounded;
        protected SceneMarkup _sceneMarkup;

        public virtual async Task PrepareScene( Action<float> updateProgressCallback )
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

            updateProgressCallback?.Invoke( 0.5f );

            await _sceneMarkup.Prewarm();

            updateProgressCallback?.Invoke( 1f );
        }
    }
}
