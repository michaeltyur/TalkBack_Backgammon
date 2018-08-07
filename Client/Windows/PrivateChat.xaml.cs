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

namespace Client.Windows
{
    /// <summary>
    /// Interaction logic for PrivateChat.xaml
    /// </summary>
    public partial class PrivateChat : Window
    {
        private  string User { get; }
        private  string Opponent { get; }
        private TalkBackService.IChatService ChatHost { get; }
        private TalkBackService.IGameService GameHost { get; }

        public PrivateChat(string user,string opponent)
        {
            InitializeComponent();
            ChatHost = ClientInstances.Instance.Chat.ChatHost;
            GameHost = ClientInstances.Instance.Chat.GameHost;
            User = user;
            Opponent = opponent;
            howToChat.Text = TopMenu.GetHelpForPrChat();
            about.Text = TopMenu.GetAbout();
            topTitle.Text += Opponent;
            title.Text += User;
            titleHours.DataContext = ClientInstances.Instance.Time;
            titleMinutes.DataContext = ClientInstances.Instance.Time;
            titleSecond.DataContext = ClientInstances.Instance.Time;
            Show();
        }

        //Moving windows
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void TopMenuButton_Click(object sender, RoutedEventArgs e)
        {
            topMenu.IsOpen = true;
        }
        //Minimiz Window
        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        //Start Game
        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            gameButton.IsEnabled = false;
            GameHost.RequestGameAsync(User, Opponent);
        }

        //Recieve Message
        public void RecieveMsg(string from, string content)
        {

                if (from == User)
                {
                    messageArea.Items.Add($"[you] {content}");
                }
                else messageArea.Items.Add($"[{from}] {content}");
            
        }

        //Send Button Event
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            var msg = sendTextBox.Text;

            if (msg != string.Empty && User != string.Empty && Opponent != string.Empty)
            {
                ChatHost.SendPrivateMsgAsync(User, Opponent, msg);
            }
            else ChatHost.SendPrivateMsgAsync("ghost", "ghost", msg);

            sendTextBox.Text = string.Empty;
        }

        //Custom X button and log out
        public  void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientInstances.Instance.PrivateChats.ContainsKey(Opponent))
            {
                ClientInstances.Instance.PrivateChats.Remove(Opponent);
            }
            ClientInstances.Instance.Chat.UpdateUserList();

            ChatHost.ExitPrivateChatAsync(User, Opponent);

            Close();
        }
    }
}
