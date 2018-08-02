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
    public class RegistrationWindows : LoginWindow
    {
        private TextBox _firstNameText;
        private TextBox _lastNameText;



        public RegistrationWindows()
        {
            _window.Title = "Registration";
            _window.Height = 550;
            //_myGrid.ShowGridLines = true;

            SetGridRows(3);

            _myGrid.RowDefinitions[9].Height = new GridLength(70);

            SetFieldsTitles(5, 7);
            SetFields(6, 8);
            SetButtons(0, 9);
        }
        private void SetFieldsTitles(int a, int b)
        {
            //base.SetFields(a,b);
            TextBlock textBlock = new TextBlock
            {
                Text = "first name",
                FontSize = 15,
                Foreground = new SolidColorBrush(Colors.Beige),
                VerticalAlignment = VerticalAlignment.Bottom,
                TextAlignment = TextAlignment.Center,
            };
            Grid.SetRow(textBlock, a);
            _myGrid.Children.Add(textBlock);

            TextBlock textBlock2 = new TextBlock
            {
                Text = "last name",
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
            _firstNameText = new TextBox
            {
                Name = "firstNameText",
                FontSize = 15,
                Height = 30,
                Width = 170
            };
            Grid.SetRow(_firstNameText, a);
            _myGrid.Children.Add(_firstNameText);

            _lastNameText = new TextBox
            {
                Name = "lastNameText",
                FontSize = 15,
                Height = 30,
                Width = 170
            };
            Grid.SetRow(_lastNameText, b);
            _myGrid.Children.Add(_lastNameText);
        }

        protected override void SetButtons(int a, int b)
        {
            base.SetButtons(0, b);
            _myGrid.Children.Remove(_loginButton);

        }

        protected override void RegButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;

            var userName = _userNameText.Text;
            var pass = _passwordText.Password;
            var firstName = _firstNameText.Text;
            var lastName = _lastNameText.Text;

            if (userName != string.Empty && pass != string.Empty&&firstName!=string.Empty&&lastName!=string.Empty)
            {
                 _host.Register(userName, pass, firstName, lastName);
                 _host.Register(userName, pass, firstName, lastName);
            }
            else messageBoxResult = MessageBox.Show("Please enter name and password", "Warning");
        }
        private void ClearFields()
        {
            _userNameText.Text = string.Empty;
            _passwordText.Password = string.Empty;
            _firstNameText.Text = string.Empty;
            _lastNameText.Text = string.Empty;
        }
    }
}
