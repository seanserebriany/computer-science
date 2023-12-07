using GameEngine.GameObjects;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace Arcanoid.GameObjects
{
    public class Bar : GameMovingObject
    {
        public double dX => _dX;
        private double _speed;
        /// <summary>
        /// הפעולה בונה עצם מחבט
        /// </summary>
        /// <param name="scene">במת המשחק</param>
        /// <param name="fileName">שם הקובץ שמתאר את המחבט</param>
        /// <param name="speed">מהירות אופקית</param>
        /// <param name="Width">רוחב המחבט</param>
        /// <param name="placeX">מיקום היבצרות המחבט אופקי</param>
        /// <param name="placeY">מיקום היבצרות המחבט אנכי</param>
        public Bar(Scene scene ,string fileName, double speed, int Width, double placeX, double placeY):base(placeX, placeY, fileName, scene)
        {
            _speed = speed;
            image.Height = 20; //אורך המחבט
            image.Width = Width; //רוחב המחבט
            image.Stretch = Windows.UI.Xaml.Media.Stretch.Fill;

            //כך המחבט נרשם לאירועים הקשורים למקלדת
            Manager.GameEvent.OnKeyDown += KeyDown;
            Manager.GameEvent.OnKeyUp += KeyUp;
        }

        /// <summary>
        /// באמצעות הפעולה הזאת המחבט מגיב לעזיבת המקש
        /// </summary>
        /// <param name="key">המקש שנעזב</param>
        private void KeyUp(VirtualKey key)
        {
            switch(key)
            {
                case VirtualKey.Left:
                case VirtualKey.Right:
                    Stop();
                    break;
            }
        }

        /// <summary>
        /// כך המחבט מגיב ללחיצה על מקלדת
        /// </summary>
        /// <param name="key"></param>
        private void KeyDown(VirtualKey key)
        {
            switch(key)
            {
                case VirtualKey.Left:
                    MoveTo(int.MinValue, _Y, _speed, 0);
                    break;
                case VirtualKey.Right:
                    MoveTo(int.MaxValue, _Y, _speed, 0);
                    break;
            }

        }
        public override void Render()
        {
            base.Render();
            if(_X <= 0)
            {   
                _X = 0;
                Stop();
            }
            else if(_X >= _scene?.ActualWidth - Width)//כאשר תיגע עם הצד הימני שלך בקיר הימני של הזירה
            {
                _X = _scene.ActualWidth - Width;
                Stop();
            }
        }
    }
}
