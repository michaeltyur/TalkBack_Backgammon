using Client.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Client.Windows
{
    public sealed class PrivateChatWindow : BaseWindow
    {

        private ListView _chatList;
        private TextBlock _title;
        private TextBox _sendText;
        private Button _sendButton;
        public Button _gameButton { get; set; }
        private string _opponentName;

        public PrivateChatWindow(string opponent) : base(330, 400)
        {
            _opponentName = opponent;
            _currentUserName = ClientInstances.Instance.Chat._currentUserName;
            _window.Title = "Private Chat";
           
            //grid
            SetGridRowsCols(3, 3);

            //Right Col
            _myGrid.ColumnDefinitions[2].Width = new GridLength(80);
            //Left Col
            _myGrid.ColumnDefinitions[0].Width = new GridLength(80);

            //Send Area Row
            _myGrid.RowDefinitions[3].Height = new GridLength(50);

            //Main Title
            _myGrid.RowDefinitions[1].Height = new GridLength(50);
            _title = new TextBlock
            {
                Text = $"Hi, {_currentUserName}",
                FontWeight = FontWeights.Bold,
                FontStyle = FontStyles.Italic,
                FontFamily = new FontFamily("Comic Sans MS, Verdana"),
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 20
            };
            _myGrid.Children.Add(_title);
            Grid.SetRow(_title, 1);
            Grid.SetColumn(_title, 1);

            //Chat Area
            SetChatArea();
            //Buttons Inisilization
            SetButtons();

            //Help menu
            SetTopMenu(HelpString.HelpForPrChat,"How to chat");

            SetHeaderTitle($"Chat with {_opponentName}");

            Grid.SetColumnSpan(_topPanel, 3);

            UpdateTopMenu();

            _window.Show();
        }

        private void UpdateTopMenu()
        {
            //Top Menu
            var menuItem = (MenuItem)_menuButton.ContextMenu.Items[0];
            menuItem.Header = "Back";
            menuItem.Click += ExitButton_Click;
            var back = (MenuItem)_menuButton.ContextMenu.Items[0];
            back.Click += ExitButton_Click;
            var action = (MenuItem)_menuButton.ContextMenu.Items[1];
            action.IsEnabled = true;

            //update action menu (Send msg)
            ((MenuItem)action.Items[0]).Click += SendButton_Click;//Send Message
            ((MenuItem)action.Items[1]).Click += GameButton_Click;//Start Game

            var help = (MenuItem)_menuButton.ContextMenu.Items[2];

        }

        //Add Users Names
        public void UpdateUsersName(string user, string opponent)
        {
            _currentUserName = user;
            _opponentName = opponent;
            _title.Text = $"Hi, {user}";

            SetHeaderTitle($"Chat with {opponent}");

            _sendText.IsEnabled = true;
            _sendButton.IsEnabled = true;
            _gameButton.IsEnabled = true;

            _window.Show();
        }

        private void SetChatArea()
        {
            _chatList = new ListView
            {
                Margin = new Thickness(3),
                Background = new SolidColorBrush(Colors.Bisque),
                FontSize = 18
            };

            _myGrid.Children.Add(_chatList);
            Grid.SetRow(_chatList, 2);
            Grid.SetColumn(_chatList, 0);

            Grid.SetColumnSpan(_chatList, 3);

            _sendText = new TextBox
            {
                FontSize = 20,
                Background = new SolidColorBrush(Colors.Bisque),
                Margin = new Thickness(5)
            };
            _myGrid.Children.Add(_sendText);
            Grid.SetRow(_sendText, 3);
            Grid.SetColumn(_sendText, 0);
            Grid.SetColumnSpan(_sendText, 2);
        }

        //Set Buttons
        private void SetButtons()
        {
            Button exitButton = new Button
            {

                Content = "Back",
                Margin = new Thickness(5),
                BorderThickness = new Thickness(2),
                Background = new SolidColorBrush(Colors.Blue),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 15
            };
            _myGrid.Children.Add(exitButton);
            Grid.SetRow(exitButton, 1);
            Grid.SetColumn(exitButton, 2);

            exitButton.Click += ExitButton_Click;

            _sendButton = new Button
            {

                Content = "Send",
                Margin = new Thickness(5),
                BorderThickness = new Thickness(2),
                Background = new SolidColorBrush(Colors.RosyBrown),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 15,
                IsDefault = true
            };
            _myGrid.Children.Add(_sendButton);
            Grid.SetRow(_sendButton, 3);
            Grid.SetColumn(_sendButton, 2);

            _sendButton.Click += SendButton_Click;

            _gameButton = new Button
            {

                Content = "Game",
                Margin = new Thickness(5),
                BorderThickness = new Thickness(2),
                Background = new SolidColorBrush(Colors.Brown),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 15
            };
            _myGrid.Children.Add(_gameButton);
            Grid.SetRow(_gameButton, 1);
            Grid.SetColumn(_gameButton, 0);

            _gameButton.Click += GameButton_Click;           
        }

        #region Game
        //Start Game
        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            _gameButton.IsEnabled = false;
            Host.RequestGameAsync(_currentUserName, _opponentName);
        }

        public void CloseGame()
        {
                _sendButton.IsEnabled = true;
                _gameButton.IsEnabled = true;
        }
        #endregion

        public void WindowsOpacity()
        {
            if (_window.Opacity != 1)
            {
                _window.Opacity = 1;
            }
            else _window.Opacity = 0.5;
        }





        //Send Button Event
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            var msg=string.Empty;
            if (_sendText!=null) msg=_sendText.Text;

            if (msg != string.Empty && _currentUserName != string.Empty && _opponentName != string.Empty)
            {
                Host.SendPrivateMsgAsync(_currentUserName, _opponentName, msg);
            }
            else Host.SendPrivateMsgAsync("ghost", "ghost", msg);

            _sendText.Text = string.Empty;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientInstances.Instance.PrivateChats.ContainsKey(_opponentName))
            {
                ClientInstances.Instance.PrivateChats.Remove(_opponentName);
            }

            Host.ExitPrivateChatAsync(_currentUserName, _opponentName);

            _window.Close();           
        }

        //Custom X button and log out
        public override void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientInstances.Instance.PrivateChats.ContainsKey(_opponentName))
            {
                ClientInstances.Instance.PrivateChats.Remove(_opponentName);
            }

            Host.ExitPrivateChatAsync(_currentUserName, _opponentName);

            _window.Close();
        }

        //public void ClosePrivateChat()
        //{
        //    _window.Close();
        //}

    }
}
