using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using LyricalMiracle.Entities;
using LyricalMiracle.BusinessLogic;
using iTunesLib;
using System.Windows.Interop;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;

namespace LyricalMiracle.UI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        #region Properties

        private User user = new User();
        private LoginLogic LLInstance = new LoginLogic();
        private UserLogic ULInstance = new UserLogic();

        static byte[] s_aditionalEntropy = { 9, 8, 7, 6, 5 };

        #endregion

        public LoginWindow()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            { 
                throw ex;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement parent = (FrameworkElement)LoginTbUser.Parent;
            while (parent != null && parent is IInputElement && !((IInputElement)parent).Focusable)
            {
                parent = (FrameworkElement)parent.Parent;
            }

            DependencyObject scope = FocusManager.GetFocusScope(LoginTbUser);
            FocusManager.SetFocusedElement(scope, parent as IInputElement);
        }

        private void LoginBtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = LoginTbUser.Text;
            string password = LoginTbPassword.Password;

            UILoading(true);

            if (LLInstance.VerifyPassword(username, password))
            {
                if (LoginCbRemember.IsChecked.Value)
                {
                    //Code pour lever le flag du autoconnect
                }

                user = ULInstance.GetUser(username);

                MainWindow main = new MainWindow(user);
                App.Current.MainWindow = main;
                this.Close();
                main.Show();
            }
            else
            {
                MessageBox.Show(Properties.Resources.LoginWrongPassword);
            }
        }

        private void LoginTbUser_GotFocus(object sender, RoutedEventArgs e)
        {
            if (LoginTbUser.Foreground == Brushes.Gray)
            {
                LoginTbUser.Text = string.Empty;
                LoginTbUser.Foreground = Brushes.Black;
            }
        }

        private void LoginTbUser_LostFocus(object sender, RoutedEventArgs e)
        {
            if (LoginTbUser.Text == string.Empty)
            {
                LoginTbUser.Text = Properties.Resources.LoginUsername;
                LoginTbUser.Foreground = Brushes.Gray;
            }
        }

        private void LoginTbPasswordText_GotFocus(object sender, RoutedEventArgs e)
        {
            LoginTbPasswordText.Visibility = System.Windows.Visibility.Hidden;
            LoginTbPassword.Visibility = System.Windows.Visibility.Visible;
            LoginTbPassword.Focus();
        }

        private void LoginTbPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!LoginTbPassword.IsFocused && LoginTbPassword.Password == string.Empty)
            {
                LoginTbPassword.Visibility = System.Windows.Visibility.Hidden;
                LoginTbPasswordText.Visibility = System.Windows.Visibility.Visible;
            }
        }

        #region Utilitaries

        private void UILoading(bool loading)
        {
            if (loading)
            {
                LoadingGIF.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => LoadingGIF.Visibility = System.Windows.Visibility.Visible));
            }
            else
            {
                LoadingGIF.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => LoadingGIF.Visibility = System.Windows.Visibility.Hidden));
            }
        }

        public static byte[] Protect(byte[] data)
        {
            try
            {
                // Encrypt the data using DataProtectionScope.CurrentUser. The result can be decrypted 
                //  only by the same current user. 
                return ProtectedData.Protect(data, s_aditionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("Data was not encrypted. An error occurred.");
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public static byte[] Unprotect(byte[] data)
        {
            try
            {
                //Decrypt the data using DataProtectionScope.CurrentUser. 
                return ProtectedData.Unprotect(data, s_aditionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("Data was not decrypted. An error occurred.");
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        #endregion
    }
}
