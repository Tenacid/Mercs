namespace SevenMagpies.AppGeneral
{
    public interface IUpdatable
    {
        public void Register( IUpdateProcessor updateProcessor );
        public void Unregister( IUpdateProcessor updateProcessor );
        public void Update(float deltaTime);
    }
}
