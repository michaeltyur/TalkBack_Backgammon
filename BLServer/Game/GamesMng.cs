using BLServer.Game.AutoPlayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace BLServer.Game
{
    public class GamesMng
    {
        private static readonly object padlock = new object();
        private IDictionary HowFirst;
        private IDictionary Games;

        private Random _rand;

        public GamesMng()
        {
            HowFirst = new Dictionary<Tuple<string, string>, HowFirstLogic>();
            Games = new Dictionary<Tuple<string, string>, Game>();
            _rand = new Random();
        }



        /// <summary>
        /// Adds new game instance
        /// </summary>
        /// <param name="whiteGamer">name of gamer that games with white checkers</param>
        /// <param name="blackGamer">name of gamer that games with black checkers</param>
        public void AddGame(string whiteUser, string blackUser)
        {
            var keys = new Tuple<string, string>(whiteUser, blackUser);
            var keys2 = new Tuple<string, string>(blackUser, whiteUser);
            lock (padlock)
            {
                if (!Games.Contains(keys) && !Games.Contains(keys2))
                {

                    Games.Add(keys, new Game(whiteUser, blackUser, _rand));
                }
            }
        }


        /// <summary>
        /// Returns the game instance according names of gamers
        /// </summary>
        /// <param name="whiteGamer">name of gamer</param>
        /// <param name="blackGamer">name of gamer</param>
        /// <returns></returns>
        public Game GetGame(string firstUser, string secondUser)
        {
            var keys = new Tuple<string, string>(firstUser, secondUser);
            var keys2 = new Tuple<string, string>(secondUser, firstUser);

            if (Games.Contains(keys))
            {
                return (Game)Games[keys];
            }
            else if (Games.Contains(keys2))
            {
                return (Game)Games[keys2];
            }
            else return null;
        }

        /// <summary>
        /// Delete the game instance according names of gamers
        /// </summary>
        /// <param name="whiteGamer">name of first gamer</param>
        /// <param name="blackGamer">name of second gamer</param>
        /// 
        public void DeleteGame(string firstUser, string secondUser)
        {
            var keys = new Tuple<string, string>(firstUser, secondUser);
            var keys2 = new Tuple<string, string>(secondUser, firstUser);
            try
            {
                lock (padlock)
                {
                    if (Games.Contains(keys))
                    {
                        Games.Remove(keys);
                    }
                    else if (Games.Contains(keys2))
                    {
                        Games.Remove(keys2);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Moves checkers if this is posible end returns array represents checkers positions
        /// </summary>
        /// <param name="whiteGamer">gamer that games with white checkers</param>
        /// <param name="blackGamer">gamer that games with black checkers</param>
        /// <param name="selectedIndexColl">index of collection that need to moving</param>
        /// <param name="futureIndexColl">index of destination collection</param>
        /// <param name="white">color of checkers</param>
        /// <returns>true in case that is done successfully</returns>
        public bool CheckersMoovingLogic(string firstUser, string secondUser, int selectedIndexColl, int futuredIndexColl)
        {
            if (!string.IsNullOrEmpty(firstUser) && !string.IsNullOrEmpty(secondUser))
            {
                var game = GetGame(firstUser, secondUser);
                if (game != null)
                {
                    try
                    {
                        return game.GameLogic.CheckersMoovingLogic(selectedIndexColl, futuredIndexColl);
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                }
                else return false;
            }
            else return false;
        }

        /// <summary>
        /// Moves checkers if this is posible end returns array represents checkers positions
        /// </summary>
        /// <param name="firstUser">name of user</param>
        /// <param name="secondUser">name of user</param>
        /// <param name="number">number of dice</param>
        /// <returns>true in case that is done successfully</returns>
        public bool CheckersMoovingLogicAuto(string firstUser, string secondUser, int number)
        {
            if (!string.IsNullOrEmpty(firstUser) && !string.IsNullOrEmpty(secondUser))
            {
                var game = GetGame(firstUser, secondUser);

                if (game != null)
                {
                    try
                    {
                        return game.AutoPlayer.ComputerTurn(number);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                else return false;

            }
            else return false;
        }

        /// <summary>
        /// Moves checkers from bar if this is posible end returns array represents checkers positions
        /// </summary>
        /// <param name="whiteGamer">gamer that games with white checkers</param>
        /// <param name="blackGamer">gamer that games with black checkers</param>
        /// <param name="selectedIndexColl">index of collection that need to moving</param>
        /// <param name="futureIndexColl">index of destination collection</param>
        /// <param name="white">color of checkers</param>
        /// <returns>true in case that is done successfully</returns>
        public bool CheckersMoovingFromBarLogic(string firstUser, string secondUser, int selectedIndexColl, int futuredIndexColl)
        {
            if (!string.IsNullOrEmpty(firstUser) && !string.IsNullOrEmpty(secondUser))
            {
                var game = GetGame(firstUser, secondUser);
                if (game != null)
                {
                    try
                    {
                        return game.GameLogic.CheckersMoovingFromBarLogic(selectedIndexColl, futuredIndexColl);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
                else return false;
            }
            else return false;
        }

        /// <summary>
        /// returns the array represents chechers position
        /// </summary>
        /// <param name="firstUser"></param>
        /// <param name="secondUser"></param>
        /// <returns>array with checkers positions</returns>
        public int[][] GetGameTableForSend(string firstUser, string secondUser)
        {
            var game = GetGame(firstUser, secondUser);
            if (game != null)
            {
                return game.GameTable.GetGameTableForSend();
            }
            else return null;
        }

        #region How To First
        //How to first
        /// <summary>
        /// adds instance to dictionary
        /// </summary>
        /// <param name="firstUser"></param>
        /// <param name="secondUser"></param>
        public void AddHowToFirst(string firstUser, string secondUser)
        {
            var keys = new Tuple<string, string>(firstUser, secondUser);
            var keys2 = new Tuple<string, string>(secondUser, firstUser);

            if (!HowFirst.Contains(keys) && !HowFirst.Contains(keys2))
            {
                HowFirst.Add(keys, new HowFirstLogic(firstUser, secondUser, _rand));
            }
        }

        /// <summary>
        /// Removes instance from dictionary
        /// </summary>
        /// <param name="firstUser"></param>
        /// <param name="secondUser"></param>
        public void DeleteHowToFirst(string firstUser, string secondUser)
        {
            var keys = new Tuple<string, string>(firstUser, secondUser);
            var keys2 = new Tuple<string, string>(secondUser, firstUser);
            try
            {
                if (HowFirst.Contains(keys))
                {
                    var instance = HowFirst[keys];
                    HowFirst.Remove(keys);
                    //instance = null;
                }
                else if (HowFirst.Contains(keys2))
                {
                    var instance = HowFirst[keys2];
                    HowFirst.Remove(keys2);
                    //instance = null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string[] RollHowFirst(string firstUser, string secondUser)
        {
            var keys = new Tuple<string, string>(firstUser, secondUser);
            var keys2 = new Tuple<string, string>(secondUser, firstUser);

            HowFirstLogic howToFirst;

            if (HowFirst.Contains(keys))
            {
                howToFirst = (HowFirstLogic)HowFirst[keys];
            }
            else if (HowFirst.Contains(keys2))
            {
                howToFirst = (HowFirstLogic)HowFirst[keys2];
            }
            else return null;
            try
            {
                var result = howToFirst.Roll(firstUser);
                if (result != null)
                {
                    return result;
                }
                else return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// returns true if one of users puted out all his chechers 
        /// </summary>
        /// <param name="firstUser">name of user</param>
        /// <param name="secondUser">name of user</param>
        /// <returns>true in case that game is over</returns>
        public bool IsGameOver(string firstUser, string secondUser)
        {
            var game = GetGame(firstUser, secondUser);

            if (game != null)
            {
                if (game.GameTable != null)
                {
                    if (game.GameTable.WhiteCounter == 0 || game.GameTable.BlackCounter == 0)
                        return true;
                    else return false;
                }
                return false;
            }
            else return false;
        }

    }

    /// <summary>
    /// Unions two keys to one
    /// </summary>
    /// <typeparam name="T1">name of user</typeparam>
    /// <typeparam name="T2">name of user</typeparam>
    public struct Tuple<T1, T2>
    {
        public readonly T1 Name1;
        public readonly T2 Name2;

        public Tuple(T1 name1, T2 name2)
        {
            Name1 = name1;
            Name2 = name2;
        }
    }

    public enum Player
    {
        singlePlayer = 1,
        autoPlayer
    }

}
