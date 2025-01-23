using SevenMagpies.AppGeneral;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SevenMagpies.UI
{
    public class PanelWidget : PoolableElement
    {
        [SerializeField]
        protected Transform _content;
        [SerializeField]
        protected Button _closeButton;
        [SerializeField]
        protected TMPro.TMP_Text _header;

        protected List<PoolableElement> _widgets;

        public virtual void SetData( string header )
        {
            _header.text = header;
            _widgets = new List<PoolableElement>();

            gameObject.SetActive( true );
        }

        public override void Init()
        {
            base.Init();
            _closeButton.onClick.AddListener( Uninit );
        }

        public override void Uninit()
        {
            _closeButton.onClick.RemoveAllListeners();
            _header.text = default;

            foreach ( var w in _widgets )
            {
                w.Uninit();
            }
            _widgets.Clear();

            base.Uninit();
        }
    }
}
