using System;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace SevenMagpies.Application
{
    public class ScenesController : IScenesController
    {
        const string MatchSceneName = "MatchScene";
        const string LoadingSceneName = "LoadingScene";
        const string MetaSceneName = "MetaScene";

        private IProcessObserver _loadingObserver;

        public void Construct( IProcessObserver loadingObserver )
        {
            _loadingObserver = loadingObserver;
        }


        public void LoadMatchScene( Action onComplete = null )
        {
            LoadSceneInner( MatchSceneName, onComplete );
        }

        public void LoadMetaScene( Action onComplete = null )
        {
            LoadSceneInner( MetaSceneName, onComplete );
        }

        private async void LoadSceneInner( string sceneName, Action onComplete )
        {
            _loadingObserver.ChangeProgress( 0 );

            var loadingSceneHandler = Addressables.LoadSceneAsync( LoadingSceneName, LoadSceneMode.Single );
            while ( !loadingSceneHandler.IsDone )
            { 
                await Task.Yield();
            }

            var handler = Addressables.LoadSceneAsync( sceneName, LoadSceneMode.Single );

            while ( !handler.IsDone )
            {
                _loadingObserver.ChangeProgress( handler.PercentComplete/100f );
                await Task.Yield();
            }

            _loadingObserver.ChangeProgress( 1f );

            onComplete?.Invoke();
        }
    }
}
