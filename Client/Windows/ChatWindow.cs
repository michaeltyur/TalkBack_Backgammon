using Client.Game;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Client.Windows
{
    public sealed class ChatWindow : BaseWindow
    {
        private static ListView _listOnLineUser;
        private static ListView _listOffLineUser;
        private TextBox _textSendBox;
        private Button _sendButton;
        private Button _logoutButton;

        //private Dictionary<string, PrivateChatWindow> _privateChats;
        //private Dictionary<string, Backgammon> _gamesList;

        private static ListView _recieveMessages;

        //Constractor
        public ChatWindow() : base(420, 550)
        {
            _window.Title = "Talk Back Chat";

            //_privateChats = ClientInstances.Instance.PrivateChats;
            //_gamesList = ClientInstances.Instance.GameList;
            _window.Show();

            //Grid
            SetGrid(6, 2);

            //Main Title
            SetMainTitle();
            SetListUsers();
            //SetListUsers();
            SetSendMessageWindow();
            SetRecievMessageWindow();
            SetButtons();

            //Help menu
            SetTopMenu(HelpString.HelpForChat,"How to chat");

            UpdateTopMenu();
        }

        #region Initialization UI Component

        private void UpdateTopMenu()
        {
            Grid.SetColumnSpan(_topPanel, 3);
            var menuItem = (MenuItem)_menuButton.ContextMenu.Items[0];
            menuItem.Header = "Disconect";
            menuItem.Click += _logoutButton_Click;
            var action = (MenuItem)_menuButton.ContextMenu.Items[1];
            action.IsEnabled = true;
            var help = (MenuItem)_menuButton.ContextMenu.Items[2];

            ((MenuItem)action.Items[0]).Click += _sendButton_Click;//Send Message

        }

        //Set Rows and Columns for Grid
        private void SetGrid(int row, int col)
        {
            for (int i = 0; i < row; i++)
            {
                RowDefinition rowDef = new RowDefinition();
                _myGrid.RowDefinitions.Add(rowDef);
            }

            _myGrid.RowDefinitions[1].Height = new GridLength(80);
            _myGrid.RowDefinitions[2].Height = new GridLength(25);
            _myGrid.RowDefinitions[4].Height = new GridLength(25);

            //bottom row
            _myGrid.RowDefinitions[_myGrid.RowDefinitions.Count - 1].Height = new GridLength(60);
            for (int i = 0; i < col; i++)
            {

                ColumnDefinition columnDef = new ColumnDefinition();
                _myGrid.ColumnDefinitions.Add(columnDef);
            }
            _myGrid.ColumnDefinitions[1].Width = new GridLength(120);
        }

        //Main Title
        private void SetMainTitle()
        {
            SetTitle("Hi Gest", "Kappara Alaiha!!!", string.Empty);
        }

        public void SetTitleContent(string name)
        {
            if (name != string.Empty) _currentUserName = name;
            else _currentUserName = "Gest";
            SetTitleText("Hi", name, "Kappara Alaiha!!!");

        }

        //Set On/Off line users
        private void SetListUsers()
        {
            //Titles
            TextBlock userOnLinelabel = new TextBlock
            {
                Text = "users on line",
                Foreground = new SolidColorBrush(Colors.GreenYellow),
                Height = 25,
                FontSize = 15,
                FontWeight = FontWeights.Bold,
                Padding = new Thickness(10, 0, 5, 0)

            };
            _myGrid.Children.Add(userOnLinelabel);
            Grid.SetRow(userOnLinelabel, 2);
            Grid.SetColumn(userOnLinelabel, 1);

            TextBlock userOffLinelabel = new TextBlock
            {
                Text = "users off line",
                Foreground = new SolidColorBrush(Colors.LightGray),
                Height = 25,
                FontSize = 15,
                FontWeight = FontWeights.Bold,
                Padding = new Thickness(10, 0, 5, 0)
            };
            _myGrid.Children.Add(userOffLinelabel);

            Grid.SetRow(userOffLinelabel, 4);
            Grid.SetColumn(userOffLinelabel, 1);

            //Lists of  users
            _listOnLineUser = new ListView
            {
                Margin = new Thickness(2),
                Padding = new Thickness(5, 0, 5, 0),
                Background = new SolidColorBrush(Colors.Transparent),
                BorderThickness = new Thickness(0),
                FontSize = 15,
                FontWeight = FontWeights.Bold,
                FontStyle = FontStyles.Italic,
                Foreground = new SolidColorBrush(Colors.Yellow)
            };
            _myGrid.Children.Add(_listOnLineUser);

            Grid.SetRow(_listOnLineUser, 3);
            Grid.SetColumn(_listOnLineUser, 1);

            //ListOffLineUser
            _listOffLineUser = new ListView
            {
                Margin = new Thickness(2),
                Padding = new Thickness(5, 0, 5, 0),
                Background = new SolidColorBrush(Colors.Transparent),
                BorderThickness = new Thickness(0),
                FontSize = 15,
                FontWeight = FontWeights.Bold,
                FontStyle = FontStyles.Italic,
            };

            _listOffLineUser.Foreground = new SolidColorBrush(Colors.Gray);

            _myGrid.Children.Add(_listOffLineUser);

            Grid.SetRow(_listOffLineUser, 5);
            Grid.SetColumn(_listOffLineUser, 1);
        }
        //Create Buttons()
        private void SetButtons()
        {
            StackPanel buttonsPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(0, 0, 0, 0),

            };
            _myGrid.Children.Add(buttonsPanel);
            Grid.SetRow(buttonsPanel, 1);
            Grid.SetColumn(buttonsPanel, 1);

            StackPanel bottomButtonsPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 0, 0, 0),
            };

            //Send Message Button
            _sendButton = new Button
            {
                Content = "Send",
                Height = 40,
                Width = 70,
                BorderThickness = new Thickness(2),
                Background = new SolidColorBrush(Colors.Blue),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 15,
                IsDefault = true
            };
            _myGrid.Children.Add(_sendButton);

            Grid.SetRow(_sendButton, 6);
            Grid.SetColumn(_sendButton, 1);
            _sendButton.Click += _sendButton_Click;

            //Log Out Button
            _logoutButton = new Button
            {
                Content = "Log Out",
                Height = 35,
                Width = 115,
                BorderThickness = new Thickness(2),
                Background = new SolidColorBrush(Colors.Brown),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 15,
                Margin = new Thickness(0, 5, 5, 2)
            };

            _logoutButton.Click += _logoutButton_Click;
            //Game
            Button _gameButton = new Button
            {
                Content = "Game",
                Height = 35,
                Width = 55,
                BorderThickness = new Thickness(2),
                Background = new SolidColorBrush(Colors.DarkBlue),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 15,
                Margin = new Thickness(0, 0,5, 0),
            };

            _gameButton.Click += _gameButton_Click;

            Button _privateChatButton = new Button
            {
                Content = "Chat",
                Height = 35,
                Width = 55,
                BorderThickness = new Thickness(2),
                Background = new SolidColorBrush(Colors.DarkBlue),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 15,
                Margin = new Thickness(0, 0, 5, 0),
            };
            _privateChatButton.Click += PrivateChatButton_Click;

            buttonsPanel.Children.Add(_logoutButton);
            buttonsPanel.Children.Add(bottomButtonsPanel);

            bottomButtonsPanel.Children.Add(_gameButton);
            bottomButtonsPanel.Children.Add(_privateChatButton);
        }
        #endregion

        //Close Windows
        public void CloseChatWindow()
        {
            _window.Close();
            ClientInstances.Instance.Chat = null;
        }

        //Show Windows
        public void ShowChatWindow()
        {
            _window.Show();

        }

        //Hide Windows
        public void HideChatWindow()
        {
            _window.Hide();

        }

        //Update User to OnLine List
        public void UpdateUser(List<string> userOnLine, List<string> userOffLine)
        {
            if (_listOnLineUser != null && _listOffLineUser != null)
            {
                _myGrid.Children.Remove(_listOnLineUser);
                _myGrid.Children.Remove(_listOffLineUser);

                SetListUsers();

                _listOnLineUser.Items.Clear();
                _listOffLineUser.Items.Clear();

                _listOffLineUser.ItemsSource = userOffLine;
                foreach (var userName in userOnLine)
                {
                    if (_currentUserName == userName)
                    {
                        _listOnLineUser.Items.Add(
                            new TextBlock
                            {
                                Text = userName,
                                Foreground = new SolidColorBrush(Colors.Red),
                                TextDecorations=TextDecorations.Underline
                            }
                            );
                    }
                    else
                    {
                        TextBlock textBlock= new TextBlock
                         {
                             Text = userName
                         };
                        if(ClientInstances.Instance.GameList.ContainsKey(userName) ||
                           ClientInstances.Instance.PrivateChats.ContainsKey(userName))
                        {
                            textBlock.Foreground = new SolidColorBrush(Colors.Blue);
                        }
                        _listOnLineUser.Items.Add(textBlock);
                    }
                 }                                               
            }
        }

        #region Message
        //Set Message Window
        public void SetSendMessageWindow()
        {
            _textSendBox = new TextBox
            {
                Height = 40,
                FontSize = 20,
                Background = new SolidColorBrush(Colors.Bisque),
                Margin = new Thickness(10)
            };

            _myGrid.Children.Add(_textSendBox);
            Grid.SetRow(_textSendBox, 6);
            Grid.SetColumn(_textSendBox, 0);
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
            Grid.SetRow(_recieveMessages, 2);
            Grid.SetColumn(_recieveMessages, 0);
            Grid.SetRowSpan(_recieveMessages, 4);

        }

        public static void RecieveMessage(string userName, string text)
        {

            if (_recieveMessages != null)
            {
                _recieveMessages.Items.Add($"[{userName}] {text}");

            }
        }
        #endregion

        #region Events  
        private void PrivateChatButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            if (_listOnLineUser.SelectedIndex >= 0)
            {
                var opponent = ((TextBlock)_listOnLineUser.SelectedItem).Text;

                if (opponent == _currentUserName)
                    messageBoxResult = MessageBox.Show("Du you want to chat with your self? Ah ah ah!!!");
                else if (ClientInstances.Instance.PrivateChats.ContainsKey(opponent))
                {
                    var window = ClientInstances.Instance.PrivateChats[opponent];
                    window.Topmost = true;
                    messageBoxResult = MessageBox.Show($"Private chat with {opponent} is open");
                }
                else Host.PrivateChat(_currentUserName, opponent);
            }
            else messageBoxResult = MessageBox.Show("Please select a user for the private chat ");
            _listOnLineUser.SelectedIndex = -1;
        }

        private void _gameButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            if (_listOnLineUser.SelectedIndex >= 0)
            {
                var opponent = ((TextBlock)_listOnLineUser.SelectedItem).Text;
                if (opponent == _currentUserName)
                    messageBoxResult = MessageBox.Show("Du you want to game with your self? Ah ah ah!!!");

                else Host.RequestGameAsync(_currentUserName, opponent);
            }
            else messageBoxResult = MessageBox.Show("Please select a user for start the game");
        }

        //Send Button Click
        private void _sendButton_Click(object sender, RoutedEventArgs e)
        {
            if (_textSendBox.Text != string.Empty)
            {
                var userName = _currentUserName;
                var content = _textSendBox.Text;
                Host.SendMessageAsync(userName, content);
                _textSendBox.Text = string.Empty;

            }
        }

        //Login Button Click
        private void _logoutButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var key in ClientInstances.Instance.GameList.Keys.ToList())
            {
                if (ClientInstances.Instance.GameList.ContainsKey(key))
                {
                   ClientInstances.Instance.GameList[key].Close();
                   ClientInstances.Instance.GameList.Remove(key);
                   Host.UserLeftGameAsync(_currentUserName,key);
                }
            }
            foreach (var key in ClientInstances.Instance.PrivateChats.Keys.ToList())
            {
                ClientInstances.Instance.PrivateChats[key].Close();
                ClientInstances.Instance.PrivateChats.Remove(key);//remove from private chats list
                Host.ExitPrivateChatAsync(_currentUserName, key); 
            }
            Host.LogoutAsync(_currentUserName);
        }
        #endregion

        #region Private Chat
        //Private Chat
        //public bool IsDoubleChatStart(string opponent)
        //{
        //    return ClientInstances.Instance.PrivateChats.ContainsKey(opponent);
        //}
        //Start Private Chat
        public void StartPrivateChat(string opponentName)
        {
            if (!string.IsNullOrEmpty(opponentName))
            {
                if (!ClientInstances.Instance.PrivateChats.ContainsKey(opponentName))
                {
                    ClientInstances.Instance.PrivateChats.Add(opponentName, new PrivateChat(_currentUserName,opponentName));
                }
            }
        }

        #endregion
    }
}
