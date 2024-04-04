using DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Tetris.GameServices;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
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
    public sealed partial class AccountPage : Page
    {
        public AccountPage()
        {
            this.InitializeComponent();
        }
        private void imgBack_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
        private void img_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Image img = (Image)sender;
            string name = img.Name;
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void img_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
            Image img = (Image)sender;
        }
        private void BtnExistYes_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void BtnNewYes_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (newName.Text == "" || newPass.Password == "" || newSamePass.Password == "")
            {
                await new MessageDialog("data is missing").ShowAsync();
            }
            else if (newPass != newSamePass)
            {
                await new MessageDialog("passwords not matching").ShowAsync();
            }
            else if(CheckPassword(newPass.Password))//check valid password
            {
                await new MessageDialog("passwords is not strong enough").ShowAsync();
            }
            else if (CheckMail(newPass.Password))//check valid mail need to add
            {
                await new MessageDialog("mail is not valid").ShowAsync();
            }
            else
            {
                int? userId = Server.ValidateUser(newName.Text.Trim(), newPass.Password.Trim());
                if(userId == null)
                {
                    //user is not in data base needs to be added
                    GameManager.Player = Server.AddNewUser(newName.Text.Trim(), newPass.Password.Trim());//add email
                    Frame.Navigate(typeof(MainPage));
                }
                else
                    await new MessageDialog("this accaount already exist, log in").ShowAsync();
            }
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
            ForgotPasswordGrid.Visibility = Visibility.Collapsed;
            BtnLogIn.Visibility = Visibility.Visible;
            BtnSignUp.Visibility = Visibility.Collapsed;
            LoginPassword.Password = "";
            LoginUser.Text = "";
        }

        private void text_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
            TextBlock img = (TextBlock)sender;
            img.Foreground = new SolidColorBrush(Colors.White);
        }

        private void text_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            TextBlock img = (TextBlock)sender;
            img.Foreground = new SolidColorBrush(Colors.LightGray);

        }

        private void txt_ForgotPassword_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            txt_ForgotPassword.Foreground = new SolidColorBrush(Colors.LightGray);
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void txt_ForgotPassword_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            txt_ForgotPassword.Foreground = new SolidColorBrush(Colors.White);
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }
        private void txt_ForgotPassword_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            ForgotPasswordGrid.Visibility = Visibility.Visible;
            existedUserGrid.Visibility = Visibility.Collapsed;
            ForgotPasswordUsername.Text = "";
            ForgotPasswordNew.Password = "";
            ForgotPasswordNewSame.Password = "";
            LoginUser.Text = "";
            LoginPassword.Password = "";
        }

        private void imgNewPasswordYes_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            ForgotPasswordGrid.Visibility = Visibility.Collapsed;
            existedUserGrid.Visibility = Visibility.Visible;
            ForgotPasswordUsername.Text = "";
            ForgotPasswordNew.Password = "";
            ForgotPasswordNewSame.Password = "";
        }
        private bool CheckMail(string mail)
        {
            return true;
        }
        private bool CheckPassword(string password)
        {
            return true;
        }
    }
}
