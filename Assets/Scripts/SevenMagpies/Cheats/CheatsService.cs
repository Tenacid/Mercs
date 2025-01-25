using SevenMagpies.AppGeneral;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;


namespace SevenMagpies.Cheats
{
    public class CheatsService : MonoBehaviour
    {
        [SerializeField]
        private Canvas _canvas;
        [SerializeField]
        private Button _cheatsButton;

        private const string CheatsPanelWidgetName = "CheatsPanelWidget";
        private const string CheatsPanelHeader = "Cheats";

        void Start()
        {
            _cheatsButton.onClick.RemoveAllListeners();
            _cheatsButton.onClick.AddListener(OpenCheatsPanel);

            DontDestroyOnLoad( gameObject );
        }

        private async void OpenCheatsPanel() 
        {
            var poolService = DIContainer.Get<IPoolService>();

            var panel = await poolService.GetResource<CheatsPanelWidget>( CheatsPanelWidgetName );
            panel.transform.SetParent(_canvas.transform, false );
            panel.SetData( CheatsPanelHeader );
            panel.gameObject.SetActive( true );
        }
    }
}