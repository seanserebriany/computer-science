using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficLight.objects;
using Windows.UI.Xaml.Controls;

namespace TrafficLight
{
    public class Dino : Character
    {
        public Dino(Image imageAnimal) : base(imageAnimal)
        {
        }
        protected override void MatchGifToState()
        {
            switch (_characterState)
            {
                case CharacterStateType.standing:
                    _bitmapimage.UriSource = new Uri("ms-appx:///Assets/Characters/dino/dino_idle.gif");
                    break;
                case CharacterStateType.ready:
                    _bitmapimage.UriSource = new Uri("ms-appx:///Assets/Characters/dino/dino_jump.gif");
                    break;
                case CharacterStateType.going:
                    _bitmapimage.UriSource = new Uri("ms-appx:///Assets/Characters/dino/dino_run.gif");
                    break;
            }
        }
    }
}
