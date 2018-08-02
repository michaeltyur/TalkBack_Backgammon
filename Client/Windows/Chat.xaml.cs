using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Client.Windows
{
    /// <summary>
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class Chat : Window
    {
        public string User { get; }
        public ChatServer.ChatServiceClient Host { get; }
        private DispatcherTimer Timer;

        public Chat(string user)
        {
            InitializeComponent();
            Host = ClientInstances.Instance.Host;
            User = user;
            title.Text = User;
            howToChat.Text = TopMenu.GetHelpForChat();
            about.Text = TopMenu.GetAbout();
            titleHours.DataContext = ClientInstances.Instance.Time;
            titleMinutes.DataContext = ClientInstances.Instance.Time;
            titleSecond.DataContext = ClientInstances.Instance.Time;

            Timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromHours(1)

            };
            Timer.Start();
            Timer.Tick += Timer_Tick;

            GetDayTime();
            Show();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            GetDayTime();
        }

        //Drag Window
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void TopMenuButton_Click(object sender, RoutedEventArgs e)
        {
            topMenu.IsOpen = true;
        }

        public void RecieveMessage(string userName, string text)
        {
            messageArea.Items.Add($"[{userName}] {text}");
        }

        //Update User to OnLine List
        public void UpdateUser(List<string> userOnLine, List<string> userOffLine)
        {

            userOnlineList.Items.Clear();

            userOnlineList.Items.Add(
            new TextBlock
            {
                Text = "autoPlayer",
                Foreground = new SolidColorBrush(Colors.SlateGray)
            });

            userOfflineList.ItemsSource = userOffLine;

            userOfflineList.ItemsSource = userOffLine;

            foreach (var userName in userOnLine)
            {
                if (User == userName)
                {
                    userOnlineList.Items.Add(
                    new TextBlock
                    {
                        Text = userName,
                        Foreground = new SolidColorBrush(Colors.Red),
                        TextDecorations = TextDecorations.Underline
                    });
                }
                else
                {
                    TextBlock textBlock = new TextBlock
                    {
                        Text = userName
                    };
                    if (ClientInstances.Instance.GameList.ContainsKey(userName) ||
                       ClientInstances.Instance.PrivateChats.ContainsKey(userName))
                    {
                        textBlock.Foreground = new SolidColorBrush(Colors.Blue);
                    }
                    userOnlineList.Items.Add(textBlock);
                }
            }

        }

        /// <summary>
        /// Set color to users that current user games or chats
        /// </summary>
        public void UpdateUserList()
        {
            var gamersList = ClientInstances.Instance.GameList.Keys.ToList();
            var chatersList = ClientInstances.Instance.PrivateChats.Keys.ToList();

            foreach (var item in userOnlineList.Items)
            {
                var textBlock = (TextBlock)item;

                if (textBlock.Text != Player.autoPlayer.ToString()&& textBlock.Text !=User)
                {
                    textBlock.Foreground = new SolidColorBrush(Colors.Yellow);

                    foreach (var gamer in gamersList)
                    {
                        if (textBlock.Text == gamer)
                            textBlock.Foreground = new SolidColorBrush(Colors.Blue);
                    }
                    foreach (var chater in chatersList)
                    {
                        if (textBlock.Text == chater && textBlock.Text != Player.autoPlayer.ToString())
                            textBlock.Foreground = new SolidColorBrush(Colors.Blue);
                    }
                }
            }
        }

        private void GetDayTime()
        {
            var dayTimeNow = DateTime.Now.TimeOfDay.Hours;

            if (dayTimeNow > 4 && dayTimeNow < 13)
            {
                titleGreating.Text = $"Good Morning";
            }
            else if (dayTimeNow > 12 && dayTimeNow < 18)
            {
                titleGreating.Text = $"Good Day";
            }
            else if (dayTimeNow > 17 && dayTimeNow < 24)
            {
                titleGreating.Text = $"Good Evening";
            }
            else
            {
                titleGreating.Text = $"Good Night";
            }

        }

        #region Events  
        //Minimiz Window
        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void PrivateChatButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            if (userOnlineList.SelectedIndex >= 0)
            {
                var opponent = ((TextBlock)userOnlineList.SelectedItem).Text;

                if (opponent == User)
                    messageBoxResult = MessageBox.Show("Do you want to chat with your self? Ah ah ah!!!");
                else if (ClientInstances.Instance.PrivateChats.ContainsKey(opponent))
                {
                    Host.SendErrorMessageAsync(opponent, "This type really wants to talk to you, write to him urgently!");
                    messageBoxResult = MessageBox.Show("You have already open private chat with this user.");
                }
                else if (opponent == Player.autoPlayer.ToString())
                    messageBoxResult = MessageBox.Show("Do you want to chat with computer? Ah ah ah!!!");
                else if (ClientInstances.Instance.PrivateChats.ContainsKey(opponent))
                {
                    var window = ClientInstances.Instance.PrivateChats[opponent];
                    window.Topmost = true;
                    messageBoxResult = MessageBox.Show($"Private chat with {opponent} is open");
                }
                else Host.PrivateChat(User, opponent);

                userOnlineList.SelectedIndex = -1;
            }
            else messageBoxResult = MessageBox.Show("Please select a user for the private chat ");
            userOnlineList.SelectedIndex = -1;
        }

        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            if (userOnlineList.SelectedIndex >= 0)
            {
                var opponent = ((TextBlock)userOnlineList.SelectedItem).Text;
                if (opponent == User)
                    messageBoxResult = MessageBox.Show("Du you want to game with your self? Ah ah ah!!!");
                else if(ClientInstances.Instance.GameList.ContainsKey(opponent))
                    messageBoxResult = MessageBox.Show("You have already open game with this user");
                else if (opponent == Player.autoPlayer.ToString())
                    Host.StartHowToFirstAsync(User, Player.autoPlayer.ToString());
                else Host.RequestGameAsync(User, opponent);

                userOnlineList.SelectedIndex = -1;
            }
            else messageBoxResult = MessageBox.Show("Please select a user for start the game");
        }
        //Send Button Click
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (sendTextBox.Text != string.Empty)
            {
                var userName = User;
                var content = sendTextBox.Text;
                Host.SendMessageAsync(userName, content);
                sendTextBox.Text = string.Empty;
            }
        }

        //Login Button Click
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show($"Do you want to exit?", "Exit Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                foreach (var key in ClientInstances.Instance.GameList.Keys.ToList())
                {
                    if (ClientInstances.Instance.GameList.ContainsKey(key))
                    {
                        ClientInstances.Instance.GameList[key].Close();
                        ClientInstances.Instance.GameList.Remove(key);
                        Host.UserLeftGameAsync(User, key);
                    }
                }
                foreach (var key in ClientInstances.Instance.PrivateChats.Keys.ToList())
                {
                    ClientInstances.Instance.PrivateChats[key].Close();
                    ClientInstances.Instance.PrivateChats.Remove(key);//remove from private chats list
                    Host.ExitPrivateChatAsync(User, key);
                }
                Host.LogoutAsync(User);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show($"Do you want to exit?", "Exit Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                foreach (var key in ClientInstances.Instance.GameList.Keys.ToList())
                {
                    if (ClientInstances.Instance.GameList.ContainsKey(key))
                    {
                        ClientInstances.Instance.GameList[key].Close();
                        ClientInstances.Instance.GameList.Remove(key);
                        Host.UserLeftGameAsync(User, key);
                    }
                }
                foreach (var key in ClientInstances.Instance.PrivateChats.Keys.ToList())
                {
                    ClientInstances.Instance.PrivateChats[key].Close();
                    ClientInstances.Instance.PrivateChats.Remove(key);//remove from private chats list
                    Host.ExitPrivateChatAsync(User, key);
                }
                Host.LogoutAsync(User);

                Application.Current.Shutdown();
            }
        }
        #endregion

        private void SetColorForname(string name, Brush color)
        {

        }
    }
}
