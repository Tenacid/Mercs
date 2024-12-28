namespace SevenMagpies.AppGeneral
{

    public class GameStateService : IGameStateService
    {
        private readonly IScenesController _scenesController;

        private bool _loading;
        private GameState _curState;


        public GameStateService( IScenesController scenesController )
        {
            _scenesController = scenesController;
        }

        public void ToMeta()
        {
            _scenesController.LoadMetaScene( OnMetaLoaded );            
        }

        public void OnMetaLoaded() 
        {
            _curState = GameState.Meta;
        }
    }
}
