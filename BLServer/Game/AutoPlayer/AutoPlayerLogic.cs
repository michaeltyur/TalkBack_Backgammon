using Common.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace BLServer.Game.AutoPlayer
{
    public class AutoPlayerLogic
    {
        static readonly Semaphore sem = new Semaphore(1, 1);

        public Brush MyCheckerColor { get; set; }
        public string Opponent { get; }
        private GameTable gameTable;
        private GameLogic gameLogic;
        private readonly DiceLogic diceLogic;        
        private Random rand;
        private ObservableCollection<Checker> BarColl { get; set; }
        private DispatcherTimer Timer;
        private bool firstMove;

        public AutoPlayerLogic(GameTable gameTable, GameLogic gameLogic, DiceLogic diceLogic, bool white)
        {
            firstMove = true;
            rand = new Random();
            this.gameTable = gameTable;

            Timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2)
            };
            //Timer.Tick += ComputerTurn;

            if (white)
            {
                MyCheckerColor = gameTable.White;
                firstMove = true;
            }
            else
            {
                MyCheckerColor = gameTable.Black;
                firstMove = false;
            }

            this.gameLogic = gameLogic;
            this.diceLogic = diceLogic;

            if (white) BarColl = gameTable.WhiteBar;
            else BarColl = gameTable.BlackBar;
        }

        /// <summary>
        ///try to move the checkers
        /// </summary>
        /// <param name="number">the number of dice</param>
        /// <returns>returns result</returns>
        public bool ComputerTurn(int number)
        {
            if (firstMove)
            {
                firstMove = false;
            }
            sem.WaitOne();
            bool result = false;
            
            var myCheckersPositionIndexes = GetmyCheckersPositionIndexes();

            try
            {
                if (BarColl.Count > 0)
                {
                    try
                    {
                        result = TryMovingFromBar(number);
                    }
                    catch (Exception)
                    {
                        result = false;
                    }

                }
                if (!result)
                {
                    try
                    {
                        result = TryStandartMoving(number);
                    }
                    catch (Exception)
                    {
                        result = false;
                    }
                }
                sem.Release();
                return result;
            }
            catch (Exception)
            {
                sem.Release();
                return false;
            }
        }

        /// <summary>
        /// Gets checkres position of current player
        /// </summary>
        /// <returns></returns>
        private List<int> GetmyCheckersPositionIndexes()
        {
            int[] myTable = GetMyCheckersTable();

            var result = new List<int>();

            for (int i = 0; i < myTable.Length; i++)
            {
                if (myTable[i] > 0) result.Add(i);
            }
            return result;
        }

        /// <summary>
        ///try to move the checkers on the board
        /// </summary>
        /// <param name="diceNumber">the number of dice</param>
        /// <returns>returns result</returns>
        private bool TryStandartMoving( int diceNumber)
        {
            bool result = false;
            var myCheckersPositionIndexes = GetmyCheckersPositionIndexes();

            while (!result && myCheckersPositionIndexes.Count > 0)
            {

                var randomCurrentIndex = rand.Next(myCheckersPositionIndexes.Count);

                int index = myCheckersPositionIndexes[randomCurrentIndex];
                myCheckersPositionIndexes.Remove(index);

                try
                {
                    result = gameLogic.CheckersMoovingLogic(index, diceNumber);
                    //if (result!=null)Thread.Sleep(TimeSpan.FromSeconds(0.5));                                           
                    return result;
                    
                }
                catch (Exception)
                {
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        ///try to move the checkers from the bar
        /// </summary>
        /// <param name="diceNumber">the number of dice</param>
        /// <returns>returns result</returns>
        private bool TryMovingFromBar(int diceNumber)
        {
            var result = false;
            try
            {
                var index = 0;
                if (MyCheckerColor == gameTable.White) index = 0;
                else index = 1;
                result = gameLogic.CheckersMoovingFromBarLogic(index, diceNumber);
               // if (result!=null) Thread.Sleep(TimeSpan.FromSeconds(0.5));
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the array with user's checkers position
        /// </summary>
        /// <returns></returns>
        private int[] GetMyCheckersTable()
        {
            var tables = gameTable.GetGameTableForSend();

            int[] myTable;
            int[] opponentTable;

            if (MyCheckerColor == gameTable.White)
            {
                myTable = tables[0];
                opponentTable = tables[1];

            }
            else
            {

                myTable = tables[1];
                opponentTable = tables[0];
            }
            return myTable;
        }
    }
}
