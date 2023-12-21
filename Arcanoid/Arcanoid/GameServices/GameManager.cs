using Arcanoid.GameObjects;
using Arcanoid1.GameObjects;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Arcanoid.GameObjects.Jelly;

namespace Arcanoid.GameServices
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

            Scene.RemoveAllObjects();
            for (int i = 0; i < 9; i++) 
            {
                Scene.AddObject(new Jelly(Scene, (jellyType)rnd.Next(0, 3), 90, 100 * i, 30));
            }
            for (int i = 0; i < 9; i++)
            {
                Scene.AddObject(new Jelly(Scene, (jellyType)rnd.Next(0, 3), 90, 100 * i, 110));
            }
            for (int i = 0; i < 9; i++)
            {
                Scene.AddObject(new Jelly(Scene, (jellyType)rnd.Next(0, 3), 90, 100 * i, 190));
            }

            var bar = new Bar(Scene, "Bar/Bar.png", speed: 3, Width: 160, Scene.ActualWidth / 2 - 40, Scene.ActualHeight-60);
            Scene.AddObject(bar);
            var ball = new Ball(Scene, "Ball/Ball.jpg", speed: 3, width: 50, Scene.ActualWidth / 2 - 40, Scene.ActualHeight - 65);
            Scene.AddObject(ball);

        }
    }
}
