using SevenMagpies.Scenes;

namespace SevenMagpies.AppGeneral
{
    public interface IGameStateService
    {
        public void ToMeta( MetaSceneLoadingPayload payload );
        public void ToMatch( MatchSceneLoadingPayload payload );
    }
}
