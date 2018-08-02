using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Client.Game.GameOver
{
    public class GameOverImages : INotifyPropertyChanged
    {     
        private bool _winner;
        private int count = 0;

        public string[] WinnerImages;
        public string[] LoserImages;

        public GameOverImages(bool winner)
        {
            _winner = winner;
            WinnerImages = new string[3];
            LoserImages = new string[3];
            FillArrays();
            if (_winner)
                Image = WinnerImages[0];
            else Image = LoserImages[0];
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void FillArrays()
        {
            for (int i = 0; i < 3; i++)
            {
                WinnerImages[i] = $"../../Images/GameOver/winner{i + 1}.jpg";
                LoserImages[i] = $"../../Images/GameOver/loser{i + 1}.jpg";
            }
        }
        public string ChangeImage()
        {
            if (count < 2) count++;
            else count = 0;

           
                if (_winner)
                    return WinnerImages[count];
                else return  LoserImages[count];
            

        }
    }
}
