using GameEngine.GameObjects;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Arcanoid.GameObjects
{
    internal class Jelly
    {
        protected double _X;
        protected double _Y;

        protected double _PlaceX;
        protected double _PlaceY;

        public Image Image { get; set; }
        protected string _fileName;

        public double Width => Image.ActualWidth;

        public double Height => Image.ActualHeight;

        public virtual Rect Rect => new Rect(_X, _Y, Width, Height);

        public bool Collisional { get; set; } = true;
        protected Scene _scene;

        public Jelly(Scene scene, string filename, double placeX, double placeY)
        {
            _scene = scene;
            _fileName = filename;
            _PlaceX = placeX;
            _PlaceY = placeY;
            Image = new Image();
            SetImage(filename);
            Render();
        }
        public virtual void Init()
        {
            _X = _PlaceX;
            _Y = _PlaceY;
        }
        public virtual void Collide(GameObject gameobject)
        {

        }
        public virtual void Render()
        {
            Canvas.SetLeft(Image, _X);
            Canvas.SetTop(Image, _Y);
        }
        public void SetImage(string filename)
        {
            Image.Source = new BitmapImage(new Uri($"/Assets/{filename}"));
        }
    }
}
