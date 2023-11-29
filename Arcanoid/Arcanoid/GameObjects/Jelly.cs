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
        public Jelly(Scene scene, jellyType jellyType, double width, double placeX, double placeY) : base(scene, string.Empty, placeY, placeX)
        {

            Image.Width = width;
            Image.Height = width;
            _jellyType = jellyType;
            SetImage();
        }
        private void SetImage()
        {
            switch (_jellyType)
            {
                case jellyType.Green:
                    base.SetImage("jelly/jellyGreen.png");
                    break;
                case jellyType.Pink:
                    base.SetImage("jelly/jellyPink.png");
                    break;
                case jellyType.Yellow:
                    base.SetImage("jelly/jellyYellow.png");
                    break;
            }
        }
    }
}
