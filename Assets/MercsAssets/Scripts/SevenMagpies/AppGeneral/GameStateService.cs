using SevenMagpies.Scenes;

namespace SevenMagpies.AppGeneral
{

    public class GameStateService : IGameStateService
    {
        private readonly IScenesController _scenesController;

        private bool _loading;
        private GameState _curState;

        private SceneLoadingPayload _payload;


        public GameStateService( IScenesController scenesController )
        {
            _scenesController = scenesController;
        }

        public void ToMeta( MetaSceneLoadingPayload payload )
        {
            _payload = payload;
            _scenesController.LoadMetaScene( OnMetaLoaded );            
        }

        public void OnMetaLoaded() 
        {
            _curState = GameState.Meta;

            //Use payload

            _payload = null;
        }

        public void ToMatch( MatchSceneLoadingPayload payload )
        {
            _payload = payload;
            _scenesController.LoadMatchScene( OnMatchLoaded );
        }

        public void OnMatchLoaded()
        {
            _curState = GameState.Match;

            //Use payload

            _payload = null;
        }
    }
}
