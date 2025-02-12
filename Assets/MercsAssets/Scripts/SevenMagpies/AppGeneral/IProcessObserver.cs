using System;

namespace SevenMagpies.AppGeneral
{
    public interface IProcessObserver
    {
        public void ChangeProgress( float progress );
        public event EventHandler<float> OnProgressChanged;
    }
}
