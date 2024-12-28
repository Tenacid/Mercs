using System;

namespace SevenMagpies.AppGeneral
{
    public interface IScenesController
    {
        public void LoadMatchScene( Action onComplete = null );
        public void LoadMetaScene( Action onComplete = null );
    }
}
