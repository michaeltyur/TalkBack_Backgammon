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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.Game.UserControls
{
    /// <summary>
    /// Interaction logic for BlackUser.xaml
    /// </summary>
    public partial class BlackUser : UserControl
    {
        Backgammon _backgammon;

        private СheckerLogic _checkerLogic;

        public BlackUser(Backgammon backgammon)
        {
            InitializeComponent();
            _backgammon = backgammon;
            _checkerLogic = new СheckerLogic(backgammon);
        }

        #region Checkers Events
        private void Sp1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _checkerLogic.CollSelected(_backgammon.ListAllCollections[0]);
        }

        private void Sp2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _checkerLogic.CollSelected(_backgammon.ListAllCollections[1]);
        }

        private void Sp3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _checkerLogic.CollSelected(_backgammon.ListAllCollections[2]);
        }

        private void Sp4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _checkerLogic.CollSelected(_backgammon.ListAllCollections[3]);
        }

        private void Sp5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _checkerLogic.CollSelected(_backgammon.ListAllCollections[4]);
        }

        private void Sp6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _checkerLogic.CollSelected(_backgammon.ListAllCollections[5]);
        }

        private void Sp7_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _checkerLogic.CollSelected(_backgammon.ListAllCollections[6]);
        }

        private void Sp8_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _checkerLogic.CollSelected(_backgammon.ListAllCollections[7]);
        }

        private void Sp9_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _checkerLogic.CollSelected(_backgammon.ListAllCollections[8]);
        }

        private void Sp10_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _checkerLogic.CollSelected(_backgammon.ListAllCollections[9]);
        }

        private void Sp11_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _checkerLogic.CollSelected(_backgammon.ListAllCollections[10]);
        }

        private void Sp12_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _checkerLogic.CollSelected(_backgammon.ListAllCollections[11]);
        }

        private void Sp13_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _checkerLogic.CollSelected(_backgammon.ListAllCollections[12]);
        }

        private void Sp14_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _checkerLogic.CollSelected(_backgammon.ListAllCollections[13]);
        }

        private void Sp15_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _checkerLogic.CollSelected(_backgammon.ListAllCollections[14]);
        }

        private void Sp16_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _checkerLogic.CollSelected(_backgammon.ListAllCollections[15]);
        }

        private void Sp17_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _checkerLogic.CollSelected(_backgammon.ListAllCollections[16]);
        }

        private void Sp18_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _checkerLogic.CollSelected(_backgammon.ListAllCollections[17]);
        }

        private void Sp19_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _checkerLogic.CollSelected(_backgammon.ListAllCollections[18]);
        }

        private void Sp20_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _checkerLogic.CollSelected(_backgammon.ListAllCollections[19]);
        }

        private void Sp21_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _checkerLogic.CollSelected(_backgammon.ListAllCollections[20]);
        }

        private void Sp22_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _checkerLogic.CollSelected(_backgammon.ListAllCollections[21]);
        }

        private void Sp23_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _checkerLogic.CollSelected(_backgammon.ListAllCollections[22]);
        }

        private void Sp24_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _checkerLogic.CollSelected(_backgammon.ListAllCollections[23]);
        }
        #endregion

        //Black Bar Event
        private void BlackBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _checkerLogic.CollSelected(_backgammon.BlackBar);
        }
    }
}
