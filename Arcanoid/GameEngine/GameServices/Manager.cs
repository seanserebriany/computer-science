using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace GameEngine.GameServices
{
    /// <summary>
    /// an abstract class thats manage the game itself (clock ,stage ,events ect)   
    /// </summary>
    public abstract class Manager
    {
        public Scene Scene {  get; private set; }
        private static DispatcherTimer _runTimer;//הטיימר יפעיל ללא הפסקה אירוע OnRun
        
        public static GameEvents GameEvent { get;} = new GameEvents();
        public Manager(Scene scene)
        {
            Scene = scene;
            if(_runTimer == null)
            {
                _runTimer = new DispatcherTimer();
                _runTimer.Interval = TimeSpan.FromSeconds(Constans.RunInterval);
                _runTimer.Tick += _RunTimer_Tick;
            }
        }

        private void _RunTimer_Tick(object sender, object e)
        {
            if(GameEvent.OnRun != null)
            {
                GameEvent.OnRun();
            }
        }
    }
}
