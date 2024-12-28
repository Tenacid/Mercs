
namespace SevenMagpies.AppGeneral
{
    public interface IPlayerSaveStateProvider
    {
        public bool FirstLaunchCompleted { get; }

        public void PrepareState();
    }
}
