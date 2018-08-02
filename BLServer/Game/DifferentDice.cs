using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLServer.Game
{
   public class DifferentDice
    {
        private readonly string _firstPlayer;
        private readonly string _secondPlayer;

        private int _movingCounter { get; set; }

        public DifferentDice(string firstPlayer, string secondPlayer)
        {
            _firstPlayer = firstPlayer;
            _secondPlayer = secondPlayer;
        }
        public bool IsMoovingFinish()
        {
            if (_movingCounter >= 1) return true;
            else
            {
                _movingCounter++;
                return false;
            }
        }
    }
}
