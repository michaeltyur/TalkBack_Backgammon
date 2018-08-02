using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLServer.Game
{
    public class HowFirstLogic
    {
        private static readonly object padlock = new object();

        private string _leftUser;
        private string _rightUser;
        private int _leftNumber;
        private int _rightNumber;
        private Random _rand;

        public HowFirstLogic(string user, string opponent, Random rand)
        {
            _leftUser = user;
            _rightUser = opponent;
            _rand = rand;
        }

        public string[] Roll(string name)
        {
            lock (padlock)
            {
               
                    if (name == _leftUser)
                    {
                        while (_leftNumber == _rightNumber || _leftNumber==0)
                            _leftNumber = _rand.Next(1, 7);

                    }
                    else if (name == _rightUser)
                    {
                        while (_leftNumber == _rightNumber || _rightNumber==0)
                            _rightNumber = _rand.Next(1, 7);
                    }
                
                string[] array = new string[3];

                array[0] = _leftNumber.ToString();
                array[1] = _rightNumber.ToString();
                array[2] = string.Empty;

                if (_leftNumber != 0 && _rightNumber != 0 && _leftNumber != _rightNumber)
                {
                    if (_leftNumber > _rightNumber)
                    {
                        array[2] = _leftUser;
                    }
                    else array[2] = _rightUser;
                }
                if (_leftNumber == _rightNumber)
                {
                    _leftNumber = 0;
                    _rightNumber = 0;
                }

                return array;
            }

        }

    }
}
