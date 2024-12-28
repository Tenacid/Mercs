using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenMagpies.AppGeneral
{
    public interface IResourcesService
    {
        public void Construct();

        public bool Contains( string resourceName );

        public Task<T> GetResourceAsync<T>( string resourceName );
    }
}
