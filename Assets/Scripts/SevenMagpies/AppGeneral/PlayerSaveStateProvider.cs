using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace SevenMagpies.AppGeneral
{
    public class PlayerSaveStateProvider : IPlayerSaveStateProvider
    {
        private const string PlayerSaveStateFileName = "MercsSaveState.dat";

        public readonly JsonSerializerSettings SerializationSetting = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Full, Formatting = Formatting.Indented };

        private string _savePath = Path.Combine( Application.persistentDataPath, PlayerSaveStateFileName );

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
            SaveState();
        }

        public bool TryLoadStateFromStorage()
        {            

            if ( !File.Exists( _savePath ) )
            {
                return false;
            }

            _state = ( PlayerSaveState ) JsonConvert.DeserializeObject( File.ReadAllText( _savePath ), SerializationSetting );

            return true;
        }

        public void SaveState() 
        {
            var text = JsonConvert.SerializeObject( _state, SerializationSetting );
            File.WriteAllText( _savePath, text );            
        }

        public void DeleteState() 
        {
            if ( !File.Exists( _savePath ) )
            {
                return;
            }

            File.Delete( _savePath );
        }

        public bool FirstLaunchCompleted => _state.FirstLaunchCompleted;

        public void SetFirstLaunchCompleted() 
        {
            _state.FirstLaunchCompleted = true;
            SaveState();
        }
    }
}
