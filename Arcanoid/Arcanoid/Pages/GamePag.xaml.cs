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
    public sealed partial class GamePag : Page
    {
        public GamePag()
        {
            this.InitializeComponent();
        }
        private void imgSettings_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            popupGridMenu.Visibility = Visibility.Visible;
            imgSettings.Visibility = Visibility.Collapsed;
        }
        private void imgYes_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            popupGridMenu.Visibility = Visibility.Collapsed;
            imgSettings.Visibility = Visibility.Visible;
        }
        private void img_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Image img = (Image)sender;
            string name = img.Name;
            if (name == "imgExit")
                imgExit.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/ExitP.png"));
            if (name == "imgSettings")
                imgSettings.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/SettingsP.png"));
            if (name == "imgHelp")
                imgHelp.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/HelpP.png"));
            if (name == "imgMusic")
                imgMusic.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/MusicP.png"));
            if (name == "imgYes")
                imgYes.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/YesO.png"));
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void img_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Image img = (Image)sender;
            string name = img.Name;
            if (name == "imgExit")
                imgExit.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/ExitO.png"));
            if (name == "imgSettings")
                imgSettings.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/SettingsO.png"));
            if (name == "imgHelp")
                imgHelp.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/HelpO.png"));
            if (name == "imgMusic")
                imgMusic.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/MusicO.png"));
            if (name == "imgYes")
                imgYes.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/YesG.png"));
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        private void imgExit_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage));
        }
    }
}
