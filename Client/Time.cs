using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
   public class Time : INotifyPropertyChanged
    {
        public Time()
        {
            _hour= DateTime.Now.TimeOfDay.Hours.ToString();
            _minutes= DateTime.Now.TimeOfDay.Minutes.ToString();

        }
        private string _hour { get; set; }
        public string Hour
        {
            get
            {
                return _hour;
            }
            set
            {
                _hour = value;
                OnPropertyChanged();
            }
        }

        private string _minutes { get; set; }
        public string Minutes
        {
            get
            {
                return _minutes;
            }
            set
            {
                _minutes = value;
                OnPropertyChanged();
            }
        }
        private string _seconds { get; set; }
        public string Seconds
        {
            get
            {
                return _seconds;
            }
            set
            {
                _seconds = value;
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
