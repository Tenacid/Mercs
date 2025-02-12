using SevenMagpies.AppGeneral;
using System;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace SevenMagpies.Scenes
{
    public class ScenesController : IScenesController
    {
        const string MatchSceneName = "MatchScene";
        const string LoadingSceneName = "LoadingScene";
        const string MetaSceneName = "MetaScene";

        const float ProgressWeightSceneLoading = 0.5f;
        const float ProgressWeightSceneStarting = 0.5f;

        private IProcessObserver _loadingObserver;

        private float _scenePreparationProgress;

        public event Action BeforeChangeScene;

        public void Construct( IProcessObserver loadingObserver )
        {
            _loadingObserver = loadingObserver;
        }

        public void LoadMatchScene( Action onComplete = null )
        {
            LoadSceneInner( MatchSceneName, onComplete, new MatchSceneContext() );
        }

        public void LoadMetaScene( Action onComplete = null )
        {
            LoadSceneInner( MetaSceneName, onComplete, new MetaSceneStarter() );
        }

        private async void LoadSceneInner( string sceneName, Action onComplete, SceneContext sceneStarter )
        {
            _loadingObserver.ChangeProgress( 0 );

            BeforeChangeScene?.Invoke();            

            var loadingSceneHandler = Addressables.LoadSceneAsync( LoadingSceneName, LoadSceneMode.Single );
            while ( !loadingSceneHandler.IsDone )
            { 
                await Task.Yield();
            }

            var handler = Addressables.LoadSceneAsync( sceneName, LoadSceneMode.Additive );

            while ( !handler.IsDone )
            {
                _loadingObserver.ChangeProgress( handler.PercentComplete/100f * ProgressWeightSceneLoading );
                await Task.Yield();
            }

            await sceneStarter.Init( UpdateSceneStartProgress );

            _loadingObserver.ChangeProgress( 1f );

            var unloadingSceneHandler = Addressables.UnloadSceneAsync( loadingSceneHandler );
            while ( !unloadingSceneHandler.IsDone )
            {
                await Task.Yield();
            }

            onComplete?.Invoke();
        }

        private void UpdateSceneStartProgress( float progress ) 
        {
            _loadingObserver.ChangeProgress( ProgressWeightSceneLoading + progress * ProgressWeightSceneStarting );
        }
    }
}
