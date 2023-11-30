using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameObjects
{
    public abstract class MoveObject : GameObject
    {
        protected MoveObject(Scene scene, string filename, double placeX, double placeY) : base(scene, filename, placeX, placeY)
        {
        }
    }
}
