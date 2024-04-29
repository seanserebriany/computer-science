using DataBase.Models;
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
        private bool fromright = true;
        private bool fromleft = true;
        private bool ceeling = true;

        private int _countlifes;
        private Scene _scene;
        private Block _block = null;
        private bool IsOnGround = false;
        private static int countOnGroundBlocks = 0;
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
            CheckSides();
            switch (key)
            {
                case VirtualKey.Right:
                    if (fromleft == true)
                    {
                        _X += 51;
                        break;
                    }
                    else
                    {
                        fromleft = true;
                        fromright = true;
                        break;
                    }
                case VirtualKey.Left:
                    if (fromright == true)
                    {
                        _X -= 51;
                        break;
                    }
                    else
                    {
                        fromright = true;
                        fromleft = true;
                        break;
                    }
            }

        }

        private void Fall()
        {
            GameLevel level = new GameLevel();
            _dY = level.Speed;
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
            ceeling = false;
             if (_Y + _dY <= 50 && ceeling == false) // נוגעה בתקרה
            {
                Manager.GameEvent.OnRun = null;
                Manager.GameEvent.GameOver();
                // Manager.GameOver();
            }
            if (IsFalling)
            {
                Init();
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
                    if (IsOnGround == false)
                    {
                        countOnGroundBlocks++;
                        IsOnGround = true;
                    }
                    if (countOnGroundBlocks == 9)
                    {
                        RemoveLine();
                    }
                }

            }
            else if (_X >= 408) // כאשר תיגע עם הצד הימני שלך בקיר הימני של הזירה
            {
                _dX = -_dX;
                _X = 408; // עוצר לך בפינה
                if (_Y >= _scene?.ActualHeight - Height)
                {
                    Stop();
                    if (IsOnGround == false)
                    {
                        countOnGroundBlocks++;
                        IsOnGround = true;
                    }
                    if (countOnGroundBlocks == 9)
                    {
                        RemoveLine();
                    }

                }
            }
            else if (_Y >= _scene?.ActualHeight - 50) // בדיקת רצפה
            {
                Manager.GameEvent.OnKeyDown -= KeyDown;
                Stop();
                if(IsOnGround==false)
                {
                    countOnGroundBlocks++;
                    IsOnGround = true;
                }
                if (countOnGroundBlocks == 9)
                {
                    RemoveLine();
                }
            }
            base.Render();
        }



        public void CheckSides()
        {
            foreach (GameObject obj in _scene._GameObjectSnapShot)
            {
                if (obj != null)
                {
                    if (obj is Block block)
                    {
                        if (this != block)
                        {
                            if (Math.Abs(block.rect.Right - this.rect.Left) == 1)
                            {
                                {
                                    if (Math.Abs(block.rect.Top - this.rect.Top) <= 50)
                                    {
                                        fromright = false;
                                    }
                                }
                            }
                            else if(Math.Abs(block.rect.Left - this.rect.Right) == 1)
                            {
                                if (Math.Abs(block.rect.Top - this.rect.Top) <= 50)
                                {
                                    fromleft = false;
                                }
                            }

                        }
                    }
                }
            }
        }
        public void RemoveLine()
        {

            foreach (GameObject obj in _scene._GameObjectSnapShot)
            {
                countOnGroundBlocks = 0;
                if (obj != null)
                {
                    if (obj is Block block)
                    {
                        if (block.rect.Top >= _scene?.ActualHeight - Height)
                        {
                            _scene.RemoveObject(block);
                        }
                        else if(block._Y> 100)
                        {
                            block._Y += 45;
                        }
                    }

                }
            }
            Manager.GameEvent.AddScore();

        }

    }
}

