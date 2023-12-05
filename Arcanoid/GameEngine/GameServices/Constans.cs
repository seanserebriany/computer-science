using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameServices
{
    public static class Constans
    {
        public const int Runinterval = 100;
        public enum GameState
        {
            Loaded, Started, Paused, GameOver
        }
        public static GameState Current { get;}
    }
}
