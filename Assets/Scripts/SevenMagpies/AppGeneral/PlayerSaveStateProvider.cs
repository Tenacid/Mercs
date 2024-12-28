using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace SevenMagpies.AppGeneral
{
    public class PlayerSaveStateProvider : IPlayerSaveStateProvider
    {
        private const string PlayerSaveStateFileName = "MercsSaveState.dat";

        public readonly JsonSerializerSettings SerializationSetting = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Full, Formatting = Formatting.Indented };

        private PlayerSaveState _state;

        public PlayerSaveStateProvider()
        {
        }

        public void PrepareState()
        {
            if ( TryLoadStateFromStorage() )
            {
                return;
            }

            _state = new PlayerSaveState();
        }

        public bool TryLoadStateFromStorage()
        {
            var path = Path.Combine( Application.persistentDataPath, PlayerSaveStateFileName );

            if ( !File.Exists( path ) )
            {
                return false;
            }

            _state = ( PlayerSaveState ) JsonConvert.DeserializeObject( File.ReadAllText( path ), SerializationSetting );

            return true;
        }

        public bool FirstLaunchCompleted => _state.FirstLaunchCompleted;
    }
}
