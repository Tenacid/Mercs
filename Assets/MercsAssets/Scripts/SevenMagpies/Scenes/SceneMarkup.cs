using System.Threading.Tasks;
using UnityEngine;

namespace SevenMagpies.Scenes
{
    public  class SceneMarkup : MonoBehaviour
    {
        [SerializeField]
        private Transform _viewsParent;

        public async Task Prewarm() 
        {
            await Task.Delay( 3000 );
            Debug.Log( "Prewarm" );
        }
    }
}
