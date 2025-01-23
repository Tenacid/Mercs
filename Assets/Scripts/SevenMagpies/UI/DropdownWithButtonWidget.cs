using SevenMagpies.AppGeneral;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SevenMagpies.UI
{
    public class DropdownWithButtonWidget : PoolableElement
    {
        [SerializeField]
        private TMPro.TMP_Text _text;
        [SerializeField]
        private TMP_Dropdown _dropdown;
        [SerializeField]
        private Button _button;

        private object[] _values;
        private Action<int> _action;

        public void SetData( object[] values, Action<int> action, string label, int curIndex = 0 )
        {
            _values = values;
            _action = action;

            _text.text = label;

            _dropdown.ClearOptions();
            _dropdown.AddOptions( _values.Select( v => new TMP_Dropdown.OptionData( v.ToString() ) ).ToList() );
            _dropdown.SetValueWithoutNotify( curIndex );

            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener( OnClick );
        }

        private void OnClick()
        {
            _action?.Invoke( _dropdown.value );
        }

        public override void Uninit()
        {
            _values = default;
            _action = default;


            base.Uninit();
        }
    }
}
