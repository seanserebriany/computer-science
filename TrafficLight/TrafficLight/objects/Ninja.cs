using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficLight.objects;
using Windows.UI.Xaml.Controls;

namespace TrafficLight
{
    public class Ninja : Character
    {
        public Ninja(Image imageAnimal) : base(imageAnimal)
        {
        }
        protected override void MatchGifToState()
        {
            switch (_characterState)
            {
                case CharacterStateType.standing:
                    _bitmapimage.UriSource = new Uri("ms-appx:///Assets/Characters/ninja/ninja_idle.gif");
                    break;
                case CharacterStateType.ready:
                    _bitmapimage.UriSource = new Uri("ms-appx:///Assets/Characters/ninja/ninja_jump.gif");
                    break;
                case CharacterStateType.going:
                    _bitmapimage.UriSource = new Uri("ms-appx:///Assets/Characters/ninja/ninja_run.gif");
                    break;
            }
        }
    }
}
