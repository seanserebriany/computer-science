using DatabaseProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Arcanoid.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LevelsPage : Page
    {
        public static Level Level = new Level();
        public LevelsPage()
        {
            this.InitializeComponent();
        }
        private void img_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Image img = (Image)sender;
            string name = img.Name;
            if(name == "imgLevel1")
                imgLevel1.Opacity = 0.5;
            if (name == "imgLevel2")
                imgLevel2.Opacity = 0.5;
            if (name == "imgLevel3")
                imgLevel3.Opacity = 0.5;
            if (name == "imgLevel4")
                imgLevel4.Opacity = 0.5;
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void img_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Image img = (Image)sender;
            string name = img.Name;
            if (name == "imgLevel1")
                imgLevel1.Opacity = 1;
            if (name == "imgLevel2")
                imgLevel2.Opacity = 1;
            if (name == "imgLevel3")
                imgLevel3.Opacity = 1;
            if (name == "imgLevel4")
                imgLevel4.Opacity = 1;
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        private void imgLevel1_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Level.LevelNumber = 1;
            Level.BarWidth = 300;
            Level.CountJellyGreen = 10;
            Level.CountJellyPurple = 4;
            Level.CountJellyYellow = 2;
            Level.BallSpeed = 1;

            Frame.Navigate(typeof(GamePag));
        }

        private void imgLevel2_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Level.LevelNumber = 1;
            Level.BarWidth = 250;
            Level.CountJellyGreen = 10;
            Level.CountJellyPurple = 4;
            Level.CountJellyYellow = 2;
            Level.BallSpeed = 2;
            Frame.Navigate(typeof(GamePag));

        }

        private void imgLevel3_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Level.LevelNumber = 1;
            Level.BarWidth = 190;
            Level.CountJellyGreen = 10;
            Level.CountJellyPurple = 4;
            Level.CountJellyYellow = 2;
            Level.BallSpeed = 3;

            Frame.Navigate(typeof(GamePag));

        }

        private void imgLevel4_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Level.LevelNumber = 1;
            Level.BarWidth = 100;
            Level.CountJellyGreen = 10;
            Level.CountJellyPurple = 4;
            Level.CountJellyYellow = 2;
            Level.BallSpeed = 4;

            Frame.Navigate(typeof(GamePag));

        }
    }
}
