
using Client.Game.GameData;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Client.Game
{
    /// <summary>
    /// Interaction logic for HowIsFirst.xaml
    /// </summary>
    public partial class HowIsFirst : Window
    {
        public TalkBackService.IChatService ChatHost { get; }
        public TalkBackService.IGameService GameHost { get; }

        //private Backgammon _game;
        private DispatcherTimer Timer;

        public Dice Dice1 { get; set; }
        public Dice Dice2 { get; set; }

        public GameInfo GameInfo { get; set; }
        public bool IsLeftPlayer { get; set; }
        private bool _winner;

        private string _yourDice = string.Empty;

        private string User { get; set; }
        private string Opponent { get; set; }

        public HowIsFirst(string user,string opponent)
        {
            InitializeComponent();
            User = user;
            Opponent = opponent;

            ChatHost = ClientInstances.Instance.ChatHost;
            GameHost = ClientInstances.Instance.GameHost;

            Dice1 = new Dice();
            Dice2 = new Dice();

            _yourDice = "Roll the Dice";

            GameInfo = new GameInfo { Info = $"Hi, {User}!\nHow is first?! Roll Dice !!!" };

            diceImage1.DataContext = Dice1;
            diceImage2.DataContext = Dice2;
            gameInfo.DataContext = GameInfo;
            DataContext = this;

            Timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2)
            };
            //Event Registration
            Timer.Tick += Timer_Tick;
            Show();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Timer.Stop();
            GameHost.StartGameAsync(User, Opponent, _winner);

            if (ClientInstances.Instance.HowFirstsList.ContainsKey(Opponent))
            {
                ClientInstances.Instance.HowFirstsList.Remove(Opponent);
            }
            Close();
        }

        public void HowFirstClose(bool winner)
        {           
            _winner = winner;

            if (winner)
            {
                GameInfo.Info = "You are Winner!\nYou start first.\nGood Like!";
            }
            else GameInfo.Info = "Your opponent is winner!\nHis starts first.\nGood Like!";

            Timer.Start();
        }

        public void HowFirstClose()
        {
            if (ClientInstances.Instance.HowFirstsList.ContainsKey(Opponent))
            {
                ClientInstances.Instance.HowFirstsList.Remove(Opponent);
            }
            Close();
        }
        // Drag Window
        private void MyMainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        //Roll Click
        private void RollButton_Click(object sender, RoutedEventArgs e)
        {          
            GameHost.RollHowFirstAsync(User,Opponent);
            rollButton.IsEnabled = false;
        }

        public void RecieveRoll(int[] numbers)
        {
            Dice1.Image = Dice.GetDiceImage(numbers[0]);
            Dice1.Number = numbers[0];

            Dice2.Image = Dice.GetDiceImage(numbers[1]);
            Dice2.Number = numbers[1];
        }

        public void GameInfoUpdate(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                GameInfo.Info = content;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {

            ChatHost.BackToChat(User,Opponent);

            if (ClientInstances.Instance.HowFirstsList.ContainsKey(Opponent))
            {
                ClientInstances.Instance.HowFirstsList[Opponent].Close();
                ClientInstances.Instance.HowFirstsList.Remove(Opponent);
            }
        }

        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
