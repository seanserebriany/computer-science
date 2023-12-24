using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using static GameEngine.GameServices.Constans;

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
        public static GameState gameState { get; private set;}
        public Manager(Scene scene)
        {
            Scene = scene;
            if(_runTimer == null)
            {
                _runTimer = new DispatcherTimer();
                _runTimer.Interval = TimeSpan.FromMilliseconds(30);
                _runTimer.Start();
                _runTimer.Tick += _RunTimer_Tick;
            }
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;


        }

        private void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            if(GameEvent.OnKeyUp != null)
            {
                GameEvent.OnKeyUp(args.VirtualKey);

            }
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            if (GameEvent.OnKeyDown != null)
            {
                GameEvent.OnKeyDown(args.VirtualKey);

            }
        }

        private void _RunTimer_Tick(object sender, object e)
        {
            if(GameEvent.OnRun != null)
            {
                GameEvent.OnRun();
            }
        }
        public void Start()
        {
            Scene.Init();
            _runTimer.Start();
            gameState = GameState.Started;
        }
        public void Pause()
        {
            _runTimer.Stop();
            gameState = GameState.Paused;
        }
        public void Resume()
        {
            _runTimer.Start();
            gameState = GameState.Started;

        }
        public static bool GameOver()
        {
            if(gameState != GameState.Ended)
            {
                _runTimer.Stop();
                gameState = GameState.Ended;
                return true;
            }
            return false;
        }
    }
}
