using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Tetris.GameServices;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Tetris
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void img_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            
        }

        private void img_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        private async void imgPlay_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //if (GameManager.Player.Name != "Annonymous")
            Frame.Navigate(typeof(LevelPage));
            //else
            //    await new MessageDialog("you need to log in first").ShowAsync();
        }

        private void imgAccount_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AccountPage));
        }

        private void imgLeaderboard_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(LeaderboardPage));
        }

        private void imgExit_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            popupGrid.Visibility = Visibility.Visible;
        }

        private void imgYesPop_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            popupGridMusic.Visibility = Visibility.Collapsed;
        }

        private void BackgroundMusic_Toggled(object sender, RoutedEventArgs e)
        {
            if (BackgroundMusic.IsOn)
            {
                BackgroundSound.Play("BackgroundMusic.mp3");
            }
            else
                BackgroundSound.Stop();
        }

        private void BackgroundVolumeSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            BackgroundSound.SetVolume(BackgroundVolumeSlider.Value);

        }

        private void imgMusic_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            popupGridMusic.Visibility = Visibility.Visible; 
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Manager.GameEvent.OnRun = null;
            Manager.GameEvent.OnKeyDown = null;
            if (BackgroundSound.isOn)
            {
                BackgroundMusic.IsOn = true;
                BackgroundVolumeSlider.Value = BackgroundSound.GetVolume();
            }
            else
            {
                BackgroundSound.Stop();
            }
        }

        private void imgHelp_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(HelpPage));
        }

        private void imgPopExit_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            popupGrid.Visibility = Visibility.Collapsed;
        }

        private void imgYes_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            CoreApplication.Exit();

        }

        private void imgAccount_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            if (GameManager.Player.Name != "Annonymous")
            {
                UserNameText.Text = GameManager.Player.Name;
                UserNameText.Visibility = Visibility.Visible;
            }
        }

        private void imgAccount_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
            if (GameManager.Player.Name != "Annonymous")
            {
                UserNameText.Visibility = Visibility.Collapsed;
            }
        }
    }
}
