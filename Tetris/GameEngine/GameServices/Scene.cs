using GameEngine.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.ApplicationModel.Appointments;

namespace GameEngine.GameServices
{
    public abstract class Scene : Canvas
    {
        public List<GameObject> _GameObjects = new List<GameObject>();// list of the game object

        public List<GameObject> _GameObjectSnapShot => _GameObjects.ToList();

        public double Ground { get; set; }

        private int sum = 0;

        public Scene()
        {
            if(Manager.GameEvent.OnRun==null)
            {
                Manager.GameEvent.OnRun += Run; //בזכות השורה הפעולה RUN פועלת ללא הפסקה
                Manager.GameEvent.OnRun += CheckCollision;
            }
        }
        private void Run()
        {
            foreach (var obj in _GameObjectSnapShot)
            {
                if (obj is GameMovingObject)
                {
                    obj.Render();
                }
            }
        }

        public void Init()
        {
            foreach (var obj in _GameObjectSnapShot)
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
        //public void CountObject()
        //{
        //    sum += 1;
        //}
        //public int ReturnCountObject()
        //{
        //    return sum;
        //}
        public void CheckCollision() // OnRun הפעולה פעולת ללא ערב כתגובה לאירוע
        {
            foreach (var gameObject in _GameObjectSnapShot)  // עוברים על כל רשימת הפרוייקט
            {
                if (gameObject.Collisional) //  אם האובייקט הוא לא שקוף
                {
                    // מחפשים מופע ראשון של אובייקט אשר הוא לא אותו אובייקט, הוא לא שקוף והוא נוגע באובייקט הנוכחי
                    var otherObject = _GameObjectSnapShot.FirstOrDefault(g => !ReferenceEquals(g, gameObject) && g.Collisional &&
                    !RectHelper.Intersect(g.rect, gameObject.rect).IsEmpty);

                    if (otherObject != null)
                    {
                        gameObject.Collide(otherObject);
                    }
                }
            }
        }

    }
}
