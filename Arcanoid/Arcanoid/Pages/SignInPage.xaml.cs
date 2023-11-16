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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Arcanoid.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignInPage : Page
    {
        public SignInPage()
        {
            this.InitializeComponent();
        }
        private void imgBack_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage));
        }

        private void img_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void img_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);

        }
        private void BtnExistYes_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage));
        }

        private void BtnNewYes_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage));
        }

        private void LogIn_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            newUserGrid.Visibility = Visibility.Collapsed;
            existedUserGrid.Visibility = Visibility.Visible;
            BtnSignUp.Visibility = Visibility.Visible;
            BtnLogIn.Visibility = Visibility.Collapsed;
            newName.Text = "";
            newPass.Password = "";
            newSamePass.Password = "";
        }

        private void SignUp_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            newUserGrid.Visibility = Visibility.Visible;
            existedUserGrid.Visibility = Visibility.Collapsed;
            BtnLogIn.Visibility = Visibility.Visible;
            BtnSignUp.Visibility = Visibility.Collapsed;
            LoginPassword.Password = "";
            LoginUser.Text = "";
        }
    }
}
