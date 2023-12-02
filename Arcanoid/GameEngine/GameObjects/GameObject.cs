using GameEngine.GameServices;
using System;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace GameEngine.GameObjects
{
    public abstract class GameObject
    {
        protected double _X;
        protected double _Y;

        protected double _PlaceX;
        protected double _PlaceY;

        public Image image {  get; set; }
        protected string _FileName;

        public double Width => image.ActualWidth;
        public double Height => image.ActualHeight;

        public virtual Rect rect => new Rect(_X, _Y, Width, Height);
        public bool Collisional { get; set; } = true;
        protected Scene _scene; // Game Scene
        private Scene scene;
        private string empty;
        private double placeY;
        private double placeX;

        /// <summary>
        /// building a gameobject object
        /// </summary>
        /// <param name="x"> x pos </param>
        /// <param name="y"> y pos </param>
        /// <param name="placeX"> starting x pos </param>
        /// <param name="placeY"> starting y pos</param>
        /// <param name="image"> the image object</param>
        /// <param name="fileName"> the file name of the character </param>
        /// <param name="scene"> game scene </param>
        public GameObject(double placeX, double placeY, string fileName, Scene scene)
        {
            _X = placeX;
            _Y = placeY;
            _PlaceX = placeX;
            _PlaceY = placeY;
            _FileName = fileName;
            image = new Image();
            SetImage(_FileName);
            Render();
            _scene = scene;
        }

        public virtual void Init()
        {
            _X = _PlaceX;
            _Y = _PlaceY;
        }
        /// <summary>
        /// collision event with another game object 
        /// </summary>
        /// <param name="gameObject">the colliding object </param>
        public virtual void Collide(GameObject gameObject)
        {

        }
        /// <summary>
        /// renders the object position on the gird 
        /// </summary>
        public virtual void Render()
        {
            Canvas.SetLeft(image, _X);
            Canvas.SetTop(image, _Y);
        }
        /// <summary>
        /// sets the image object source with the given image file name
        /// </summary>
        /// <param name="FileName"></param>
        public void SetImage(string FileName)
        {
            image.Source = new BitmapImage(new Uri($"ms-appx:///Assets/{FileName}"));
        }
    }
}
