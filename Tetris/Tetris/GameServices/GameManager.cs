using DataBase;
using DataBase.Models;
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
        public static GameUser Player = new GameUser();

        public GameManager(Scene scene) : base(scene)
        {
            Init();
        }
        public void Init()
        {
            Manager.GameEvent.Clear();
            var block = new Block(Scene, $"Blocks/{Player.UsingProduct}.png", 3, 50, 153, 0, true);
            Scene.AddObject(block);
        }
    }
}
