using Arcanoid.GameObjects;
using GameEngine.GameServices;
using System.Linq;

namespace Arcanoid.GameServices
{
    public class GameScene : Scene
    {
        public int BrickCount => _GameObjectSnapShot.Count(x=> x is Jelly);
    }
}
