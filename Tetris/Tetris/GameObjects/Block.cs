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
using Tetris.GameServices;
using Windows.Foundation;
using Windows.Storage.FileProperties;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Documents;

namespace Tetris.GameObjects
{
    public class Block : GameMovingObject
    {
        private bool _isNewGame;
        public double dX => _dX;
        public double dY => _dY;
        public double _speed;//the block speed
        private bool IsFalling = true;//if the block is falling
        private bool fromright = true;//if there block can move to the left and there is no obstacle there
        private bool fromleft = true;//if there block can move to the right and there is no obstacle there
        private bool ceeling = true;//to check if the block passed the y=0 so the game wont recognize it as a lose
        private Scene _scene;// the scene
        private bool IsOnGround = false; //לבדוק אם הבלוק על הרצפה
        private static int countOnGroundBlocks = 0;//לבדוק כמה בלוקים יש על הרצפה כדי לדעת מתי למחוק את השורה
        /// <summary>
        ///
        /// </summary>
        /// <param name="scene">במת המשחק</param>
        /// <param name="fileName">שם קובץ שמתאר את המחבא</param>
        /// <param name="speed">מהירות אופקית</param>
        /// <param name="width">מהירות אנכית</param>
        /// <param name="placeX">מיקום יווצרות הכדור ביחס לציר האופקי</param>
        /// <param name="placeY">מיקום יווצרות המחבט ביחס לציר אנכי</param>
        public Block(Scene scene, string fileName, double speed, int width, double placeX, double placeY, bool IsNewGame) :
            base(placeX, placeY, fileName, scene)
        {
            _isNewGame = IsNewGame;
            if(IsNewGame)
            {
                countOnGroundBlocks = 0;
            }
            _speed = speed;
            image.Height = width; // כך אנו קובעים את עובי הכדו
            image.Width = width; // קובעים את הרוחב הכדור
            image.Stretch = Windows.UI.Xaml.Media.Stretch.Fill; // כך אנו מותחים את מראה המחבט
            _scene = scene;
            if (Manager.GameEvent.OnKeyDown == null) 
            {
                Manager.GameEvent.OnKeyDown += KeyDown;//הרשמת הEVENT KEYDOWN
            }
            Fall();//פונקציה שנותנת לבלוק את המהירות לפי הלבל
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
                    if (fromleft == true)//אם נלחץ כפתור שמאלה אז להזיז את הבלוק שמאלה אם אין הפרעה מצד ימין שנבדק באמצעות המשתנה from left
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
                case VirtualKey.Left://אם נלחץ כפתור שמאלה אז להזיז את הבלוק שמאלה אם אין הפרעה מצד שמאל שנבדק באמצעות המשתנה from right
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
                case VirtualKey.Down: //אם נלחץ כפתור למטה אז להוריד את הבלוק
                    {
                        BringDown();
                        break;
                    }

            }

        }

        private void BringDown()
        {
            double maxY = _scene.ActualHeight; // Initialize maxY with the lowest possible value
            // Iterate over each game object in the scene snapshot
            foreach (GameObject obj in _scene._GameObjectSnapShot)
            {
                // Check if the object is a block and not the same block
                if (obj is Block block && block != this)
                {
                    // Check if the block is directly above the current object and lower than maxY
                    if (block._X == _X && block._Y > _Y)
                    {
                        maxY = block._Y; // Update maxY to the highest position below the current block
                    }
                }
            }

            // If maxY hasn't changed, set Y to the default value; otherwise, move down by 60 units
            if (maxY == _scene.ActualHeight)
                _Y = _scene.ActualHeight - 50;
            else
                _Y = maxY - 60;
        }

        private void Fall() // מעניקה לבלוק מהירות לפי הלבל
        {
            _dY = LevelPage.Level.Speed;
            _dX = 0;
        }
        public override void Collide(GameObject gameObject) // ובודקת התנגשות בין שני בלוקים CheckCollistion פעולה מתבצעת רק כאשר יש נגיעה וודאית שבודקים את זה בפונקציה
        {
            if (gameObject is Block block)
            {

                if (block._Y >= _Y) //אם האובייקט מתחת לבלוק
                {
                    _Y = block._Y - 48; 
                    Stop();
                }
            }
        }


        public override void Stop()// הפונקצייה שעוצרת את המשחק ובודקת הפסד
        {
            Manager.GameEvent.OnKeyDown -= KeyDown; //ביטול הרשמה מאירוע כי לא צריך כבר הבלוק נעצר
            base.Stop();
            ceeling = false;//בגלל שהבלוק כבר לא יורד מהגבול אלה הוא נעצר
            if (_Y <= _scene.ActualHeight / 3 && ceeling == false) // נוגעה בגבול
            {
                Manager.GameEvent.OnRun = null;
                Manager.GameEvent.GameOver();
                IsFalling = false;//כדי למנוע יצירה של בלוק חדש בגלל שהמשחק נגמר
            }
            if (IsFalling)//אם המשחק לא נגמר והבלוק עדיין צריך ליפול אז יוצרים בלוק חדש
            {
                var block = new Block(_scene, $"Blocks/{GameManager.Player.UsingProduct}.png", 3, 50, 153, 0, false);
                _scene.AddObject(block);
            }
            IsFalling = false;//אחרת ימשיכו להיווצר בלוקים שיובליו לסיום המשחק למרות שהוא לא הסתיים
        }

        public override void Render()// מתבצעת כל הזמן
        {
            if (_scene != null && _scene.ActualHeight != 0)
            {

                // בדיקת קצוות הזירה
                //if (IsRelased)
                //{
                if (_X <= 0) // נגיעה בקיר שמאלי
                {
                    _dX = -_dX;
                    _X = 0;
                    if (_Y >= _scene.ActualHeight - Height)//נגיעה ברצפה
                    {
                        _Y = _scene.ActualHeight - 50;
                        Stop();
                        if (IsOnGround == false)//אם הוא לא על הרצפה
                        {
                            countOnGroundBlocks++;
                            IsOnGround = true;//לעדכן שהוא על הרצפה
                        }
                        if (countOnGroundBlocks == 6)
                        {
                            RemoveLine();
                        }
                    }

                }
                else if (_X >= 255) // כאשר תיגע עם הצד הימני שלך בקיר הימני של הזירה
                {
                    _dX = -_dX;
                    _X = 255; // עוצר לך בפינה
                    if (_Y >= _scene.ActualHeight - 50)
                    {
                        _Y = _scene.ActualHeight - 50;
                        Stop();
                        if (IsOnGround == false)
                        {
                            countOnGroundBlocks++;
                            IsOnGround = true;
                        }
                        if (countOnGroundBlocks == 6)
                        {
                            RemoveLine();
                        }
                    }
                }
                else if (_Y >= _scene.ActualHeight - 55) // בדיקת רצפה
                {
                    _Y = _scene.ActualHeight - 50;
                    Stop();
                    if (IsOnGround == false)
                    {
                        countOnGroundBlocks++;
                        IsOnGround = true;
                    }
                    if (countOnGroundBlocks == 6)
                    {
                        RemoveLine();
                    }
                }
                base.Render();
            }
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
                            if (Math.Abs(block.rect.Right - this.rect.Left) == 1) // אם יש מכשול מצדו השמאלי של הבלוק
                            {
                                {
                                    if (Math.Abs(block.rect.Top - this.rect.Top) <= 50) //והמכשול בגובה המתאים
                                    {
                                        fromright = false; //לעדכן שאי אפשר להזיז את הבלוק שמאלה ושיש מכשול
                                    }
                                }
                            }
                            else if (Math.Abs(block.rect.Left - this.rect.Right) == 1)// אם יש מכשול מצדו הימני של הבלוק
                            {
                                if (Math.Abs(block.rect.Top - this.rect.Top) <= 50)//והמכשול בגובה המתאים
                                {
                                    fromleft = false;//לעדכן שאי אפשר להזיז את הבלוק ימינה ושיש מכשול
                                }
                            }

                        }
                    }
                }
            }
        }
        public void RemoveLine()
        {
            foreach (GameObject obj in _scene._GameObjectSnapShot) //עובר על כל הבלוקים
            {
                countOnGroundBlocks = 0;
                if (obj != null)
                {
                    if (obj is Block block)
                    {
                        if (block.rect.Top >= _scene.ActualHeight - Height) // אם הבלוק בשורה הכי נמוכה
                        {
                            _scene.RemoveObject(block);
                        }
                        else if (block._Y > 100)
                        {
                            block._Y += 50;
                        }
                    }

                }
            }
            Manager.GameEvent.AddScore();

        }

    }
}

