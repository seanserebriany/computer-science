using GameEngine.GameObjects;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace Arcanoid.GameObjects
{
    public class Ball : GameMovingObject
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
        public Ball(Scene scene, string fileName, double speed, int Width, double placeX, double placeY) : base(placeX, placeY, fileName, scene)
        {
            _speed = speed;
            image.Height = 20; //אורך המחבט
            image.Width = Width; //רוחב המחבט
            image.Stretch = Windows.UI.Xaml.Media.Stretch.Fill;
            //MoveTo(_X, int.MinValue, _speed, 0);

            //כך המחבט נרשם לאירועים הקשורים למקלדת
            Manager.GameEvent.OnKeyDown += KeyUp;
        }
        private void KeyUp(VirtualKey key)
        {
            switch (key)
            {
                case VirtualKey.Up:
                    MoveTo(_X, int.MinValue, _speed, 0);
                    Manager.GameEvent.OnKeyDown -= KeyUp;
                    break;
            }
        }
        private void Down()
        {
            MoveTo(_X, int.MaxValue, _speed, 0);

        }
        private void Up()
        {
            MoveTo(_X, int.MinValue, _speed, 0);

        }
        private void diff()
        {
            MoveTo(-_X, int.MinValue, _speed, 0);

        }
        public override void Render()
        {
            base.Render();

            //בדיקת קצוות הזירה
            if (_Y < 0)
            {
                _Y = 0;
                Down();
            }
            else if (_Y > _scene?.ActualHeight)//כאשר תיגע עם הצד הימני שלך בקיר הימני של הזירה
            {
                _Y = _scene.ActualHeight;
                Up();
            }
            else if (_X < 0)
            {
                _X = 0;
                diff();
            }
            else if (_X > _scene?.ActualWidth - Width)//כאשר תיגע עם הצד הימני שלך בקיר הימני של הזירה
            {
                _X = _scene.ActualWidth - Width;
                diff();
            }


        }
        public override void Collide()
        {

        }
    }
}
