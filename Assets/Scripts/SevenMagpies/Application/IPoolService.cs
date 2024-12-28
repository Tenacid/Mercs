using System;
using System.Threading.Tasks;

namespace SevenMagpies.Application
{
    public interface IPoolService : IDisposable
    {
        void Construct( IResourcesService iGameobjectResourcesHolder );
        Task<T> GetResource<T>( string resourceName ) where T : PoolableElement;
        void RemoveToPool( PoolableElement element );
        IResourcesService GetResourcesService();
    }
}
