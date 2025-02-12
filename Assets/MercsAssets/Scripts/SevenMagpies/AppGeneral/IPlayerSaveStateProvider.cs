
namespace SevenMagpies.AppGeneral
{
    public interface IPlayerSaveStateProvider
    {
        public bool FirstLaunchCompleted { get; }

        public void PrepareState();

        public void DeleteState();

        public void SetFirstLaunchCompleted();
    }
}
