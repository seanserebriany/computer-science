using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using static TrafficLight.objects.Trafficlights;

namespace TrafficLight.objects
{
    public abstract class Character
    {
        public enum CharacterStateType //המצבים שבהם הדמויות יכולות להימצא
        {
            standing, ready, going
        }
        protected Image _imageCharacter;
        protected BitmapImage _bitmapimage;
        protected CharacterStateType _characterState; // המצב
        public Character(Image imageAnimal)
        {
            _characterState = CharacterStateType.standing;
            _imageCharacter = imageAnimal;
            _bitmapimage = new BitmapImage();
            _imageCharacter.Source = _bitmapimage;
            MatchGifToState();
            Events.OnChangeState += SetState;//הרשמה לאירוע מעכשיו החייה מקשיבה לאירוע
        }
        /// <summary>
        /// פעולת תגובה לאירוע מצד החיה
        /// </summary>
        /// <param name="state"> המצב בעדכני של הרמזור</param>
        private void SetState(trafficlightsstate state)
        {
            switch (state)
            {
                case trafficlightsstate.Red:
                    _characterState = CharacterStateType.standing;
                        break;
                case trafficlightsstate.Yellow:
                    _characterState = CharacterStateType.ready;
                        break;
                case trafficlightsstate.Green:
                    _characterState = CharacterStateType.going;
                        break;
            }
            MatchGifToState();
        }

        protected virtual void MatchGifToState()
        {
            
        }
    }
}
