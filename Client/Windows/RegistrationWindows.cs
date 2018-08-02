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
using System.ComponentModel;

namespace Client.Windows
{
    public sealed class RegistrationWindows : BaseWindow
    {
        private Button _regButton;
        private Button _backButton;
        private TextBox _userNameText;
        private PasswordBox _passwordText;

        private TextBox _firstNameText;
        private TextBox _lastNameText;

        //Constractor
        public RegistrationWindows() : base(300, 500)
        {
            _window.Title = "Registration";

            SetGridRowsCols(10, 0);
            _myGrid.RowDefinitions[1].Height = new GridLength(80);//main title
            _myGrid.RowDefinitions[2].Height = new GridLength(35);//lable
            _myGrid.RowDefinitions[3].Height = new GridLength(45);//text box
            _myGrid.RowDefinitions[4].Height = new GridLength(35);
            _myGrid.RowDefinitions[5].Height = new GridLength(45);
            _myGrid.RowDefinitions[6].Height = new GridLength(35);
            _myGrid.RowDefinitions[7].Height = new GridLength(45);
            _myGrid.RowDefinitions[8].Height = new GridLength(35);
            _myGrid.RowDefinitions[9].Height = new GridLength(45);

            SetTitle("Welcome to", "TalkBack", "Massager");
            SetFieldsTitles();
            SetFields();
            SetButtons();

            //Help menu
            SetTopMenu(HelpString.HelpForRegistration,"How to registration");

            var menuItem = (MenuItem)_menuButton.ContextMenu.Items[0];
            menuItem.Header = "Registration";
            menuItem.Click += RegButton_Click;
            var action = (MenuItem)_menuButton.ContextMenu.Items[1];
            action.IsEnabled = false;
        }

        //Close Windows
        public void CloseRegWindow()
        {
            Timer.Stop();
            _window.Close();
            ClientInstances.Instance.Registration = null;
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

            TextBlock textBlock3 = new TextBlock
            {
                Text = "first name",
                FontSize = 15,
                Foreground = new SolidColorBrush(Colors.Beige),
                VerticalAlignment = VerticalAlignment.Bottom,
                TextAlignment = TextAlignment.Center,
            };
            Grid.SetRow(textBlock3, 6);
            _myGrid.Children.Add(textBlock3);

            TextBlock textBlock4 = new TextBlock
            {
                Text = "last name",
                FontSize = 15,
                Foreground = new SolidColorBrush(Colors.Beige),
                VerticalAlignment = VerticalAlignment.Bottom,
                TextAlignment = TextAlignment.Center,
            };
            Grid.SetRow(textBlock4, 8);
            _myGrid.Children.Add(textBlock4);
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

            _firstNameText = new TextBox
            {
                Name = "firstNameText",
                FontSize = 15,
                Height = 30,
                Width = 170,
                MaxLength = 15
            };
            Grid.SetRow(_firstNameText, 7);
            _myGrid.Children.Add(_firstNameText);

            _lastNameText = new TextBox
            {
                Name = "lastNameText",
                FontSize = 15,
                Height = 30,
                Width = 170,
                MaxLength = 15
            };
            Grid.SetRow(_lastNameText, 9);
            _myGrid.Children.Add(_lastNameText);
        }

        private void SetButtons()
        {
            StackPanel buttonsPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                //Margin = new Thickness(20,0,10,0),

            };
            _myGrid.Children.Add(buttonsPanel);
            Grid.SetRow(buttonsPanel, 10);

            _regButton = new Button
            {
                FontSize = 15,
                Height = 30,
                Width = 120,
                Content = "Registration",
                IsDefault = true,
                Background = new SolidColorBrush(Colors.BurlyWood),
                Margin = new Thickness(85, 0, 20, 0)
            };
            buttonsPanel.Children.Add(_regButton);
            _regButton.Click += RegButton_Click;

            _backButton = new Button
            {
                FontSize = 15,
                Height = 30,
                Width = 40,
                Content = "Back",
                IsDefault = true,
                Background = new SolidColorBrush(Colors.Chocolate)
            };
            buttonsPanel.Children.Add(_backButton);
            _backButton.Click += BackButton_Click;

        }

        #endregion Initialization

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Timer.Stop();
            ClientInstances.Instance.Login.Show();
            CloseRegWindow();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;

            var userName = _userNameText.Text;
            var pass = _passwordText.Password;
            var firstName = _firstNameText.Text;
            var lastName = _lastNameText.Text;

            if (userName != string.Empty && pass != string.Empty && firstName != string.Empty && lastName != string.Empty)
            {
                Host.RegisterAsync(userName, pass, firstName, lastName);
                ButtonsOnOff();
            }
            else messageBoxResult = MessageBox.Show("One or more fields is empty", "Warning");

        }

        public void ButtonsOnOff()
        {
            if (_window.IsActive)
            {
                var menuItem = (MenuItem)_menuButton.ContextMenu.Items[0];
                if (_regButton != null&& _backButton!=null&&menuItem!=null)
                {
                    _regButton.IsEnabled = !_regButton.IsEnabled;
                    _backButton.IsEnabled = !_backButton.IsEnabled;
                    menuItem.IsEnabled = !menuItem.IsEnabled;
                }
                if (_regButton.IsEnabled) Timer.Stop();
                else Timer.Start();
            }
        }

        private void ClearFields()
        {
            _userNameText.Text = string.Empty;
            _passwordText.Password = string.Empty;
            _firstNameText.Text = string.Empty;
            _lastNameText.Text = string.Empty;
        }

        //End Timer
        public override void Timer_Tick(object sender, EventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show($"The server does not respond. Try again later.");
            Application.Current.Shutdown();
        }
    }
}
