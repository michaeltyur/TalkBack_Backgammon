using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLServer.Game
{
    public class DiceLogic
    {
        private Random rand;

        public DiceLogic(Random rand)
        {
            this.rand = rand;
        }

        public List<int> GetRandomDices()
        {
            List<int> numbers = new List<int>();

            int number = rand.Next(1,6);
            numbers.Add(number);

            number = rand.Next(1, 6);

            numbers.Add(number);

            return numbers;
            //Thread.Sleep(250);
        }
    }
}
