using SevenMagpies.AppGeneral;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SevenMagpies.UI
{
    public class ToggleWidget : PoolableElement
    {
        [SerializeField]
        private TMPro.TMP_Text _text;
        [SerializeField]
        private Toggle _toggle;

        private Action<bool> _onClickAction;

        public void SetData( string header, bool value, Action<bool> onClickAction )
        {
            _text.text = header;
            _toggle.isOn = value;
            _onClickAction = onClickAction;

            _toggle.onValueChanged.AddListener( OnClickAction );

            gameObject.SetActive( true );
        }

        public void SetValue( bool value )
        {
            _toggle.isOn = value;
        }

        private void OnClickAction( bool value )
        {
            _onClickAction?.Invoke( value );
        }

        public override void Uninit()
        {
            _toggle.onValueChanged.RemoveAllListeners();

            base.Uninit();
        }
    }
}