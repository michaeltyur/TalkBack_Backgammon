using Client.Game.GameLogic;
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
    /// Сreates an Dice object for Backgammon
    /// </summary>
    public class Dice : INotifyPropertyChanged
    {
        public Dice()
        {
            _image = "../Images/dice1.png";
            _number = 1;
        }
        private string _image { get; set; }
        public string Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }

        private int _number { get; set; }
        public int Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
                OnPropertyChanged();
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static string GetDiceImage(int diceNumber)
        {
            return $"../Images/dice{diceNumber}.png";
        }
    }
}
