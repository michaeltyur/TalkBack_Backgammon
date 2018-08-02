using Client.ChatServer;
using Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Client.Windows
{
    public class LoginWindow : BaseWindow
    {
        protected Button _loginButton;
        protected Button _regButton;
        protected TextBox _userNameText;
        protected PasswordBox _passwordText;

        public LoginWindow()
        {
            //Window Titl
            _window.Title = "Log In";

            //Grid
            SetGridRows(7);

            _myGrid.RowDefinitions[0].Height = new GridLength(130);
            _myGrid.RowDefinitions[1].Height = new GridLength(35);
            _myGrid.RowDefinitions[2].Height = new GridLength(45);
            _myGrid.RowDefinitions[3].Height = new GridLength(35);
            _myGrid.RowDefinitions[4].Height = new GridLength(45);

            SetTitle($"Welcome to\nTalkBack\nMassager");
            SetFieldsTitles(1, 3);
            SetFields(2, 4);
            SetButtons(5, 6);

            _window.Show();
        }

        private void SetFieldsTitles(int a, int b)
        {
            TextBlock textBlock = new TextBlock
            {
                Text = "user name",
                FontSize = 15,
                Foreground = new SolidColorBrush(Colors.Beige),
                VerticalAlignment = VerticalAlignment.Bottom,
                TextAlignment = TextAlignment.Center,
            };
            Grid.SetRow(textBlock, a);
            _myGrid.Children.Add(textBlock);

            TextBlock textBlock2 = new TextBlock
            {
                Text = "password",
                FontSize = 15,
                Foreground = new SolidColorBrush(Colors.Beige),
                VerticalAlignment = VerticalAlignment.Bottom,
                TextAlignment = TextAlignment.Center,
            };
            Grid.SetRow(textBlock2, b);
            _myGrid.Children.Add(textBlock2);
        }

        private void SetFields(int a, int b)
        {
            _userNameText = new TextBox
            {
                Name = "userNameText",
                FontSize = 15,
                Height = 30,
                Width = 170
            };
            Grid.SetRow(_userNameText, a);
            _myGrid.Children.Add(_userNameText);

            _passwordText = new PasswordBox
            {
                Name = "passwordText",
                FontSize = 15,
                Height = 30,
                Width = 170
            };
            Grid.SetRow(_passwordText, b);
            _myGrid.Children.Add(_passwordText);

            _userNameText.Text = "afek";
            _passwordText.Password = "123";
        }

        protected virtual void SetButtons(int a, int b)
        {
            _loginButton = new Button
            {
                Name = "LoginButton",
                FontSize = 15,
                Height = 30,
                Width = 120,
                Content = "Log In",
                Background = new SolidColorBrush(Colors.BurlyWood)
            };
            Grid.SetRow(_loginButton, a);
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
            Grid.SetRow(_regButton, b);
            _myGrid.Children.Add(_regButton);
            _regButton.Click += RegButton_Click;
        }

        protected virtual void RegButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindows regWindows = new RegistrationWindows();
            _window.Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var userName = _userNameText.Text;
            var password = _passwordText.Password;

            _host.Login(userName, password);       
        }
    }
}
