using Arcanoid.GameServices;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Isolation;
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
        private GameManager _GameManeger;

        public GamePag()
        {
            this.InitializeComponent();
        }
        private void img_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Image img = (Image)sender;
            string name = img.Name;
            if (name == "imgExit")
                imgExit.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/ExitP.png"));
            if (name == "imgHelp")
                imgHelp.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/HelpP.png"));
            if (name == "imgMusic")
                imgMusic.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/MusicP.png"));
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void img_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Image img = (Image)sender;
            string name = img.Name;
            if (name == "imgExit")
                imgExit.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/ExitO.png"));
            if (name == "imgHelp")
                imgHelp.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/HelpO.png"));
            if (name == "imgMusic")
                imgMusic.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/MusicO.png"));
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        private void imgExit_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage));
        }

        private void imgHelp_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(HelpPage));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _GameManeger = new GameManager(scene);
            _GameManeger.Start();
            Manager.GameEvent.OnRemoveHeart += RemoveHeart;
            Manager.GameEvent.OnAddScore += AddScore;
            Manager.GameEvent.GameOver += GameOver;


        }

        private void GameOver()
        {
            Frame.Navigate(typeof(Win));
        }

        private void AddScore(int score)
        {
            if (int.TryParse(txt_score.Text, out int currentScore))
            {
                int newScore = 0;
                newScore = currentScore + score;
                txt_score.Text = newScore.ToString();
            }
        }

        private void RemoveHeart(int lifes)
        {
            if(lifes == 2)
            {
                Heart1.Visibility = Visibility.Collapsed;
            }
            if (lifes == 1)
            {
                Heart2.Visibility = Visibility.Collapsed;
            }
            if (lifes == 0)
            {
                Heart3.Visibility = Visibility.Collapsed;
                Frame.Navigate(typeof(Loose));

            }
        }
    }
}
