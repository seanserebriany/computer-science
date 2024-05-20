using DataBase;
using DataBase.Models;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Tetris.GameObjects;
using Tetris.GameServices;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Tetris
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LevelPage : Page
    {
        public static GameLevel Level = new GameLevel();
        public LevelPage()
        {
            this.InitializeComponent();
        }
        private void img_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Image img = (Image)sender;
            string name = img.Name;
            if (name == "imgLevel1")
                imgLevel1.Opacity = 0.5;
            if (name == "imgLevel2")
                imgLevel2.Opacity = 0.5;
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
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        private void imgLevel1_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

            Level.LevelNumber = 1;
            Level.Speed = 5;
            //Server.SetCurrentLevel(GameManager.Player, 1);
            Manager.GameEvent.Clear();
            Frame.Navigate(typeof(GamePage));
        }

        private void imgLevel2_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Level.LevelNumber = 2;
            Level.Speed = 10;
            //Server.SetCurrentLevel(GameManager.Player, 2);
            Frame.Navigate(typeof(GamePage));

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
