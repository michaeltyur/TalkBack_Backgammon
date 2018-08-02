using Client.ChatServer;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Client.Windows
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private DispatcherTimer Timer;
        private ChatServiceClient Host { get; }

        public Registration()
        {
            Host = ClientInstances.Instance.Host;
            InitializeComponent();

            //Set help and about text
            howToLogin.Text = TopMenu.GetHelpForRegistration();
            about.Text = TopMenu.GetAbout();

            titleHours.DataContext = ClientInstances.Instance.Time;
            titleMinutes.DataContext = ClientInstances.Instance.Time;
            titleSecond.DataContext = ClientInstances.Instance.Time;

            Timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(30)
            };
            Timer.Tick += Timer_Tick;
            Show();
        }

        //Close Windows
        public void CloseRegWindow()
        {
            Timer.Stop();
            Close();
            ClientInstances.Instance.Registration = null;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Timer.Stop();
            ClientInstances.Instance.Login.Show();
            CloseRegWindow();
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

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;

            var _userName = userName.Text;
            var _pass = password.Password;
            var _firstName = firstName.Text;
            var _lastName = lastName.Text;

            if (_userName != string.Empty && _pass != string.Empty && _firstName != string.Empty && _lastName != string.Empty)
            {
                Host.RegisterAsync(_userName, _pass, _firstName, _lastName);
                ButtonsOnOff();
            }
            else messageBoxResult = MessageBox.Show("One or more fields is empty", "Warning");

        }
        public void ButtonsOnOff()
        {
            if (regButton != null)
            {
                regButton.IsEnabled = !regButton.IsEnabled;
                backButton.IsEnabled = !backButton.IsEnabled;
                userName.IsEnabled = !userName.IsEnabled;
                password.IsEnabled = !password.IsEnabled;
                firstName.IsEnabled = !firstName.IsEnabled;
                lastName.IsEnabled = !lastName.IsEnabled;
                topMenuRegButton.IsEnabled = !topMenuRegButton.IsEnabled;
                topMenuBackButton.IsEnabled = !topMenuBackButton.IsEnabled;
            }
            if (regButton.IsEnabled) Timer.Stop();
            else Timer.Start();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //End Timer
        public void Timer_Tick(object sender, EventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show($"The server does not respond. Try again later.");
            Application.Current.Shutdown();
        }
    }

}
