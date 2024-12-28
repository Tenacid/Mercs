using SevenMagpies.AppGeneral;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;


namespace SevenMagpies.AppGeneral
{
    public class GameStarter : MonoBehaviour
    {
        private const string ApplicationContextName = "ApplicationContext";

        void Start()
        {
            Application.targetFrameRate = 60;

            StartCoroutine( StartingRoutine() );
        }

        private IEnumerator StartingRoutine()
        {
            var handler = Addressables.LoadAssetAsync<GameObject>( ApplicationContextName );
            while ( !handler.IsDone )
            {
                yield return null;
            }
            var applicationContext = handler.Result.GetComponent<ApplicationContext>();
            applicationContext.Initialize();

            var stateProvider = DIContainer.Get<IPlayerSaveStateProvider>();
            stateProvider.PrepareState();

            if ( stateProvider.FirstLaunchCompleted )
            {
                var gameStateService = DIContainer.Get<IGameStateService>();
                gameStateService.ToMeta();
            }
            else
            {
                //run what you need for first launch   
            }            
        }
    }
}