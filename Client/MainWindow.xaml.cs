using Client.Game;
using Client.Game.GameFinish;
using Client.Windows;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public object NotifyIcon { get; private set; }

        //ChatServer.ChatServiceClient host;
        public MainWindow()
        {
            InitializeComponent();
            myMainWindow.Hide();
            ClientInstances.Instance.Login = new Login();
            ClientInstances.Instance.Login.Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }

}
