using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Client.ChatServer;

namespace Client.Windows
{
    
    public class BaseWindow : Window
    {
        public ChatServiceClient Host { get; }
        protected Window _window;
        protected Grid _myGrid;
        protected StackPanel _mainTitle;
        protected StackPanel _topPanel;
        protected Button _menuButton;
        protected Button _closeButton;
        public string _currentUserName;
        protected DispatcherTimer Timer;

        //Constractor
        public BaseWindow(int width,int height)
        {
            var callback = new ClientCallback();
            Host = new ChatServiceClient(new System.ServiceModel.InstanceContext(callback));
            _window = new Window
            {
                Width = width,
                Height=height,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.NoResize,
                WindowStyle = WindowStyle.None
            };
            _window.MouseLeftButtonDown += _window_MouseLeftButtonDown;
            //Grid
            _myGrid = new Grid();
            _window.Content = _myGrid;
            _myGrid.Background = new SolidColorBrush(Colors.BlueViolet);
            //_myGrid.ShowGridLines = true;

            RowDefinition rowDef = new RowDefinition();
            _myGrid.RowDefinitions.Add(rowDef);
            _myGrid.RowDefinitions[0].Height = new GridLength(30);

            Timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(15)
            };
            Timer.Tick += Timer_Tick;
        }

        #region Initialization
        protected void SetGridRowsCols(int row, int col)
        {
            // Define the Rows
            if (row != 0)
            {
                for (int i = 0; i < row; i++)
                {
                    RowDefinition rowDef = new RowDefinition();
                    _myGrid.RowDefinitions.Add(rowDef);
                }

            }
            if (col != 0)
            {
                for (int i = 0; i < col; i++)
                {
                    ColumnDefinition columnDef = new ColumnDefinition();
                    _myGrid.ColumnDefinitions.Add(columnDef);
                }

            }
        }

        //Create Top Menu
        protected void SetTopMenu(HelpString helpString,string header)
        {
            _topPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            _myGrid.Children.Add(_topPanel);
            Grid.SetRow(_topPanel, 0);
            Grid.SetColumn(_topPanel, 0);
            _menuButton = new Button
            {
                Width = 60,
                ToolTip = "Short Menu",
                BorderBrush = new SolidColorBrush(Colors.Black)
            };
            _menuButton.ContextMenu = TopMenu.GetTopMenu(_currentUserName,header,helpString);

            var brush = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("../../Images/top_menu.jpg", UriKind.Relative))
            };
            _menuButton.Background = brush;
            _menuButton.Click += MenuButton_Click;

            TextBlock headerTitle = new TextBlock
            {
                Text = "Talk Back",
                Width = _window.Width-120,
                FontSize = 15,
                Background = new SolidColorBrush(Colors.DarkViolet),
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Height = 30,
            };

            Button minButton = new Button
            {
                Width = 30,
                Background = new SolidColorBrush(Colors.MediumVioletRed),
                Content = "___",
                BorderBrush = new SolidColorBrush(Colors.Black),
                BorderThickness=new Thickness(1)
            };
            minButton.Click += MinButton_Click;

            _closeButton = new Button
            {
                Width = 30,
                Background = new SolidColorBrush(Colors.Brown),
                Content = "X",
                BorderBrush = new SolidColorBrush(Colors.Black)
            };
            _closeButton.Click += CloseButton_Click;

            _topPanel.Children.Add(_menuButton);
            _topPanel.Children.Add(headerTitle);
            _topPanel.Children.Add(minButton);
            _topPanel.Children.Add(_closeButton);

        }

        public void SetHeaderTitle(string title)
        {
            ((TextBlock)_topPanel.Children[1]).Text = title;
        }

        protected void SetTitle(string topRow, string middleRow, string bottomRow)
        {
            _mainTitle = new StackPanel
            {
                Orientation = Orientation.Vertical,
            };
            _myGrid.Children.Add(_mainTitle);
            Grid.SetRow(_mainTitle, 1);

            TextBlock textBlock1 = new TextBlock
            {
                Text = topRow,
                FontSize = 15,
                FontStyle = FontStyles.Italic,
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Padding = new Thickness(0, 2, 0, 0)
            };
            TextBlock textBlock2 = new TextBlock
            {
                Text = middleRow,
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                FontStyle = FontStyles.Italic,
                FontFamily = new FontFamily("Comic Sans MS, Verdana"),
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            TextBlock textBlock3 = new TextBlock
            {
                Text = bottomRow,
                FontSize = 15,
                FontStyle = FontStyles.Italic,
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Padding = new Thickness(0, 2, 0, 0)

            };
            _mainTitle.Children.Add(textBlock1);
            _mainTitle.Children.Add(textBlock2);
            _mainTitle.Children.Add(textBlock3);
        }

        protected void SetTitleText(string topRow, string middleRow, string bottomRow)
        {
            var _topRow = (TextBlock)_mainTitle.Children[0];
            var _middleRow = (TextBlock)_mainTitle.Children[1];
            var _bottomRow = (TextBlock)_mainTitle.Children[2];
            _topRow.Text = topRow;
            _middleRow.Text = middleRow;
            _bottomRow.Text = bottomRow;
        }
        #endregion

        //Open Windows
        public void OpenWindow()
        {
            _window.Show();
        }

        #region Events
        //Drag Window
        private void _window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _window.DragMove();
        }

        //Open Top Menu
        private void MenuContainer_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var textBlock = (TextBlock)sender;
            var menu = textBlock.ContextMenu;
            if (_currentUserName != null && _currentUserName != string.Empty)
            {
                menu.IsOpen = true;
            }
        }
        private void MenuContainer_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var textBlock=(TextBlock)sender;
            var menu = textBlock.ContextMenu;
            if (_currentUserName!=null&&_currentUserName!=string.Empty)
            {
                menu.IsOpen = true;
            }
            ((Button)_topPanel.Children[0]).ContextMenu.IsOpen = true;
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {          
            ((Button)sender).ContextMenu.IsOpen = true;
        }

        //Minimiz Window
        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            _window.WindowState = WindowState.Minimized;
        }

        //Custom X button and log out
        public virtual void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Host.LogoutAsync(_currentUserName);
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(ex.Message, "Error!");
            }
        }

        public virtual void Timer_Tick(object sender, EventArgs e) { }
        #endregion
    }
}
