using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace TrafficLight.objects
{
    internal class Trafficlightss
    {
        private trafficlightsstate _state;//מצב בו יכול להמצא הרמזור

        private Ellipse _elipseRed;
        private Ellipse _elipseYellow;
        private Ellipse _elipseGreen;
        private bool _IsAuto;
        public Trafficlightss(Ellipse ElipseRed, Ellipse ElipseYellow, Ellipse ElipseGreen)
        {
            _elipseGreen = ElipseGreen;
            _elipseRed = ElipseRed;
            _elipseYellow = ElipseYellow;
            _IsAuto = false;
            _state=new trafficlightsstate();
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
            _elipseRed.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
            break;
            case trafficlightsstate.Yellow:
            _elipseRed.Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
            break;
            case trafficlightsstate.Red:
            _elipseRed.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
            break;
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
