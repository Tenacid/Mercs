using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace SevenMagpies.Application
{
    public class PoolService : MonoBehaviour, IPoolService
    {
        private IResourcesService _resourcesService;

        private Dictionary<string, Stack<PoolableElement>> _pool;

        public void Construct( IResourcesService iGameobjectResourcesHolder )
        {

            _resourcesService = iGameobjectResourcesHolder;

            _pool = new Dictionary<string, Stack<PoolableElement>>();

            transform.position = new Vector3( 0, 0, -10000 );

            enabled = false;
        }

        public void Dispose()
        {
            ClearPool();
        }

        private void ClearPool()
        {
            foreach ( var kv in _pool )
            {
                kv.Value.Clear();
            }

            _pool.Clear();

            _resourcesService = null;
        }

        public async Task<T> GetResource<T>( string resourceName ) where T : PoolableElement
        {
            T resource = default;
            bool resourceExists = false;

            if ( _pool.TryGetValue( resourceName, out var list ) )
            {
                if ( list.Count > 0 )
                {
                    resource = ( T ) list.Pop();
                    resourceExists = true;
                }
            }

            if ( !resourceExists )
            {

                if ( _resourcesService.Contains( resourceName ) )
                {
                    var origin = await _resourcesService.GetResourceAsync<T>( resourceName );

                    resource = Instantiate( origin, transform );
                    resource.ResourceName = resourceName;
                    resourceExists = true;
                }
            }

            if ( !resourceExists )
            {
                throw new System.Exception( $"PoolService can't create instance of {resourceName}" );
            }

            resource.Construct( this, _resourcesService );
            resource.Init();

            return resource;
        }

        public void RemoveToPool( PoolableElement element )
        {
            if ( !_pool.TryGetValue( element.ResourceName, out var list ) )
            {
                list = new Stack<PoolableElement>();
                _pool.Add( element.ResourceName, list );
            }
            list.Push( element );

            element.transform.SetParent( transform );
        }

        public IResourcesService GetResourcesService()
        {
            return _resourcesService;
        }
    }
}
