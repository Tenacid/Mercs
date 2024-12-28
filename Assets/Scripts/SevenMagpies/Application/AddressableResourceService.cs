using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace SevenMagpies.Application
{
    public class AddressableResourceService : IResourcesService
    {
        public void Construct()
        {
        }

        public bool Contains( string resourceName )
        {
            throw new System.NotImplementedException();
        }

        public async Task<T> GetResourceAsync<T>( string resourceName )
        {
            var handler = Addressables.LoadAssetAsync<T>( resourceName );

            await handler.Task;

            return handler.Result;
        }
    }
}
