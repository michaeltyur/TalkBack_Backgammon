using Client.Game.GameData;
using Client.Game.GameOver;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Client.Game.GameFinish
{
    /// <summary>
    /// Interaction logic for HowIsFirst.xaml
    /// </summary>
    public partial class GameOver : Window
    {
        private DispatcherTimer Timer;
        private DispatcherTimer CloseTimer;
        private GameOverImages image;
        public GameOver(bool winner)
        {
            InitializeComponent();
            image = new GameOverImages(winner);
            windowBackground.DataContext = image;
            DataContext = this;

            Timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            CloseTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(10)
            };
            //Event Registration
            Timer.Tick += Timer_Tick;
            CloseTimer.Tick += CloseTimer_Tick;
            Timer.Start();
            CloseTimer.Start();
            Show();
        }

        private void CloseTimer_Tick(object sender, EventArgs e)
        {
            Timer.Stop();
            CloseTimer.Stop();
            Close();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            image.Image = image.ChangeImage();
        }

        // Drag Window
        private void MyMainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
