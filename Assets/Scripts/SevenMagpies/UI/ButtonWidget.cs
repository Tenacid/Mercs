using SevenMagpies.AppGeneral;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SevenMagpies.UI
{
    public class ButtonWidget : PoolableElement
    {
        [SerializeField]
        private TMPro.TMP_Text _text;
        [SerializeField]
        private Button _button;

        private Action _action;

        public void SetData( Action action, string label )
        {
            _action = action;
            _text.text = label;

            _button.onClick.AddListener( OnClick );
        }

        private void OnClick()
        {
            _action?.Invoke();
        }

        public override void Uninit()
        {
            _action = default;
            _button.onClick.RemoveAllListeners();

            base.Uninit();
        }
    }
}