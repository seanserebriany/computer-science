using GameEngine.GameObjects;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Windows.Foundation;
using Windows.Storage.FileProperties;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Documents;

namespace Tetris.GameObjects
{
    public class Block : GameMovingObject
    {
        public double dX => _dX;
        public double dY => _dY;
        public double _speed;
        private bool IsFalling = true;
        private bool Condition = true;
        private int _countlifes;
        private Scene _scene;
        private Block _block = null;
        /// <summary>
        ///
        /// </summary>
        /// <param name="scene">במת המשחק</param>
        /// <param name="fileName">שם קובץ שמתאר את המחבא</param>
        /// <param name="speed">מהירות אופקית</param>
        /// <param name="width">מהירות אנכית</param>
        /// <param name="placeX">מיקום יווצרות הכדור ביחס לציר האופקי</param>
        /// <param name="placeY">מיקום יווצרות המחבט ביחס לציר אנכי</param>
        public Block(Scene scene, string fileName, double speed, int width, double placeX, double placeY) :
            base(placeX, placeY, fileName, scene)
        {
            _speed = speed;
            image.Height = width; // כך אנו קובעים את עובי הכדו
            image.Width = width; // קובעים את הרוחב הכדור
            image.Stretch = Windows.UI.Xaml.Media.Stretch.Fill; // כך אנו מותחים את מראה המחבט
            _scene = scene;
            Manager.GameEvent.OnKeyDown += KeyDown;
            Fall();
        }
        /// <summary>
        /// באמצעות הפעולה הזאת הכדור מגיב לעזיבת המקש
        /// </summary>
        /// <param name="key">המקש שנעזוב</param>
        public void KeyDown(VirtualKey key)
        {
            Check();
            switch (key)
            {
                case VirtualKey.Right:                  
                    if (Condition == true)
                    {
                        _X += 51;
                        break;
                    }
                    else
                    {
                        Condition = true;
                        break;
                    }                       
                case VirtualKey.Left:
                    if (Condition == true)
                    {
                        _X -= 51;
                        break;
                    }
                    else
                    {
                        Condition = true;
                        break;
                    }
            }

        }

        private void Fall()
        {
            _dY = 9;
            _dX = 0;
        }
        public override void Collide(GameObject gameObject) // CheckCollistion פעולה מתבצעת רק כאשר יש נגיעה וודאית שבודקים את זה בפונקציה
        {
            if (gameObject is Block block)
            {
                var intersectRect = RectHelper.Intersect(rect, block.rect); // בדיקת התנגשות בין כדור לגיילי
                if (intersectRect.Height < intersectRect.Width)// בדיקת פגיעה וגם מוצאת את גיילי זה
                {
                    Stop();
                }
            }
        }

        public override void Stop()
        {
            Manager.GameEvent.OnKeyDown -= KeyDown;
            base.Stop();
            if (IsFalling)
            {
                var block = new Block(_scene, "Blocks/Block.png", speed: 2, width: 50, 153, 0);
                _scene.AddObject(block);
                _scene.CountObject();
            }
            IsFalling = false;
        }

        public override void Render()// מתבצעת כל הזמן
        {
            // בדיקת קצוות הזירה
            //if (IsRelased)
            //{
            if (_X <= 0) // נגיעה בקיר שמאלי
            {
                _dX = -_dX;
                _X = 0;
                if (_Y >= _scene?.ActualHeight - Height)
                {
                    Stop();
                }

            }
            else if (_Y + _dY <= 0) // נוגעה בתקרה
            {

            }
            else if (_X >= 408) // כאשר תיגע עם הצד הימני שלך בקיר הימני של הזירה
            {
                _dX = -_dX;
                _X = 408; // עוצר לך בפינה
                if (_Y >= _scene?.ActualHeight - Height)
                {
                    Stop();
                }
            }
            else if (_Y >= _scene?.ActualHeight - Height) // בדיקת רצפה
            {
                Manager.GameEvent.OnKeyDown -= KeyDown;
                Stop();
            }
            base.Render();
        }



        public void Check()
        {
            for (int i = 0; i < _scene.ReturnCountObject(); i++)
            {
                if (this != _scene._GameObjects[i])
                {
                    if (Math.Abs(_scene._GameObjects[i].rect.Right - rect.Left) == 1 || Math.Abs(_scene._GameObjects[i].rect.Left - rect.Right) == 1)
                    {
                        {
                            if(_scene._GameObjects[i].rect.Top - rect.Top <=50)
                            {
                                Condition = false;
                            }
                        }
                    }
                }
            }
        }

    }

}

