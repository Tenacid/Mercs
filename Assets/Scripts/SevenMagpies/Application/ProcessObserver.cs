using System;

namespace SevenMagpies.Application
{
    public class ProcessObserver : IProcessObserver
    {
        public event EventHandler<float> OnProgressChanged;

        public void ChangeProgress( float progress )
        {
            OnProgressChanged?.Invoke( this, progress );
        }
    }
}
