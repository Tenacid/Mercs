namespace SevenMagpies.AppGeneral
{
    public interface IUpdateProcessor
    {
        public void Add( IUpdatable updatable );
        public void Remove( IUpdatable updatable );
    }
}
