using SevenMagpies.Application;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

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

        var gameStateService = DIContainer.Get<IGameStateService>();
        gameStateService.ToMeta();
    }
}
