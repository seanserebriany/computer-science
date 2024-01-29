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

namespace Tetris
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        public GamePage()
        {
            this.InitializeComponent();
        }
        private void img_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Image img = (Image)sender;
            string name = img.Name;
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void img_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Image img = (Image)sender;
            string name = img.Name;
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        private void imgExit_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void imgHelp_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(HelpPage));
        }

        private void imgHome_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        //private void Page_Loaded(object sender, RoutedEventArgs e)
        //{
        //    _GameManeger = new GameManager(scene);
        //    _GameManeger.Start();
        //    Manager.GameEvent.OnRemoveHeart += RemoveHeart;
        //    Manager.GameEvent.OnAddScore += AddScore;
        //    Manager.GameEvent.GameOver += GameOver;


        //}

        //private void GameOver()
        //{
        //    Frame.Navigate(typeof(Win));
        //}

        //private void AddScore(int score)
        //{
        //    if (int.TryParse(txt_score.Text, out int currentScore))
        //    {
        //        int newScore = 0;
        //        newScore = currentScore + score;
        //        txt_score.Text = newScore.ToString();
        //    }
        //}
    }
}
