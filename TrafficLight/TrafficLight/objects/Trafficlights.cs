using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace TrafficLight.objects
{
    internal class Trafficlights
    {
        private trafficlightsstate _state;//מצב בו יכול להמצא הרמזור

        private Ellipse _elipseRed;
        private Ellipse _elipseYellow;
        private Ellipse _elipseGreen;
        private bool _isAuto;

        public bool IsAuto
        {
            get
            {
                return _isAuto;
            }
            set
            {
                if (value==true || value == false)
                {
                    _isAuto = value;
                }
                if(IsAuto==true)
                {
                    _timer.Start();
                }
                else
                {
                    _timer.Stop();
                }
            }
        }
        private DispatcherTimer _timer;


        public Trafficlights(Ellipse ElipseRed, Ellipse ElipseYellow, Ellipse ElipseGreen)
        {
            _elipseGreen = ElipseGreen;
            _elipseRed = ElipseRed;
            _elipseYellow = ElipseYellow;
            _isAuto = false;
            _state=new trafficlightsstate();
            _timer=new DispatcherTimer();// בניית טיימר
            _timer.Stop();
            _timer.Interval=TimeSpan.FromSeconds(2);// פסק זמן בין החלפת מצב הרמזור
            _timer.Tick += _timer_Tick;
        }

        private void _timer_Tick(object sender, object e)
        {
            SetState();
        }

        public enum trafficlightsstate
        {
            Red =0, Yellow=1, Green =2,
        }

        public void SetState()
        {
            Reset();
            switch(_state)
            {
            case trafficlightsstate.Green:
            _elipseRed.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
                    _state = trafficlightsstate.Red;
            break;
            case trafficlightsstate.Yellow:
            _elipseGreen.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
                    _state = trafficlightsstate.Green;
            break;
            case trafficlightsstate.Red:
            _elipseYellow.Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
                    _state = trafficlightsstate.Yellow;
            break;
            }
            if(Events.OnChangeState != null)//אם מישהו נרשם לאירוע אז
            {
                Events.OnChangeState(_state);//הפעלת האירוע
            }
            
        }
        //paint all elipse in black
        private void Reset()
        {
            _elipseRed.Fill = new SolidColorBrush(Windows.UI.Colors.Black);
            _elipseYellow.Fill = new SolidColorBrush(Windows.UI.Colors.Black);
            _elipseGreen.Fill = new SolidColorBrush(Windows.UI.Colors.Black);

        }
    }
}
