using System.Collections.Generic;
using UnityEngine;

namespace SevenMagpies.Application
{
    public class PoolableElement : MonoBehaviour
    {
        private bool _active;
        private string _resourceName;

        protected List<PoolableElement> _childElements;

        protected IPoolService _poolService;
        protected IResourcesService _resourcesService;

        public string ResourceName { get => _resourceName; set => _resourceName = value; }
        public bool Active { get => _active; set => _active = value; }

        public virtual void Construct( IPoolService poolService, IResourcesService resourcesService )
        {
            _poolService = poolService;
            _resourcesService = resourcesService;
        }

        public virtual void Init()
        {
            Active = true;
            _childElements = new List<PoolableElement>();
        }

        public virtual void Uninit()
        {
            if ( !Active )
            {
                return;
            }

            Active = false;

            foreach ( var child in _childElements )
            {
                child.Uninit();
            }

            _childElements = null;

            gameObject.SetActive( false );
            _poolService.RemoveToPool( this );
        }

        public override bool Equals( object other )
        {
            if ( !( other is PoolableElement ) )
            {
                return false;
            }

            var casted = ( PoolableElement ) other;

            return this.gameObject.GetInstanceID() == casted.gameObject.GetInstanceID();
        }

        public override int GetHashCode()
        {
            return this.gameObject.GetInstanceID();
        }
    }
}
