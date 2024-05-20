using DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Tetris.GameServices;
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
using System.Text.RegularExpressions;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Tetris
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShopPage : Page
    {
        public ShopPage()
        {
            this.InitializeComponent();
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
        private async void btnSkin_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if(sender is Button btn)
            {
                if(btn.Content.Equals("Equip"))
                {
                    Server.SetCurrentProduct(GameManager.Player, Server.GetProductId(btn.Name));
                    this.Frame.Navigate(typeof(ShopPage));
                }
                else if(!btn.Content.Equals("Equipped"))
                {
                    if(GameManager.Player.Points >= Server.GetProductPrice(btn.Name))
                    {
                        GameManager.Player.Points -= Server.GetProductPrice(btn.Name);
                        Server.SetCurrentProduct(GameManager.Player, Server.GetProductId(btn.Name));
                        Server.Execute($"INSERT INTO UserProduct VALUES ('{GameManager.Player.Id}', '{Server.GetProductId(btn.Name)}')");
                        this.Frame.Navigate(typeof(ShopPage));
                    }
                    else
                    {
                        await new MessageDialog("Go work poor ni***").ShowAsync();
                    }
                }
            }
            //if(GameManager.Player.Points > 500)
            //{
            //    Server.AddUserProduct(GameManager.Player.Id);
            //    GameManager.Player.Points = GameManager.Player.Points - 500;
            //    btnCoolSkin.Visibility = Visibility.Collapsed;
            //}
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Server.UpdateUser(GameManager.Player);
            if (Server.ValidateProduct(GameManager.Player.Id, Server.GetProductId(CoolSkin.Name)))
            {
                CoolSkin.Content = "Equip";
            }
            if (Server.ValidateProduct(GameManager.Player.Id, Server.GetProductId(AngrySkin.Name)))
            {
                AngrySkin.Content = "Equip";
            }

            if(GameManager.Player.UsingProduct.Equals("CoolSkin"))
            {
                CoolSkin.Content = "equipped";
            }

            if (GameManager.Player.UsingProduct.Equals("AngrySkin"))
            {
                AngrySkin.Content = "equipped";
            }
        }
    }
}
