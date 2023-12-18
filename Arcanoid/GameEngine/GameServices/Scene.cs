using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using GameEngine.GameObjects;

namespace GameEngine.GameServices
{
    public abstract class Scene : Canvas
    {
        private List<GameObject> _GameObjects = new List<GameObject>();// list of the game object

        protected List<GameObject> _GameObjectSnapShot => _GameObjects.ToList();

        public double Ground {get; set; }

        public Scene() 
        {
            Manager.GameEvent.OnRun += Run; //בזכות השורה הפעולה RUN פועלת ללא הפסקה
        }

        private void Run()
        {
            foreach(var obj in _GameObjects)
            {
                if(obj is GameMovingObject)
                {
                    obj.Render();
                }
            }
        }

        public void Init()
        {
            foreach (var obj in _GameObjects)
            {
                obj.Init();
            }
        }
        public void RemoveAllObjects()
        {
            foreach (var obj in _GameObjects)
            {
                RemoveObject(obj);
            }
        }
        public void RemoveObject(GameObject obj)
        {
            if (_GameObjects.Contains(obj))
            {
                _GameObjects.Remove(obj);
                Children.Remove(obj.image);
            }
        }
        public void AddObject(GameObject obj) 
        {
            _GameObjects.Add(obj);
            Children.Add(obj.image);
        }
    }
}
