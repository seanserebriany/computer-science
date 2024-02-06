using GameEngine.GameObjects;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Xaml;

namespace Tetris.GameObjects
{
    public class Block : GameMovingObject
    {
        public double dX => _dX;
        public double dY => _dY;
        public double _speed;
        private bool IsRelased = false;
        private int _countlifes;
        private Scene _scene;
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
            image.Height = 45; // כך אנו קובעים את עובי הכדור
            image.Width = width; // קובעים את הרוחב הכדור
            image.Stretch = Windows.UI.Xaml.Media.Stretch.Fill; // כך אנו מותחים את מראה המחבט
            _countlifes = 3;
            _scene = scene;
            Fall();
        }

        private void Fall()
        {
            _dY = 3;
        }

        public override void Collide(GameObject gameObject) // CheckCollistion פעולה מתבצעת רק כאשר יש נגיעה וודאית שבודקים את זה בפונקציה
        {
          
            if (gameObject is Block block)// בדיקת פגיעה וגם מוצאת את גיילי זה
            {
                Stop();
                IsRelased = false;
            }
        }

        public override void Stop()
        {
            base.Stop();
            var block = new Block(_scene, "Blocks/Tblock.jpg", speed: 2, width: 70, _scene.ActualWidth / 2, 0);
            _scene.AddObject(block);
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


                }
                else if (_X >= _scene?.ActualWidth - Width) // כאשר תיגע עם הצד הימני שלך בקיר הימני של הזירה
                {
                    _dX = -_dX;
                    _X = _scene.ActualWidth - Width; // עוצר לך בפינה

                }
                else if (_Y >= _scene?.ActualHeight - Height) // בדיקת רצפה
                {
                    Stop();
                }
           // }
            base.Render();
        }



        //Messenger.Send(Place);
    }
}

