using Arcanoid.GameObjects;
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
            for (int i = 0; i < 3; i++) 
            {
                Scene.AddObject(new Jelly(Scene, (jellyType)rnd.Next(0, 3), 200, 200 * i, 300));
            }

            
        }
    }
}
