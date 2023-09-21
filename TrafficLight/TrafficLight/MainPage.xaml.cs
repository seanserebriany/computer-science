using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TrafficLight.objects;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Cryptography.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TrafficLight
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Trafficlights _trafficlights;
        public MainPage()
        {           
            this.InitializeComponent();
        }

        private void btnAuto_Click(object sender, RoutedEventArgs e)
        {
            _trafficlights.IsAuto = !_trafficlights.IsAuto;
            if(_trafficlights.IsAuto)
            {
                btnAuto.Content = "Stop";
            }
            else
            {
                btnAuto.Content = "Auto";
            }
            btnManual.IsEnabled = !_trafficlights.IsAuto;
        }

        private void btnManual_Click(object sender, RoutedEventArgs e)
        {
            _trafficlights.SetState();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _trafficlights = new Trafficlights(elpRed, elpYellow, elpGreen);//יצירת רמזור

        }
    }
}
