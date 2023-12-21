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
    internal class Jelly : GameObject
    {
        public enum jellyType
        {
            Green = 0,
            Pink = 1,
            Yellow = 2
        }
        private jellyType _jellyType;
        private Scene scene;
        public Jelly(Scene scene, jellyType jellyType, double width, double placeX, double placeY) : base(placeX, placeY, string.Empty, scene)
        {
            image.Width = width;
            image.Height = width;
            _jellyType = jellyType;
            SetImage();
        }
        private void SetImage()
        {
            switch (_jellyType)
            {
                case jellyType.Green:
                    base.SetImage("Jelly/JellyGreen.png");
                    break;
                case jellyType.Pink:
                    base.SetImage("Jelly/JellyPink.png");
                    break;
                case jellyType.Yellow:
                    base.SetImage("Jelly/JellyYellow.png");
                    break;
            }
        }

        internal void ChangeJelly()
        {
            switch(_jellyType)
            {
                case jellyType.Yellow:
                    _jellyType = jellyType.Pink;
                    break;
                case jellyType.Pink:
                    _jellyType = jellyType.Green;
                    break;
                case jellyType.Green:
                    _scene.RemoveObject(this);
                     break;
                    return;  
            }
            SetImage();
        }
    }
}
