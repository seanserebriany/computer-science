using GameEngine.GameObjects;
using Arcanoid.GameObjects;
using GameEngine.GameServices;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml;
using Windows.Foundation;

namespace Arcanoid1.GameObjects
{
    public class Ball : GameMovingObject
    {

        public double dX => _dX;
        public double dY => _dY;
        public double _speed;
        private bool IsRelased = false;
        private int _countlifes;



        /// <summary>
        ///
        /// </summary>
        /// <param name="scene">במת המשחק</param>
        /// <param name="fileName">שם קובץ שמתאר את המחבא</param>
        /// <param name="speed">מהירות אופקית</param>
        /// <param name="width">מהירות אנכית</param>
        /// <param name="placeX">מיקום יווצרות הכדור ביחס לציר האופקי</param>
        /// <param name="placeY">מיקום יווצרות המחבט ביחס לציר אנכי</param>
        public Ball(Scene scene, string fileName, double speed, int width, double placeX, double placeY) :
            base(placeX, placeY, fileName, scene)
        {
            _speed = speed;
            image.Height = 45; // כך אנו קובעים את עובי הכדור
            image.Width = width; // קובעים את הרוחב הכדור
            image.Stretch = Windows.UI.Xaml.Media.Stretch.Fill; // כך אנו מותחים את מראה המחבט
            Manager.GameEvent.OnKeyDown += KeyDown;
            Manager.GameEvent.OnKeyUp += KeyUp;
            _countlifes = 3;
        }
        public void KeyUp(VirtualKey key)
        {
            if (!IsRelased)
            {
                switch (key)
                {
                    case VirtualKey.Left:
                    case VirtualKey.Right:
                        Stop();
                        break;

                }
            }

        }
        /// <summary>
        /// באמצעות הפעולה הזאת הכדור מגיב לעזיבת המקש
        /// </summary>
        /// <param name="key">המקש שנעזוב</param>
        public void KeyDown(VirtualKey key)
        {
            if (!IsRelased)
            {
                switch (key)
                {
                    case VirtualKey.Up:
                        IsRelased = true;
                        //_dX = _speed * Constans.SpeedUnit / 2;
                        _dY = _speed * Constans.SpeedUnit / 2;
                        _dX = 0;
                        Manager.GameEvent.OnKeyDown -= KeyDown;
                        Manager.GameEvent.OnKeyUp -= KeyUp;
                        break;
                    case VirtualKey.Right:
                        MoveTo(int.MaxValue, _Y, _speed, 0);
                        break;
                    case VirtualKey.Left:
                        MoveTo(int.MinValue, _Y, _speed, 0);
                        break;
                }
            }
        }

        public override void Collide(GameObject gameObject) // CheckCollistion פעולה מתבצעת רק כאשר יש נגיעה וודאית שבודקים את זה בפונקציה
        {
            if (gameObject is Bar bar)
            {
                _dY = -_dY;
                if (bar.dX > 0)
                {
                    _dX = Constans.SpeedUnit; // מעניק לכדור כמחצית המהירות של המחבט, כלומר. הכדור נוטה לכיוון תנועת המחבט
                }
                else if(bar.dX<0)
                {
                    _dX = -Constans.SpeedUnit;
                }
                _Y = bar.rect.Top - Height - 10;
            }
            if (gameObject is Jelly jelly)// בדיקת פגיעה וגם מוצאת את גיילי זה
            {
                var intersectRect = RectHelper.Intersect(rect, jelly.rect); // בדיקת התנגשות בין כדור לגיילי
                if (intersectRect.Height > intersectRect.Width) // בדיקת פגיעה שאחרי זה לאן הכדור ילך
                {
                    _X -= _dX;
                    _dX = -_dX;
                }
                else
                {
                    _Y -= _dY;
                    _dY = -_dY;
                }
                jelly.ChangeJelly();
            }
        }


        public override void Render()// מתבצעת כל הזמן
        {
            // בדיקת קצוות הזירה
            if (IsRelased)
            {
                if (_X <= 0) // נגיעה בקיר שמאלי
                {
                    _dX = -_dX;
                    _X = 0;


                }
                else if (_X >= _scene?.ActualWidth - Width) // כאשר תיגע עם הצד הימני שלך בקיר הימני של הזירה
                {
                    _dX = -_dX;
                    _X = _scene.ActualWidth - Width; // עוצר לך בפינה

                }
                else if (_Y + _dY <= 0) // נוגעה בתקרה
                {
                    _dY = -dY;
                    _Y = 0;
                }
                else if (_Y >= _scene?.ActualHeight - Height) // בדיקת רצפה
                {
                    Stop();
                    _scene.Init();
                    Manager.GameEvent.OnKeyDown += KeyDown;
                    Manager.GameEvent.OnKeyUp += KeyUp;
                    IsRelased = false;
                    if (Manager.GameEvent.OnRemoveHeart != null)
                    {
                        _countlifes--;
                        Manager.GameEvent.OnRemoveHeart(_countlifes);
                    }
                }
            }
            else
            {
                if (_X <+ 70)
                {
                    _X = 70;
                }
                else if (_X >= _scene?.ActualWidth - 90)
                {
                    _X = _scene.ActualWidth - 90;
                }
            }
            base.Render();
        }



        //Messenger.Send(Place);
    }

}
