using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameServices
{
    /// <summary>
    /// an abstract class thats manage the game itself (clock ,stage ,events ect)   
    /// </summary>
    public abstract class Manager
    {
        public Scene Scene {  get; private set; }
        
        public Manager(Scene scene)
        {
            Scene = scene;
        }
    }
}
