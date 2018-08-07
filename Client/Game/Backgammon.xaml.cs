using Client.TalkBackService;
using Client.Game.GameData;
using Client.Game.GameLogic;
using Client.Game.UserControls;
using Client.TalkBackService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;


namespace Client.Game
{
    /// <summary>
    /// Interaction logic for Backgammon.xaml
    /// </summary>
    public partial class Backgammon : Window
    {
        private Random rand;
        public DispatcherTimer Timer;
        public СheckerLogic CheckerLogic { get; }

        public DiceAction DiceAction { get; }
        public Dice Dice1 { get; set; }
        public Dice Dice2 { get; set; }

        public Brush Black { get; set; }
        public Brush White { get; set; }
        public Brush Selected { get; set; }
        public Brush MyCheckerColor { get; set; }

        public bool Master { get; set; }

        public GameInfo GameInfo { get; set; }
        public BarGameInfo BarGameInfo { get; set; }
        public WhiteUser WhiteUser;
        public BlackUser BlackUser;

        public string User { get; set; }
        public string Opponent { get; set; }
        public IChatService ChatHost { get; set; }
        public IGameService GameHost { get; set; }

        public int IndexCollSelected { get; set; }

        #region Checkers
        //List of Collection
        public List<ObservableCollection<Checker>> ListAllCollections { get; set; }
        //List Of Bar Collection
        public List<ObservableCollection<Checker>> ListBarCollections { get; set; }

        //Bars
        public ObservableCollection<Checker> WhiteBar { get; set; }
        public ObservableCollection<Checker> BlackBar { get; set; }
        #endregion

        public Backgammon(string user, string opponent, bool master)
        {
            User = user;
            Opponent = opponent;
            Master = master;
            IndexCollSelected = -5;

            ChatHost = ClientInstances.Instance.Chat.ChatHost;
            GameHost= ClientInstances.Instance.Chat.GameHost;

            rand = new Random();
            DiceAction = new DiceAction(this);
            CheckerLogic = new СheckerLogic(this);

            Timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(10)
            };

            UIInitialization uIInitialization = new UIInitialization(this);

            //Event Registration
            Timer.Tick += Timer_Tick;
            checkBoxLeftDice.Checked += CheckBoxLeftDice_Checked;
            checkBoxRightDice.Checked += CheckBoxRightDice_Checked;

            if (Master) DiceAction.SetMaster();
            else
            {
                DiceAction.SetWaiter();
                if (Opponent == Player.autoPlayer.ToString())
                {
                    rollButton.IsEnabled = true;
                    rollButton.Content = "Start";
                    rollButton.Click -= RollButton_Click;
                    rollButton.Click += StartButton_Click;
                    resignButton.IsEnabled = false;
                    topResighButton.IsEnabled = false;
                }
            }
            Closed += Backgammon_Closed;

            Show();
            GameHost.GetGameTable(User, Opponent);
        }

        #region Events
        private void CheckBoxRightDice_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxLeftDice.IsChecked = false;
        }

        private void CheckBoxLeftDice_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxRightDice.IsChecked = false;
        }

        private void Backgammon_Closed(object sender, EventArgs e)
        {
            if (ClientInstances.Instance.GameList.ContainsKey(Opponent))
            {
                ClientInstances.Instance.GameList.Remove(Opponent);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Timer.Stop();
            Close();
        }

        //Drag Window
        private void MyMainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            gameWindow.DragMove();
        }

        //X Button
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Are you sure to close the game?", "Some Title", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                if (ClientInstances.Instance.GameList.ContainsKey(Opponent))
                {
                    GameHost.UserLeftGameAsync(User, Opponent);
                    ClientInstances.Instance.GameList.Remove(Opponent);
                }
                if(ClientInstances.Instance.Chat!=null)
                   ClientInstances.Instance.Chat.UpdateUserList();
                Close();

            }
        }

        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void TopMenuButton_Click(object sender, RoutedEventArgs e)
        {
            topMenu.IsOpen = true;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (Opponent == Player.autoPlayer.ToString())
            {
                GameHost.StartAutoPlayerAsync(User);
            }
            rollButton.Content = "Roll";
            rollButton.Click -= StartButton_Click;
            rollButton.Click += RollButton_Click;
            resignButton.IsEnabled = true;
            topResighButton.IsEnabled = true;
        }
        //Roll Click
        private void RollButton_Click(object sender, RoutedEventArgs e)
        {
            CheckerLogic.ClearSelections();
            if (Master)
            {
                GameHost.RollAsync(User, Opponent);

                rollButton.IsEnabled = false;
                moveButton.IsEnabled = true;

                if (checkBoxLeftDice != null) checkBoxLeftDice.IsEnabled = true;
                if (checkBoxRightDice != null) checkBoxRightDice.IsEnabled = true;
            }
        }

        private void MoveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            //int _selectedCollIndex = IndexCollSelected;
            //not shoised one or more components
            if (IndexCollSelected < -1)
            {
                messageBoxResult = MessageBox.Show("Please select the Checker", "Atention!");
                return;
            }
            if (checkBoxLeftDice.IsChecked == false && checkBoxRightDice.IsChecked == false)
            {
                messageBoxResult = MessageBox.Show("Please select any Dice or both", "Atention!");
                return;
            }
            if ((checkBoxLeftDice.IsChecked == false && checkBoxRightDice.IsEnabled == false) ||
                    (checkBoxRightDice.IsChecked == false && checkBoxLeftDice.IsEnabled == false))
            {
                messageBoxResult = MessageBox.Show("Please select the Dice", "Atention!");
                return;
            }

            var number = (checkBoxLeftDice.IsChecked == true) ? Dice1.Number : Dice2.Number;

            if (IndexCollSelected == -1)
            {
                if (MyCheckerColor == White)
                    GameHost.MoveCheckerFromBarAsync(User, Opponent, 0, number);
                else GameHost.MoveCheckerFromBarAsync(User, Opponent, 1, number);
            }
            else GameHost.MoveCheckerAsync(User, Opponent, IndexCollSelected, number);

            if (Dice1.Number != Dice2.Number && (checkBoxLeftDice.IsChecked == true || checkBoxRightDice.IsChecked == true))
            {

                if (checkBoxLeftDice.IsChecked == true)
                {
                    //CheckerLogic.CollUnSelected();
                    IndexCollSelected = -5;
                    checkBoxLeftDice.IsEnabled = false;
                }
                else if (checkBoxRightDice.IsChecked == true)
                {
                    //CheckerLogic.CollUnSelected();
                    IndexCollSelected = -5;
                    checkBoxRightDice.IsEnabled = false;
                }
            }
            CheckerLogic.CollUnSelected();
        }

        private void ResignButton_Click(object sender, RoutedEventArgs e)
        {
            if (Master)
            {
                GameHost.SetMasterAsync(User, Opponent);
                DiceAction.SetWaiter();
            }
            if (Opponent == Player.autoPlayer.ToString())
            {
                DiceAction.SetMaster();
            }
        }
        #endregion

        public void GameInfoUpdate(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                GameInfo.Info = content;
            }
        }

        public void CloseGame()
        {
            if (ClientInstances.Instance.GameList.ContainsKey(Opponent))
            {
                ClientInstances.Instance.GameList.Remove(Opponent);
            }
            if (ClientInstances.Instance.Chat != null)
                ClientInstances.Instance.Chat.UpdateUserList();
            Close();
        }

    }



}
