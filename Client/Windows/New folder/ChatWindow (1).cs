using Client.ChatServer;
using Common.Data;
using Newtonsoft.Json;
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
    public class ChatWindow : BaseWindow
    {
        private static ListView _listOnLineUser;
        private static ListView _listOffLineUser;
        private TextBox _textSendBox;
        private Button _sendButton;
        private string _currentUserName;
        private static ListView _recieveMessages;

        public ChatWindow( string userName)
        {
            _window.Title = "Talk Back Chat";
            _window.Height = 550;
            _window.Width = 550;
            _window.Show();

            _currentUserName = userName;

            //Grid
            SetGrid(4, 2);
            //Main Title
            SetMainTitle($"Hi {_currentUserName}\nKappara Alaiha!!!");
            SetListUsers();
            SetListUsers();           
            SetSendMessageWindow();
            SetRecievMessageWindow();
        }
        //Set Rows and Columns for Grid
        private void SetGrid(int row, int col)
        {
            for (int i = 0; i < row; i++)
            {
                RowDefinition rowDef = new RowDefinition();
                _myGrid.RowDefinitions.Add(rowDef);
            }

            _myGrid.RowDefinitions[0].Height = new GridLength(90);
            _myGrid.RowDefinitions[_myGrid.RowDefinitions.Count - 1].Height = new GridLength(90);

            for (int i = 0; i < col; i++)
            {

                ColumnDefinition columnDef = new ColumnDefinition();
                _myGrid.ColumnDefinitions.Add(columnDef);
            }
            _myGrid.ColumnDefinitions[1].Width = new GridLength(170);
        }

        //Main Title
        private void SetMainTitle(string title)
        {
            SetTitle(title);
            Grid.SetColumn(_mainTitle, 0);
            Grid.SetColumnSpan(_mainTitle, 2);
        }

        //Set On/Off line users
        private void SetListUsers()
        {
            //ListOnLineUser
            _listOnLineUser = new ListView
            {
                Margin = new Thickness(5),
                Padding = new Thickness(5),
                Background = new SolidColorBrush(Colors.BlueViolet),
                FontSize = 15,
                FontWeight = FontWeights.Bold,
                FontStyle = FontStyles.Italic
            };

            _listOnLineUser.Foreground = new SolidColorBrush(Colors.Yellow);

            ListViewItem itm = new ListViewItem
            {
                Content = "users on line",
                Foreground = new SolidColorBrush(Colors.GreenYellow),
                IsSelected = false
            };

            _listOnLineUser.Items.Add(itm);


            _myGrid.Children.Add(_listOnLineUser);

            Grid.SetRow(_listOnLineUser, 1);
            Grid.SetColumn(_listOnLineUser, 1);

            //ListOffLineUser
            _listOffLineUser = new ListView
            {
                Margin = new Thickness(5),
                Padding = new Thickness(5),
                Background = new SolidColorBrush(Colors.BlueViolet),
                FontSize = 15,
                FontWeight = FontWeights.Bold,
                FontStyle = FontStyles.Italic,
            };

            _listOffLineUser.Foreground = new SolidColorBrush(Colors.Gray);

            ListViewItem itm2 = new ListViewItem
            {
                Content = "users off line",
                Foreground = new SolidColorBrush(Colors.LightGray),
                IsSelected = false
            };

            _listOffLineUser.Items.Add(itm2);

            _myGrid.Children.Add(_listOffLineUser);

            Grid.SetRow(_listOffLineUser, 2);
            Grid.SetColumn(_listOffLineUser, 1);
        }

        //Update User to OnLine List
        public static void UpdateUser(List<string> userOnLine, List<string> userOffLine)
        {

            if (_listOnLineUser != null && _listOffLineUser != null)
            {
                foreach (var name in userOnLine)
                {
                    _listOnLineUser.Items.Add($"* {name}");
                }

                foreach (var name in userOffLine)
                {
                    _listOffLineUser.Items.Add($"* {name}");
                }

            }
        }

        //Set Message Window
        public void SetSendMessageWindow()
        {
            _textSendBox = new TextBox
            {
                Width = 330,
                Height = 40,
                FontSize = 20,
                Background = new SolidColorBrush(Colors.Bisque)
            };

            _myGrid.Children.Add(_textSendBox);
            Grid.SetRow(_textSendBox, 3);
            Grid.SetColumn(_textSendBox, 0);

            _sendButton = new Button
            {
                Content = "Send Message",
                Height = 40,
                Width = 110,
                BorderThickness = new Thickness(2),
                Background = new SolidColorBrush(Colors.Blue),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 15

            };
            _myGrid.Children.Add(_sendButton);
            Grid.SetRow(_sendButton, 3);
            Grid.SetColumn(_sendButton, 1);

            _sendButton.Click += _sendButton_Click;
        }

        public void SetRecievMessageWindow()
        {
            _recieveMessages = new ListView
            {
                Margin = new Thickness(10),
                Background = new SolidColorBrush(Colors.Bisque),
                FontSize = 18
            };

            _myGrid.Children.Add(_recieveMessages);
            Grid.SetRow(_recieveMessages, 1);
            Grid.SetColumn(_recieveMessages, 0);
            Grid.SetRowSpan(_recieveMessages, 2);

        }

        private void _sendButton_Click(object sender, RoutedEventArgs e)
        {
            var userName = _currentUserName;
            var content = _textSendBox.Text;
            _host.SendMessageAsync(userName, content);
            _textSendBox.Text = string.Empty;
        }

        public static void RecieveMessage(string userName, string text)
        {

            if (_recieveMessages != null)
            {
                _recieveMessages.Items.Add($"[{userName}] {text}");

            }
        }

    }
}
