using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Client.Game.GameData
{
    /// <summary>
    /// Сreates an information object for top bar of Backgammon
    /// </summary>
    public class BarGameInfo: INotifyPropertyChanged
    {
        private int _outCounter;
        public int OutCounter
        {
            get
            {
                return _outCounter;
            }
            set
            {
                _outCounter = value;
                OnPropertyChanged();
            }
        }
        private int _topCounter;
        public int TopCounter
        {
            get
            {
                return _topCounter;
            }
            set
            {
                _topCounter = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
