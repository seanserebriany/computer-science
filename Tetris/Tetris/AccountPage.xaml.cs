using DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Tetris.GameServices;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
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

        //log in
        private async void BtnExistYes_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            int? userID = Server.ValidateUser(LoginUser.Text.Trim(), LoginPassword.Password.Trim());

            if (userID.HasValue) // המשתמש אכן נמצא
            {
                GameManager.Player = Server.GetUser(userID.Value); // שליפת נתוני המשתמש
                await new MessageDialog("MyGame", "You have logged in successfully").ShowAsync();
                Frame.Navigate(typeof(MainPage));   // חוזרים לתפריט הראשי
            }
            else // נתונים לא נכונים
            {
                // יש להציג הודעה שהוזנו לא נכונים או המשתמש צריך לפתוח חשבון
                await new MessageDialog("MyGame", "User not found in DataBase").ShowAsync();
            }

        }

        //sign up
        private async void BtnNewYes_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (newName.Text == "" || SignUpPass.Password == "" || newSameSignUpPass.Password == "")
            {
                await new MessageDialog("data is missing").ShowAsync();
            }
            else if (SignUpPass.Password != newSameSignUpPass.Password)
            {
                await new MessageDialog("passwords not matching").ShowAsync();
            }
            else if (CheckPassword(SignUpPass.Password) == false)//check valid password
            {
                await new MessageDialog("passwords is not strong enough").ShowAsync();
            }
            else if (!IsMailIlegal(SignUpMail.Text))//check valid mail need to add
            {
                await new MessageDialog("mail is not valid").ShowAsync();
            }
            else
            {
                int? userId = Server.ValidateUser(newName.Text.Trim(), SignUpPass.Password.Trim());
                if (userId == null)
                {
                    //user is not in data base needs to be added
                    GameManager.Player = Server.AddNewUser(newName.Text.Trim(), SignUpPass.Password.Trim(), SignUpMail.Text.Trim());//add email instead of pass
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
            SignUpPass.Password = "";
            newSameSignUpPass.Password = "";
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
            ForgotPasswordNewPass.Password = "";
            ForgotPasswordNewSamePass.Password = "";
            LoginUser.Text = "";
            LoginPassword.Password = "";
        }

        //forgot password
        private async void imgNewPasswordYes_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (ForgotPasswordUsername.Text == "" || ForgotPasswordNewPass.Password == "" || ForgotPasswordNewSamePass.Password == "")
            {
                await new MessageDialog("data is missing").ShowAsync();
            }
            else if (ForgotPasswordNewPass.Password != ForgotPasswordNewSamePass.Password)
            {
                await new MessageDialog("passwords not matching").ShowAsync();
            }
            else if (CheckPassword(ForgotPasswordNewPass.Password) == false)//check valid password
            {
                await new MessageDialog("passwords is not strong enough").ShowAsync();
            }
            else
            {
                int? userID = Server.ValidateUserName(ForgotPasswordUsername.Text.Trim());
                if (userID == null)
                {
                    await new MessageDialog("there is no account with this username so you cant change it password.").ShowAsync();
                }
                else
                {
                    Server.ChangePassword(ForgotPasswordUsername.Text.Trim(), ForgotPasswordNewPass.Password.Trim());
                    await new MessageDialog("The password has changed sucsessfuly.").ShowAsync();
                }
            }
            ForgotPasswordGrid.Visibility = Visibility.Collapsed;
            existedUserGrid.Visibility = Visibility.Visible;
            ForgotPasswordUsername.Text = "";
            ForgotPasswordNewPass.Password = "";
            ForgotPasswordNewSamePass.Password = "";
        }


        private bool CheckPassword(string password)
        {
            //// Check length
            //if (password.Length < 8)
            //    return false;

            //// Check for at least one capital letter
            //if (!Regex.IsMatch(password, "[A-Z]"))
            //    return false;

            //// Check for at least one lowercase letter
            //if (!Regex.IsMatch(password, "[a-z]"))
            //    return false;

            //// Check for at least one number
            //if (!Regex.IsMatch(password, "[0-9]"))
            //    return false;

            //// Check for at least one special character
            //if (!Regex.IsMatch(password, "[^a-zA-Z0-9]"))
            //    return false;

            return true;
        }
    
    

        public static bool IsMailIlegal(string mail)
        {
            for (int i = 0; i < mail.Length; i++)
            {
                if (mail[i] == '@')
                {
                    return true;
                }
            }
            return false;
        }
    }
}
