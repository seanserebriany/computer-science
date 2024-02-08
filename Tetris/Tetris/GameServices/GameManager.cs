using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.GameObjects;
using Windows.System;

namespace Tetris.GameServices
{
    public class GameManager : Manager
    {
        public GameManager(Scene scene) : base(scene)
        {
            Init();
        }
        public void Init()
        {
            Random rnd = new Random();
            var block = new Block(Scene, "Blocks/Block.png", speed:2, width: 50, Scene.ActualWidth / 2, 0);
            Scene.AddObject(block);

        }
    }
}
