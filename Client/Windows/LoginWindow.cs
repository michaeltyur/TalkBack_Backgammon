using Client.ChatServer;
using Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;

namespace Client.Windows
{
    public sealed class LoginWindow : BaseWindow
    {
        private Button _loginButton;
        private Button _regButton;
        private TextBox _userNameText;
        private PasswordBox _passwordText;

        //Private constractor
        public LoginWindow() : base(300, 400)
        {
            //Window Title
            _window.Title = "Log In";

            //Grid
            SetGridRowsCols(7, 0);

            _myGrid.RowDefinitions[1].Height = new GridLength(80);
            _myGrid.RowDefinitions[2].Height = new GridLength(35);
            _myGrid.RowDefinitions[3].Height = new GridLength(45);
            _myGrid.RowDefinitions[4].Height = new GridLength(35);
            _myGrid.RowDefinitions[5].Height = new GridLength(45);
            _myGrid.RowDefinitions[6].Height = new GridLength(60);
            _myGrid.RowDefinitions[7].Height = new GridLength(60);

            SetTitle("Welcome to", "TalkBack", "Massager");
            SetFieldsTitles();
            SetFields();
            SetButtons();

            SetTopMenu(HelpString.HelpForLogin, "How to login");

            //Update Top Menu
            var menuItem = (MenuItem)_menuButton.ContextMenu.Items[0];
            menuItem.Header = "Connect";
            menuItem.Click += LoginButton_Click;
            var action = (MenuItem)_menuButton.ContextMenu.Items[1];
            action.IsEnabled = false;

            _userNameText.Text = "pazit";
           _passwordText.Password = "123";
        }

        //Close Windows
        public void CloseLoginWindow()
        {
            Timer.Stop();
            _window.Close();
            ClientInstances.Instance.Login = null;
        }

        #region Initialization
        private void SetFieldsTitles()
        {
            TextBlock textBlock = new TextBlock
            {
                Text = "user name",
                FontSize = 15,
                Foreground = new SolidColorBrush(Colors.Beige),
                VerticalAlignment = VerticalAlignment.Bottom,
                TextAlignment = TextAlignment.Center,
            };
            Grid.SetRow(textBlock, 2);
            _myGrid.Children.Add(textBlock);

            TextBlock textBlock2 = new TextBlock
            {
                Text = "password",
                FontSize = 15,
                Foreground = new SolidColorBrush(Colors.Beige),
                VerticalAlignment = VerticalAlignment.Bottom,
                TextAlignment = TextAlignment.Center,
            };
            Grid.SetRow(textBlock2, 4);
            _myGrid.Children.Add(textBlock2);
        }

        private void SetFields()
        {
            _userNameText = new TextBox
            {
                Name = "userNameText",
                FontSize = 15,
                Height = 30,
                Width = 170,
                MaxLength = 10,
                CharacterCasing = CharacterCasing.Lower
            };
            Grid.SetRow(_userNameText, 3);
            _myGrid.Children.Add(_userNameText);

            _passwordText = new PasswordBox
            {
                Name = "passwordText",
                FontSize = 15,
                Height = 30,
                Width = 170,
                MaxLength = 10
            };
            Grid.SetRow(_passwordText, 5);
            _myGrid.Children.Add(_passwordText);
        }

        private void SetButtons()
        {
            _loginButton = new Button
            {
                Name = "loginButton",
                FontSize = 15,
                Height = 30,
                Width = 120,
                Content = "Log In",
                IsDefault = true,
                Background = new SolidColorBrush(Colors.BurlyWood),
                Margin = new Thickness(0, 10, 0, 0)
            };
            Grid.SetRow(_loginButton, 6);
            _myGrid.Children.Add(_loginButton);
            _loginButton.Click += LoginButton_Click;

            _regButton = new Button
            {
                Name = "regButton",
                FontSize = 15,
                Height = 30,
                Width = 120,
                Content = "Registration",
                Background = new SolidColorBrush(Colors.BurlyWood)
            };
            Grid.SetRow(_regButton, 7);
            _myGrid.Children.Add(_regButton);
            _regButton.Click += RegButton_Click;
        }
        #endregion

        public void ButtonsOnOff()
        {
            if (_window.IsActive)
            {
                var menuItem = (MenuItem)_menuButton.ContextMenu.Items[0];
                if (_loginButton != null && _regButton != null && menuItem != null)
                {
                    _loginButton.IsEnabled = !_loginButton.IsEnabled;
                    _regButton.IsEnabled = !_regButton.IsEnabled;
                    menuItem.IsEnabled = !menuItem.IsEnabled;
                }
                if (_regButton.IsEnabled) Timer.Stop();
                else Timer.Start();

            }
        }

        #region Button's Events
        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            ClientInstances.Instance.Registration.OpenWindow();
            CloseLoginWindow();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var userName = _userNameText.Text;
            var password = _passwordText.Password;
          
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                try
                {
                    Host.LoginAsync(userName, password);
                    ButtonsOnOff();
                }
                catch (Exception ex)
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show(ex.Message, "Warning");
                }
            }
            else 
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("One or more fields is empty", "Warning");
            }        
        }
        #endregion

        //End timer
        public override void Timer_Tick(object sender, EventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show($"The server does not respond. Try again later.");
            Application.Current.Shutdown();
        }
    }
}
