using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace TrafficLight.objects
{
    public abstract class Character
    {
        public enum CharacterStateType //המצבים שבהם הדמויות יכולות להימצא
        {
            standing, ready, going
        }
        private Image _imageCharacter;
        private BitmapImage _bitmapimage;
        private CharacterStateType _animalState; // המצב
        public Character(Image imageAnimal)
        {
            _animalState = CharacterStateType.standing;
            _imageCharacter = imageAnimal;
            _bitmapimage = new BitmapImage();
            _imageCharacter.Source = _bitmapimage;
            MatchGifToState();
        }

        protected virtual void MatchGifToState()
        {
            
        }
    }
}
