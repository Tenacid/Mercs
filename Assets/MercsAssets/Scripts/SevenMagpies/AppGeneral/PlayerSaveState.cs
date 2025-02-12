using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenMagpies.AppGeneral
{
    public class PlayerSaveState
    {
        public bool FirstLaunchCompleted;
        public List<string> CompletedTutorials;
        public long RegistrationTime;

        public PlayerSaveState() 
        {
            CompletedTutorials = new List<string>();

            RegistrationTime = DateTime.UtcNow.Ticks;
        }
    }
}
