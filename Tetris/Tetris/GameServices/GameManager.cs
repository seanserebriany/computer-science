
//using DatabaseProject.Models;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcanoid.GameServices
{
    public class GameManager : Manager
    {
        public GameManager(Scene scene) : base(scene)
        {
            //User.Level = LevelsPage.Level;
            //User.Name = "Sean";
            //User.Email = "sean123@gmail.com";

            Init();

        }

        private void Init()
        {
            Random rnd = new Random();

            Scene.RemoveAllObjects();
            //for (int i = 0; i < 1; i++) 
            //{
            //    Scene.AddObject(new Jelly(Scene, (jellyType)rnd.Next(0, 3), 70, 70 * i, 0));
            //}
            //for (int i = 0; i < 1; i++)
            //{
            //    Scene.AddObject(new Jelly(Scene, (jellyType)rnd.Next(0, 3), 70, 70 * i, 40));
            //}
            //for (int i = 0; i < 1; i++)
            //{
            //    Scene.AddObject(new Jelly(Scene, (jellyType)rnd.Next(0, 3), 70, 70 * i, 80));
            //}

            //var bar = new Bar(Scene, "Bar/Bar.png", speed: User.Level.BallSpeed, User.Level.BarWidth, Scene.ActualWidth / 2 - 30, Scene.ActualHeight-30);;
            //Scene.AddObject(bar);
            //var ball = new Ball(Scene, "Ball/Ball.png", speed:User.Level.BallSpeed, width: 40, Scene.ActualWidth / 2, Scene.ActualHeight - 35);
            //Scene.AddObject(ball);

        }
    }
}
