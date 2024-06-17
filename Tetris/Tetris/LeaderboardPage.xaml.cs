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
    public sealed partial class LeaderboardPage : Page
    {
        public LeaderboardPage()
        {
            this.InitializeComponent();
        }
        private void SetRaceRank()
        {
            List<Tuple<string, string>> Ranks = DataBase.Server.LeaderBoard();
            int row = 1;
            foreach (var r in Ranks)
            {
                TextBlock rankTextBlock = new TextBlock();
                rankTextBlock.Text = row.ToString();
                rankTextBlock.FontSize = 30;
                rankTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                rankTextBlock.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetRow(rankTextBlock, row);
                Grid.SetColumn(rankTextBlock, 0);

                TextBlock nameTextBlock = new TextBlock();
                nameTextBlock.Text = r.Item1;
                nameTextBlock.FontSize = 30;
                nameTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                nameTextBlock.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetRow(nameTextBlock, row);
                Grid.SetColumn(nameTextBlock, 1);

                TextBlock timeTextBlock = new TextBlock();
                timeTextBlock.Text = r.Item2;
                timeTextBlock.FontSize = 30;
                timeTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                timeTextBlock.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetRow(timeTextBlock, row);
                Grid.SetColumn(timeTextBlock, 2);

                TetrisRankGridDefenitions.Children.Add(rankTextBlock);
                TetrisRankGridDefenitions.Children.Add(nameTextBlock);
                TetrisRankGridDefenitions.Children.Add(timeTextBlock);

                row++;
            }
        }
        private void img_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Image img = (Image)sender;
            string name = img.Name;
            if (name == "Home")
                imgBack.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/Home.png"));
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void img_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
            Image img = (Image)sender;
            string name = img.Name;
            if (name == "Home")
                imgBack.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/Home.png"));
        }
        private void imgBack_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetRaceRank();
        }
    }
}
