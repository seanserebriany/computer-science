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
        private void Init()
        {
            Random rnd = new Random();
            var block = new Block(Scene, "Blocks/Tblock.jpg", speed:2, width: 70, Scene.ActualWidth / 2, 0);
            Scene.AddObject(block);

        }
    }
}
