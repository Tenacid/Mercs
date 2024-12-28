using System;

namespace SevenMagpies.Application
{
    public interface IProcessObserver
    {
        public void ChangeProgress( float progress );
        public event EventHandler<float> OnProgressChanged;
    }
}
