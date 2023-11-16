using Arcanoid.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.ApplicationModel.Core;
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

namespace Arcanoid
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MenuPage : Page
    {
        public MenuPage()
        {
            this.InitializeComponent();
        }
        private void img_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Image img = (Image)sender;
            string name = img.Name;
            if(name == "imgExit")
                imgExit.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/ExitP.png"));
            if (name == "imgSettings")
                imgSettings.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/SettingsP.png"));
            if (name == "imgPlay")
                imgPlay.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/PlayP.png"));
            if (name == "imgHelp")
                imgHelp.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/HelpP.png"));
            if (name == "imgMusic")
                imgMusic.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/MusicP.png"));
            if (name == "imgAccaunt")
                imgAccaunt.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/ProfileP.png"));
            if (name == "imgLeader")
                imgLeader.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/LeaderP.png"));
            if (name == "imgShop")
                imgShop.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/ShopP.png"));
            if (name == "imgLevel")
                imgLevel.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/LevelsP.png"));
            if (name == "imgPopExit")
                imgPopExit.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/ExitG.png"));
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void img_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Image img = (Image)sender;
            string name = img.Name;
            if (name == "imgExit")
                imgExit.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/ExitG.png"));
            if (name == "imgSettings")
                imgSettings.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/SettingsO.png"));
            if (name == "imgPlay")
                imgPlay.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/PlayO.png"));
            if (name == "imgHelp")
                imgHelp.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/HelpO.png"));
            if (name == "imgMusic")
                imgMusic.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/MusicO.png"));
            if (name == "imgAccaunt")
                imgAccaunt.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/ProfileO.png"));
            if (name == "imgLeader")
                imgLeader.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/LeaderO.png"));
            if (name == "imgShop")
                imgShop.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/ShopO.png"));
            if (name == "imgLevel")
                imgLevel.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/LevelsO.png"));
            if (name == "imgPopExit")
                imgPopExit.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/ExitO.png"));
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        private void imgPopExit_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            popupGrid.Visibility = Visibility.Collapsed;

        }

        private void imgYes_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            CoreApplication.Exit();

        }

        private void imgPopNo_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            popupGrid.Visibility = Visibility.Collapsed;
        }

        private void imgExit_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            popupGrid.Visibility = Visibility.Visible;

        }

        private void imgPopExit_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            imgPopExit.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/ExitG.png"));
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void imgPopExit_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            imgPopExit.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/ExitO.png"));
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        private void imgAccaunt_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignInPage));
        }

        private void imgLevel_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(LevelsPage));
        }
    }
}
