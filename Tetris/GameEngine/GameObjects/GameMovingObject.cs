using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameObjects
{
    public abstract class GameMovingObject : GameObject
    {
        protected double _dX;//מהירות
        protected double _dY;
        protected double _ddX;//תאוצה
        protected double _ddY;
        protected double _toX;
        protected double _toY;
        protected GameMovingObject(double placeX, double placeY, string fileName, Scene scene) : base(placeX, placeY, fileName, scene)
        {

        }
        public override void Render() //מתבצעת כל הזמן וגורמת להנעה של הדמות
        {
            _dX += _ddX;
            _dY += _ddY;

            _X += _dX;
            _Y += _dY;

            if (Math.Abs(_X - _toX) < 4 && Math.Abs(_Y - _toY) < 4)
            {
                Stop();
                _Y = _toY;
                _X = _toX;
            }
            base.Render();
        }
        public virtual void Stop()
        {
            _dX = _dY = _ddX = 0;
        }
        public void MoveTo(double toX, double toY, double Speed = 0, double ac = 0)
        {

            _toX = toX;
            _toY = toY;

            var len = Math.Sqrt(Math.Pow(_toX - _X, 2) + Math.Pow(_toY - _Y, 2));
            var cos = (_toX - _X) / len;
            var sin = (_toY - _Y) / len;

            Speed *= Constans.SpeedUnit;
            _dX = Speed * cos;
            _dY = Speed * sin;

            _ddX = ac * cos;
            _ddY = ac * sin;
        }
    }
}
