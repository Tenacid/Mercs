using SevenMagpies.AppGeneral;
using SevenMagpies.UI;
using System.Threading.Tasks;
using UnityEngine;

namespace SevenMagpies.Cheats
{
    public class CheatsPanelWidget : PanelWidget
    {
        private bool _spawnersEneabled = true;

        public override void SetData( string header )
        {
            base.SetData( header );

            _closeButton.onClick.RemoveAllListeners();
            _closeButton.onClick.AddListener( Uninit );

            _poolService = DIContainer.Get<IPoolService>();

            SetDataInner();
        }

        private async void SetDataInner()
        {
            await AddClearSaveButton();
            await AddClearSaveButton();
        }

        private async Task AddClearSaveButton()
        {
            var widget = await _poolService.GetResource<ButtonWidget>( UIResourceMap.ButtonWidgetPrefabName );
            widget.transform.SetParent( _content );            
            widget.SetData( ClearSaveAndExit, "Clear Save And Exit" );
            widget.transform.position = UnityEngine.Vector3.zero;
            widget.GetComponent<RectTransform>().localPosition = UnityEngine.Vector3.zero;
            widget.gameObject.SetActive( true );
            _widgets.Add( widget );
        }

        private void ClearSaveAndExit()
        {
            var playerSaveStateProvider = DIContainer.Get<IPlayerSaveStateProvider>();
            playerSaveStateProvider.DeleteState();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }


    }
}