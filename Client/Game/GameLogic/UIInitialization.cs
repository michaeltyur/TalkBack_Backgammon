using Client.Game.GameData;
using Client.Game.UserControls;
using Client.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Client.Game.GameLogic
{
    /// <summary>
    /// is responsible for initializing ui components of Backgammon.xaml
    /// </summary>
    /// 
    public class UIInitialization
    {
        private Backgammon _window;

        public UIInitialization(Backgammon window)
        {
            window.InitializeComponent();

            if (window.Master)
            {
                window.WhiteUser = new WhiteUser(window);
                Grid.SetRow(window.WhiteUser, 1);
                Grid.SetColumn(window.WhiteUser, 0);
                window.windowGrid.Children.Add(window.WhiteUser);

            }
            else
            {
                window.BlackUser = new BlackUser(window);
                Grid.SetRow(window.BlackUser, 1);
                Grid.SetColumn(window.BlackUser, 0);
                window.windowGrid.Children.Add(window.BlackUser);

            }
            _window = window;

            _window.Dice1 = new Dice();
            _window.Dice2 = new Dice();
            _window.BarGameInfo = new BarGameInfo();

            SetColors();
            SetColection();
            SetTopMenu();
            SetStartInfo();
           
            //Binding
            _window.titleLeft.Text = $"Backgammon with {_window.Opponent}";
            _window.titleCenter.DataContext = _window.BarGameInfo;
            _window.titleRight.DataContext = _window.BarGameInfo;
            //_window.opponentName.DataContext = _window.Opponent;
            _window.userName.DataContext = $"Dear {_window.User}";
            _window.diceImage1.DataContext = _window.Dice1;
            _window.diceImage2.DataContext = _window.Dice2;
            _window.gameInfo.DataContext = _window.GameInfo;
            _window.DataContext = _window;

            #region Events of Collections Registration
            _window.ListAllCollections[0].CollectionChanged += Col1_CollectionChanged;
            _window.ListAllCollections[1].CollectionChanged += Col2_CollectionChanged;
            _window.ListAllCollections[2].CollectionChanged += Col3_CollectionChanged;
            _window.ListAllCollections[3].CollectionChanged += Col4_CollectionChanged;
            _window.ListAllCollections[4].CollectionChanged += Col5_CollectionChanged;
            _window.ListAllCollections[5].CollectionChanged += Col6_CollectionChanged;
            _window.ListAllCollections[6].CollectionChanged += Col7_CollectionChanged;
            _window.ListAllCollections[7].CollectionChanged += Col8_CollectionChanged;
            _window.ListAllCollections[8].CollectionChanged += Col9_CollectionChanged;
            _window.ListAllCollections[9].CollectionChanged += Col10_CollectionChanged;
            _window.ListAllCollections[10].CollectionChanged += Col11_CollectionChanged;
            _window.ListAllCollections[11].CollectionChanged += Col12_CollectionChanged;
            _window.ListAllCollections[12].CollectionChanged += Col13_CollectionChanged;
            _window.ListAllCollections[13].CollectionChanged += Col14_CollectionChanged;
            _window.ListAllCollections[14].CollectionChanged += Col15_CollectionChanged;
            _window.ListAllCollections[15].CollectionChanged += Col16_CollectionChanged;
            _window.ListAllCollections[16].CollectionChanged += Col17_CollectionChanged;
            _window.ListAllCollections[17].CollectionChanged += Col18_CollectionChanged;
            _window.ListAllCollections[18].CollectionChanged += Col19_CollectionChanged;
            _window.ListAllCollections[19].CollectionChanged += Col20_CollectionChanged;
            _window.ListAllCollections[20].CollectionChanged += Col21_CollectionChanged;
            _window.ListAllCollections[21].CollectionChanged += Col22_CollectionChanged;
            _window.ListAllCollections[22].CollectionChanged += Col23_CollectionChanged;
            _window.ListAllCollections[23].CollectionChanged += Col24_CollectionChanged;

            #endregion
           
        }

        #region Events of Collections

        private void Col1_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var collection = _window.ListAllCollections[0];

            if (collection.Count > 0)
            {
                TextBlock textBox;
                textBox = (_window.WhiteUser != null) ? _window.WhiteUser.counter1 : _window.BlackUser.counter1;
                if (collection.Count > 5)
                {
                    Brush counterColor;

                    //Different counter color for black and white users
                    if (collection.First().Color == _window.White)
                    {
                        counterColor = _window.Black;
                    }
                    else counterColor = _window.White;

                    if (_window.WhiteUser != null)
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                    else
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                }
                else
                {
                    if (_window.WhiteUser != null)
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                    else
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                }
            }
        }
        private void Col2_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var collection = _window.ListAllCollections[1];

            if (collection.Count > 0)
            {
                TextBlock textBox;
                textBox = (_window.WhiteUser != null) ? _window.WhiteUser.counter2 : _window.BlackUser.counter2;

                if (collection.Count > 5)
                {
                    Brush counterColor;

                    //Different counter color for black and white users
                    if (collection.First().Color == _window.White)
                    {
                        counterColor = _window.Black;
                    }
                    else counterColor = _window.White;

                    if (_window.WhiteUser != null)
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                    else
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                }
                else
                {
                    if (_window.WhiteUser != null)
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                    else
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                }
            }
        }
        private void Col3_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var collection = _window.ListAllCollections[2];

            if (collection.Count > 0)
            {
                TextBlock textBox;
                textBox = (_window.WhiteUser != null) ? _window.WhiteUser.counter3 : _window.BlackUser.counter3;
                if (collection.Count > 5)
                {
                    Brush counterColor;

                    //Different counter color for black and white users
                    if (collection.First().Color == _window.White)
                    {
                        counterColor = _window.Black;
                    }
                    else counterColor = _window.White;

                    if (_window.WhiteUser != null)
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                    else
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                }
                else
                {
                    if (_window.WhiteUser != null)
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                    else
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                }
            }
        }
        private void Col4_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var collection = _window.ListAllCollections[3];

            if (collection.Count > 0)
            {
                TextBlock textBox;
                textBox = (_window.WhiteUser != null) ? _window.WhiteUser.counter4 : _window.BlackUser.counter4;
                if (collection.Count > 5)
                {
                    Brush counterColor;

                    //Different counter color for black and white users
                    if (collection.First().Color == _window.White)
                    {
                        counterColor = _window.Black;
                    }
                    else counterColor = _window.White;

                    if (_window.WhiteUser != null)
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                    else
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                }
                else
                {
                    if (_window.WhiteUser != null)
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                    else
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                }
            }
        }
        private void Col5_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var collection = _window.ListAllCollections[4];

            if (collection.Count > 0)
            {
                TextBlock textBox;
                textBox = (_window.WhiteUser != null) ? _window.WhiteUser.counter5 : _window.BlackUser.counter5;
                if (collection.Count > 5)
                {
                    Brush counterColor;

                    //Different counter color for black and white users
                    if (collection.First().Color == _window.White)
                    {
                        counterColor = _window.Black;
                    }
                    else counterColor = _window.White;

                    if (_window.WhiteUser != null)
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                    else
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                }
                else
                {
                    if (_window.WhiteUser != null)
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                    else
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                }
            }
        }
        private void Col6_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            
            var collection = _window.ListAllCollections[5];

            if (collection.Count > 0)
            {
                TextBlock textBox;
                textBox = (_window.WhiteUser != null) ? _window.WhiteUser.counter6 : _window.BlackUser.counter6;
                if (collection.Count > 5)
                {
                    Brush counterColor;

                    //Different counter color for black and white users
                    if (collection.First().Color == _window.White)
                    {
                        counterColor = _window.Black;
                    }
                    else counterColor = _window.White;

                    if (_window.WhiteUser != null)
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                    else
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                }
                else
                {
                    if (_window.WhiteUser != null)
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                    else
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                }
            }
        }
        private void Col7_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
           
            var collection = _window.ListAllCollections[6];

            if (collection.Count > 0)
            {
                TextBlock textBox;
                textBox = (_window.WhiteUser != null) ? _window.WhiteUser.counter7 : _window.BlackUser.counter7;
                if (collection.Count > 5)
                {
                    Brush counterColor;

                    //Different counter color for black and white users
                    if (collection.First().Color == _window.White)
                    {
                        counterColor = _window.Black;
                    }
                    else counterColor = _window.White;

                    if (_window.WhiteUser != null)
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                    else
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                }
                else
                {
                    if (_window.WhiteUser != null)
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                    else
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                }
            }
        }
        private void Col8_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            var collection = _window.ListAllCollections[7];

            if (collection.Count > 0)
            {
                TextBlock textBox;
                textBox=(_window.WhiteUser != null) ?_window.WhiteUser.counter8: _window.BlackUser.counter8;

                if (collection.Count > 5)
                {
                    Brush counterColor;

                    //Different counter color for black and white users
                    if (collection.First().Color == _window.White)
                    {
                        counterColor = _window.Black;
                    }
                    else counterColor = _window.White;

                    if (_window.WhiteUser != null)
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                    else
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                }
                else
                {
                    if (_window.WhiteUser != null)
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                    else
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                }
            }
        }
        private void Col9_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            var collection = _window.ListAllCollections[8];

            if (collection.Count > 0)
            {
                TextBlock textBox;
                textBox = (_window.WhiteUser != null) ? _window.WhiteUser.counter9 : _window.BlackUser.counter9;
                if (collection.Count > 5)
                {
                    Brush counterColor;

                    //Different counter color for black and white users
                    if (collection.First().Color == _window.White)
                    {
                        counterColor = _window.Black;
                    }
                    else counterColor = _window.White;

                    if (_window.WhiteUser != null)
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                    else
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                }
                else
                {
                    if (_window.WhiteUser != null)
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                    else
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                }
            }
        }
        private void Col10_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var collection = _window.ListAllCollections[9];

            if (collection.Count > 0)
            {
                TextBlock textBox;
                textBox = (_window.WhiteUser != null) ? _window.WhiteUser.counter10 : _window.BlackUser.counter10;

                if (collection.Count > 5)
                {
                    Brush counterColor;

                    //Different counter color for black and white users
                    if (collection.First().Color == _window.White)
                    {
                        counterColor = _window.Black;
                    }
                    else counterColor = _window.White;

                    if (_window.WhiteUser != null)
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                    else
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                }
                else
                {
                    if (_window.WhiteUser != null)
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                    else
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                }
            }
        }
        private void Col11_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {          
            var collection = _window.ListAllCollections[10];

            if (collection.Count > 0)
            {
                TextBlock textBox;
                textBox = (_window.WhiteUser != null) ? _window.WhiteUser.counter11 : _window.BlackUser.counter11;
                if (collection.Count > 5)
                {
                    Brush counterColor;

                    //Different counter color for black and white users
                    if (collection.First().Color == _window.White)
                    {
                        counterColor = _window.Black;
                    }
                    else counterColor = _window.White;

                    if (_window.WhiteUser != null)
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                    else
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                }
                else
                {
                    if (_window.WhiteUser != null)
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                    else
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                }
            }
        }
        private void Col12_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var collection = _window.ListAllCollections[11];

            if (collection.Count > 0)
            {
                TextBlock textBox;
                textBox = (_window.WhiteUser != null) ? _window.WhiteUser.counter12 : _window.BlackUser.counter12;

                if (collection.Count > 5)
                {
                    Brush counterColor;

                    //Different counter color for black and white users
                    if (collection.First().Color == _window.White)
                    {
                        counterColor = _window.Black;
                    }
                    else counterColor = _window.White;

                    if (_window.WhiteUser != null)
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                    else
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                }
                else
                {
                    if (_window.WhiteUser != null)
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                    else
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                }
            }

        }
        private void Col13_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var collection = _window.ListAllCollections[12];
            TextBlock textBox;
            textBox = (_window.WhiteUser != null) ? _window.WhiteUser.counter13 : _window.BlackUser.counter13;

            if (collection.Count > 0)
            {
                if (collection.Count > 5)
                {
                    Brush counterColor;

                    //Different counter color for black and white users
                    if (collection.First().Color == _window.White)
                    {
                        counterColor = _window.Black;
                    }
                    else counterColor = _window.White;

                    if (_window.WhiteUser != null)
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                    else
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                }
                else
                {
                    if (_window.WhiteUser != null)
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                    else
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                }
            }
        }
        private void Col14_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var collection = _window.ListAllCollections[13];
            TextBlock textBox;
            textBox = (_window.WhiteUser != null) ? _window.WhiteUser.counter14 : _window.BlackUser.counter14;

            if (collection.Count > 0)
            {
                if (collection.Count > 5)
                {
                    Brush counterColor;

                    //Different counter color for black and white users
                    if (collection.First().Color == _window.White)
                    {
                        counterColor = _window.Black;
                    }
                    else counterColor = _window.White;

                    if (_window.WhiteUser != null)
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                    else
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                }
                else
                {
                    if (_window.WhiteUser != null)
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                    else
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                }
            }
        }
        private void Col15_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var collection = _window.ListAllCollections[14];
            TextBlock textBox;
            textBox = (_window.WhiteUser != null) ? _window.WhiteUser.counter15 : _window.BlackUser.counter15;

            if (collection.Count > 0)
            {
                if (collection.Count > 5)
                {
                    Brush counterColor;

                    //Different counter color for black and white users
                    if (collection.First().Color == _window.White)
                    {
                        counterColor = _window.Black;
                    }
                    else counterColor = _window.White;

                    if (_window.WhiteUser != null)
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                    else
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                }
                else
                {
                    if (_window.WhiteUser != null)
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                    else
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                }
            }
        }
        private void Col16_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var collection = _window.ListAllCollections[15];
            TextBlock textBox;
            textBox = (_window.WhiteUser != null) ? _window.WhiteUser.counter16 : _window.BlackUser.counter16;

            if (collection.Count > 0)
            {
                if (collection.Count > 5)
                {
                    Brush counterColor;

                    //Different counter color for black and white users
                    if (collection.First().Color == _window.White)
                    {
                        counterColor = _window.Black;
                    }
                    else counterColor = _window.White;

                    if (_window.WhiteUser != null)
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                    else
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                }
                else
                {
                    if (_window.WhiteUser != null)
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                    else
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                }
            }
        }
        private void Col17_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var collection = _window.ListAllCollections[16];
            TextBlock textBox;
            textBox = (_window.WhiteUser != null) ? _window.WhiteUser.counter17 : _window.BlackUser.counter17;

            if (collection.Count > 0)
            {
                if (collection.Count > 5)
                {
                    Brush counterColor;

                    //Different counter color for black and white users
                    if (collection.First().Color == _window.White)
                    {
                        counterColor = _window.Black;
                    }
                    else counterColor = _window.White;

                    if (_window.WhiteUser != null)
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                    else
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                }
                else
                {
                    if (_window.WhiteUser != null)
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                    else
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                }
            }
        }
        private void Col18_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var collection = _window.ListAllCollections[17];
            TextBlock textBox;
            textBox = (_window.WhiteUser != null) ? _window.WhiteUser.counter18 : _window.BlackUser.counter18;

            if (collection.Count > 0)
            {
                if (collection.Count > 5)
                {
                    Brush counterColor;

                    //Different counter color for black and white users
                    if (collection.First().Color == _window.White)
                    {
                        counterColor = _window.Black;
                    }
                    else counterColor = _window.White;

                    if (_window.WhiteUser != null)
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                    else
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                }
                else
                {
                    if (_window.WhiteUser != null)
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                    else
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                }
            }
        }
        private void Col19_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var collection = _window.ListAllCollections[18];
            TextBlock textBox;
            textBox = (_window.WhiteUser != null) ? _window.WhiteUser.counter19 : _window.BlackUser.counter19;

            if (collection.Count > 0)
            {
                if (collection.Count > 5)
                {
                    Brush counterColor;

                    //Different counter color for black and white users
                    if (collection.First().Color == _window.White)
                    {
                        counterColor = _window.Black;
                    }
                    else counterColor = _window.White;

                    if (_window.WhiteUser != null)
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                    else
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                }
                else
                {
                    if (_window.WhiteUser != null)
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                    else
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                }
            }
        }
        private void Col20_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var collection = _window.ListAllCollections[19];
            TextBlock textBox;
            textBox = (_window.WhiteUser != null) ? _window.WhiteUser.counter20 : _window.BlackUser.counter20;

            if (collection.Count > 0)
            {
                if (collection.Count > 5)
                {
                    Brush counterColor;

                    //Different counter color for black and white users
                    if (collection.First().Color == _window.White)
                    {
                        counterColor = _window.Black;
                    }
                    else counterColor = _window.White;

                    if (_window.WhiteUser != null)
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                    else
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                }
                else
                {
                    if (_window.WhiteUser != null)
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                    else
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                }
            }
        }
        private void Col21_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var collection = _window.ListAllCollections[20];
            TextBlock textBox;
            textBox = (_window.WhiteUser != null) ? _window.WhiteUser.counter21 : _window.BlackUser.counter21;

            if (collection.Count > 0)
            {
                if (collection.Count > 5)
                {
                    Brush counterColor;

                    //Different counter color for black and white users
                    if (collection.First().Color == _window.White)
                    {
                        counterColor = _window.Black;
                    }
                    else counterColor = _window.White;

                    if (_window.WhiteUser != null)
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                    else
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                }
                else
                {
                    if (_window.WhiteUser != null)
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                    else
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                }
            }
        }
        private void Col22_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var collection = _window.ListAllCollections[21];
            TextBlock textBox;
            textBox = (_window.WhiteUser != null) ? _window.WhiteUser.counter22 : _window.BlackUser.counter22;

            if (collection.Count > 0)
            {
                if (collection.Count > 5)
                {
                    Brush counterColor;

                    //Different counter color for black and white users
                    if (collection.First().Color == _window.White)
                    {
                        counterColor = _window.Black;
                    }
                    else counterColor = _window.White;

                    if (_window.WhiteUser != null)
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                    else
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                }
                else
                {
                    if (_window.WhiteUser != null)
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                    else
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                }
            }
        }
        private void Col23_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var collection = _window.ListAllCollections[22];
            TextBlock textBox;
            textBox = (_window.WhiteUser != null) ? _window.WhiteUser.counter23 : _window.BlackUser.counter23;

            if (collection.Count > 0)
            {
                if (collection.Count > 5)
                {
                    Brush counterColor;

                    //Different counter color for black and white users
                    if (collection.First().Color == _window.White)
                    {
                        counterColor = _window.Black;
                    }
                    else counterColor = _window.White;

                    if (_window.WhiteUser != null)
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                    else
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                }
                else
                {
                    if (_window.WhiteUser != null)
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                    else
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                }
            }
        }
        private void Col24_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var collection = _window.ListAllCollections[23];
            TextBlock textBox;
            textBox = (_window.WhiteUser != null) ? _window.WhiteUser.counter24 : _window.BlackUser.counter24;

            if (collection.Count > 0)
            {
                if (collection.Count > 5)
                {
                    Brush counterColor;

                    //Different counter color for black and white users
                    if (collection.First().Color == _window.White)
                    {
                        counterColor = _window.Black;
                    }
                    else counterColor = _window.White;

                    if (_window.WhiteUser != null)
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                    else
                    {
                        textBox.Text = collection.Count.ToString();
                        textBox.Foreground = counterColor;
                        Panel.SetZIndex(textBox, 100);
                    }
                }
                else
                {
                    if (_window.WhiteUser != null)
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                    else
                    {
                        Panel.SetZIndex(textBox, -100);
                        textBox.Text = string.Empty;
                    }
                }
            }
        }

        #endregion

        private void SetColection()
        {
            //Top left
            _window.ListAllCollections = new List<ObservableCollection<Checker>>();

            for (int i = 0; i < 24; i++)
            {
                _window.ListAllCollections.Add(new ObservableCollection<Checker>());
            }
            //Bars
            _window.WhiteBar = new ObservableCollection<Checker>();
            _window.BlackBar = new ObservableCollection<Checker>();

            _window.ListBarCollections = new List<ObservableCollection<Checker>>
            {
                _window.WhiteBar ,
                _window.BlackBar
            };
        }

        private void SetTopMenu()
        {
            _window.about.Text = TopMenu.GetAbout();
            _window.howToPlay.Text = TopMenu.GetHelpForGame();
        }

        private void SetColors()
        {

            _window.Selected = new SolidColorBrush(Colors.Brown);
            _window.Black =    new SolidColorBrush(Colors.Black);
            _window.White =    new SolidColorBrush(Colors.Moccasin);

            if (_window.Master)
            {
                _window.MyCheckerColor = _window.White;
                _window.your_color_example.Background = _window.White;
                _window.your_color_example.Foreground = _window.Black;

            }

            else
            {
                _window.MyCheckerColor = _window.Black;
                _window.your_color_example.Background = _window.Black;
                _window.your_color_example.Foreground = _window.White;
            }

        }

        private void SetStartInfo()
        {
            if (_window.Master)
            {
                _window.GameInfo = new GameInfo { Info = "You start first. Roll dice. " };
            }
            else
            {

                _window.GameInfo = new GameInfo { Info = "Your opponent starts first. Wait for his move" };
            }
        }
    }
}
