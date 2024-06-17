using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace GameEngine.GameServices
{
    public class GameEvents
    {
        public Action OnRun;//האירוע שבזכותו כל מי שירשם ינוע
        public Action OnClock;//האירוע שבזכותו יפעל השעון
        public Action <int> AddScore;
        public Action GameOver;
        public Action<VirtualKey> OnKeyUp;//עזיבת כפתור
        public Action<VirtualKey> OnKeyDown;//לחיצה על כפתור
        

        public void Clear()
        {
            GameOver = null;
            AddScore = null;
            OnKeyUp = null;
            OnKeyDown = null;
        }
    }
}
