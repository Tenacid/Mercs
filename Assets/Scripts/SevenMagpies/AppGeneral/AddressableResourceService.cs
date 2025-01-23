using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SevenMagpies.AppGeneral
{
    public class AddressableResourceService : IResourcesService
    {
        public void Construct()
        {
        }

        public async Task<T> GetResourceAsync<T>( string resourceName )
        {
            var usedType = typeof( T );
            if ( usedType.IsSubclassOf( typeof( Component ) ) )
            {
                var goHandler = Addressables.LoadAssetAsync<GameObject>( resourceName );

                await goHandler.Task;

                return goHandler.Result.GetComponent<T>();
            }


            var handler = Addressables.LoadAssetAsync<T>( resourceName );

            await handler.Task;

            return handler.Result;
        }
    }
}
