using Client.TalkBackService;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private DispatcherTimer Timer;

        private IChatService ChatHost { get; }

        public Login()
        {
            ChatHost = ClientInstances.Instance.ChatHost;
            InitializeComponent();

            //Set help and about text
            howToLogin.Text = TopMenu.GetHelpForLogin();
            about.Text = TopMenu.GetAbout();
            titleHours.DataContext = ClientInstances.Instance.Time;
            titleMinutes.DataContext = ClientInstances.Instance.Time;
            titleSecond.DataContext = ClientInstances.Instance.Time;
            Timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(15)
            };
            Timer.Tick += Timer_Tick;
            Show();
            userName.Text = "afek";
            password.Password = "123";
        }
        //Close Windows
        public void CloseLoginWindow()
        {
            Timer.Stop();
            Close();
            ClientInstances.Instance.Login = null;
        }

        #region Button's Events
        //Drag Window
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

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            ClientInstances.Instance.Registration = new Registration();
            ClientInstances.Instance.Registration.Show();
            CloseLoginWindow();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var _userName = userName.Text;
            var _password = password.Password;

            if (!string.IsNullOrEmpty(_userName) && !string.IsNullOrEmpty(_password))
            {
                try
                {
                    ChatHost.LoginAsync(_userName, _password);
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
        public  void Timer_Tick(object sender, EventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show($"The server does not respond. Try again later.");
            Application.Current.Shutdown();
        }

        public void ButtonsOnOff()
        {
                if (loginButton != null && regButton != null)
                {
                    loginButton.IsEnabled = !loginButton.IsEnabled;
                    regButton.IsEnabled = !regButton.IsEnabled;
                    topMenuLoginButton.IsEnabled = !topMenuLoginButton.IsEnabled;
                    topMenuRegButton.IsEnabled = !topMenuRegButton.IsEnabled;
                 }
                if (regButton.IsEnabled) Timer.Stop();
                else Timer.Start();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
