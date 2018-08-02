using Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Common.Data;
using DAL;
using System.Collections.ObjectModel;
using BLServer.Game;
using System.Threading;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace BLServer
{
    /// <summary>
    /// Contains all logic methods for performing connecting users and DB.
    /// </summary>
    public sealed class ServerLogic
    {
        private static ServerLogic _instance = null;

        private static readonly object padlock = new object();

        private Dal _dal;

        private readonly Random _rand;

        private Dictionary<string, IClientCallback> _callback;

        public GamesMng GameMng { get; }

        private ServerLogic()
        {
            _dal = new Dal();
            _callback = new Dictionary<string, IClientCallback>();
            GameMng = new GamesMng();
            _rand = new Random();
        }

        public static ServerLogic Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (padlock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ServerLogic();
                        }
                    }
                }
                return _instance;
            }

        }

        #region Authentication
        //Registration
        /// <summary>
        /// Add the new user to db
        /// </summary>
        /// <param name="username">user name</param>
        /// <param name="password">user password</param>
        /// <param name="firstname">first name of user</param>
        /// <param name="lastname">last name of user</param>
        public void Register(string username, string password, string firstname, string lastname)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(firstname) && !string.IsNullOrEmpty(lastname))
            {
                var callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();

                User user = _dal.GetUser(username, password);

                if (user != null)
                {
                    callback.RecieveError("User is exist");
                    return;
                }
                else user = new User
                { UserName = username, Password = password, FirstName = firstname, LastName = lastname };

                try
                {
                    _dal.AddUser(user);
                    Login(username, password);
                }
                catch (Exception)
                {
                    callback.RecieveError("Error Try Again");
                }
            }
        }

        /// <summary>
        ///User authentication
        /// </summary>
        /// <param name="username">user name</param>
        /// <param name="password">user password</param>
        public void Login(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();

                if (_callback.ContainsKey(username))
                {
                    callback.RecieveError($"User {username} is on line");
                    return;
                }
                try
                {
                    User user = _dal.GetUser(username, password);
                    if (user == null)
                    {
                        callback.RecieveError("User is not exist");
                    }
                    else
                    {
                        lock (padlock)
                        {
                            callback.Login(username);
                            _callback.Add(username, callback);
                            foreach (var call in _callback)
                            {
                                call.Value.UsersOnLine(GetOnLineUsers(), GetOffLineUsers());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
        }

        public void Logout(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {

                var callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
                if (!string.IsNullOrEmpty(username))
                {
                    if (_callback.ContainsKey(username))
                    {
                        try
                        {
                            lock (padlock)
                            {
                                _callback.Remove(username);
                            }
                            callback.Logout();
                            foreach (var call in _callback)
                            {
                                call.Value.UsersOnLine(GetOnLineUsers(), GetOffLineUsers());
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error during logout", ex);
                        }
                    }
                }
            }


        }
        #endregion

        #region Users

        /// <summary>
        /// Gets List On Line User's Names
        /// </summary>
        /// <returns></returns>
        private List<string> GetOnLineUsers()
        {
            return _callback.Keys.ToList();
        }

        /// <summary>
        /// Get List Off Line User's Names
        /// </summary>
        /// <returns></returns>
        public List<string> GetOffLineUsers()
        {

            List<string> usersOnLine = _callback.Keys.ToList();

            List<User> allUser = _dal.GetAllUsers();

            List<string> usersOffLine = new List<string>();

            foreach (var userOnLine in usersOnLine)
            {
                allUser.Remove(allUser.First(u => u.UserName == userOnLine));

            }
            foreach (var userDb in allUser)
            {
                usersOffLine.Add(userDb.UserName);
            }
            return usersOffLine;
        }
        #endregion

        #region Message

        /// <summary>
        /// Send CallBack Message to All Users
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="content"></param>
        public void SendMessage(string userName, string content)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(content))
            {
                var callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();

                foreach (var call in _callback)
                {
                    call.Value.RecieveMessage(userName, content);
                }
                // var user = _dal.GetUserByName(userName);
                Message message = new Message { UserName = userName, Content = content, Date = DateTime.Now };
                try
                {
                    _dal.AddMessage(message);
                }
                catch (Exception ex)
                {
                    callback.RecieveError(ex.Message);
                }

            }
        }

        public void SendErrorMessage(string userName, string content)
        {

            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(content))
            {
                if (_callback.ContainsKey(userName))
                {
                    _callback[userName].RecieveError(content);
                }
            }
        }
        #endregion

        #region Private Chat
        //Start Private Chat 
        public void PrivateChatStart(string userName, string opponentName)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(opponentName))
            {
                var callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
                if (_callback.ContainsKey(opponentName))
                {
                    try
                    {
                        _callback[opponentName].PrivateChatStart(opponentName, userName);
                    }
                    catch (Exception ex)
                    {
                        callback.RecieveError(ex.Message);
                    }
                }
                else callback.RecieveError("The User is off line");
            }
        }

        public void ConfirmationPrivateChat(string userName, string opponentName)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(opponentName))
            {
                if (_callback.ContainsKey(opponentName))
                {
                    _callback[opponentName].PrivateChatStartFromOpponent(opponentName, userName);
                }
            }
        }

        //Send Private Message
        public void SendPrivateMsg(string from, string to, string content)
        {
            if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(to) && !string.IsNullOrEmpty(content))
            {
                if (_callback.ContainsKey(to) && _callback.ContainsKey(from))
                {
                    var test = to;
                    _callback[from].RecievePrivateMsg(from, to, content);
                    _callback[to].RecievePrivateMsg(from, from, content);
                }
            }
        }

        public void ExitPrivateChat(string userName, string opponentName)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(opponentName))
            {
                if (_callback.ContainsKey(opponentName))
                {
                    _callback[opponentName].PrivateChatSlose(userName);
                }
            }
        }
        #endregion

        #region Game
        /// <summary>
        /// Send request to user about game
        /// </summary>
        /// <param name="userName">user name that send request</param>
        /// <param name="opponentName">user name that recieve request</param>
        public void RequestGame(string userName, string opponentName)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(opponentName))
            {
                var callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
                try
                {
                    if (_callback.ContainsKey(opponentName))
                    {
                        _callback[opponentName].RequestGameFromServer(opponentName, userName);
                    }
                }
                catch (Exception ex)
                {
                    callback.RecieveError(ex.Message);
                }
            }
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        /// <param name="userName">user name</param>
        /// <param name="opponentName">opponent name</param>
        /// <param name="master">how move first</param>
        public void StartGame(string userName, string opponentName, bool master)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(opponentName))
            {
                var callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
                try
                {
                    lock (padlock)
                    {
                        if (_callback.ContainsKey(userName))
                        {
                            GameMng.AddGame(userName, opponentName);
                            _callback[userName].StartGameFromServer(userName, opponentName, master);
                        }
                    }
                }
                catch (Exception ex)
                {
                    callback.RecieveError(ex.Message);
                }
            }
        }

        public void UserLeftGame(string userName, string opponentName)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(opponentName))
            {
                var callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
                try
                {
                    BackToChat(opponentName, userName);

                    GameMng.DeleteHowToFirst(userName, opponentName);
                    GameMng.DeleteGame(userName, opponentName);
                }
                catch (Exception ex)
                {
                    callback.RecieveError(ex.Message);
                }
            }
        }

        /// <summary>
        /// the game ended with the victory of one of the users
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="opponentName"></param>
        public void GameOver(string userName, string opponentName)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(opponentName))
            {
                var callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
                try
                {
                    if (_callback.ContainsKey(opponentName))
                    {
                        _callback[userName].GameOverFromServer(userName, opponentName, true);
                        _callback[opponentName].GameOverFromServer(opponentName, userName, false);
                        GameMng.DeleteGame(userName, opponentName);
                    }
                }
                catch (Exception ex)
                {
                    callback.RecieveError(ex.InnerException.Message);
                }
            }
        }

        /// <summary>
        /// Generates random numbers in dice range
        /// </summary>
        /// <param name="user">user name</param>
        /// <param name="opponent">opponent name</param>
        public void Roll(string user, string opponent)
        {
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(opponent))
            {
                var game = GameMng.GetGame(user, opponent);
                if (game == null) return;

                var diceLogic = new DiceLogic(_rand);
                var diceNumbers = diceLogic.GetRandomDices();

                var callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
                try
                {
                    if (_callback.ContainsKey(user))
                    {
                        if (diceNumbers[0] == diceNumbers[1])
                        {
                            game.DoubleDice = new DoubleDice(user, opponent);

                            _callback[user].GameInfoUpdateFromServer(opponent, "You have the double.\nMove twice please.");
                            if (_callback.ContainsKey(opponent))
                                _callback[opponent].GameInfoUpdateFromServer(user, "Your opponent has the double.\nWait please.");
                        }
                        else game.DifferentDice = new DifferentDice(user, opponent);

                        if (_callback.ContainsKey(opponent))
                            _callback[opponent].RecieveRoll(user, diceNumbers);

                        _callback[user].RecieveRoll(opponent, diceNumbers);
                    }
                }
                catch (Exception ex)
                {
                    callback.RecieveError(ex.Message);
                    if (_callback.ContainsKey(opponent))
                        _callback[opponent].RecieveError(ex.Message);
                }
            }
        }

        /// <summary>
        /// Moves the selected checker
        /// </summary>
        /// <param name="user">user name</param>
        /// <param name="opponent">opponent name</param>
        /// <param name="currIndex">index of selected checker</param>
        /// <param name="number">number  the dice</param>
        public void MoveChecker(string user, string opponent, int currIndex, int number)
        {
            //lock (padlock)
            //{
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(opponent))
            {
                var game = GameMng.GetGame(user, opponent);
                if (game == null) return;

                var callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
                bool result = false;

                if (_callback.ContainsKey(user))
                {
                    try
                    {
                        var doubleDice = game.DoubleDice;
                        var diffDice = game.DifferentDice;

                        try
                        {
                            result = GameMng.CheckersMoovingLogic(user, opponent, currIndex, number);
                        }
                        catch (Exception ex)
                        {
                            _callback[user].RecieveGameError(ex.Message);
                            _callback[user].MoveEnableFromServer(user, opponent);
                            if (_callback.ContainsKey(opponent))
                            {
                                _callback[opponent].SetWaiterFromServer(opponent, user);
                            }
                            return;
                        }
                        if (result)
                        {
                            var gameTable = GameMng.GetGameTableForSend(user, opponent);

                            var whiteCheckers = gameTable[0];
                            var blackCheckers = gameTable[1];
                            var barCheckers = gameTable[2];
                            _callback[user].UpdateTable(user, opponent, whiteCheckers, blackCheckers, barCheckers);
                            if (opponent != Player.autoPlayer.ToString())
                                _callback[opponent].UpdateTable(opponent, user, whiteCheckers, blackCheckers, barCheckers);

                            if (doubleDice != null && doubleDice.IsMoovingFinish())
                            {
                                // GameMng.DeleteDoubleDice(user, opponent);
                                if (opponent != Player.autoPlayer.ToString())
                                    _callback[opponent].SetMasterFromServer(opponent, user);
                                _callback[user].SetWaiterFromServer(user, opponent);
                                //turn computer moving
                                if (opponent == Player.autoPlayer.ToString())
                                    MoveCheckerAuto(opponent, user);
                            }
                            else if (diffDice != null && diffDice.IsMoovingFinish())
                            {
                                // GameMng.DeleteDifferentDice(user, opponent);
                                if (opponent != Player.autoPlayer.ToString())
                                    _callback[opponent].SetMasterFromServer(opponent, user);
                                _callback[user].SetWaiterFromServer(user, opponent);
                                //turn computer moving
                                if (opponent == Player.autoPlayer.ToString())
                                    MoveCheckerAuto(opponent, user);
                            }

                            if (GameMng.IsGameOver(user, opponent))
                            {
                                _callback[user].GameOverFromServer(user, opponent, true);
                                if (opponent != Player.autoPlayer.ToString())
                                    _callback[opponent].GameOverFromServer(opponent, user, false);
                                GameMng.DeleteGame(user, opponent);
                            }

                        }
                        else
                        {
                            if (_callback.ContainsKey(opponent))

                                _callback[user].SetWaiterFromServer(user, opponent);

                            if (_callback.ContainsKey(opponent))
                                _callback[opponent].SetMasterFromServer(opponent, user);
                            else MoveCheckerAuto(opponent, user);
                        }
                    }
                    catch (Exception ex)
                    {
                        callback.RecieveGameError(ex.Message);
                        if (_callback.ContainsKey(opponent))
                            _callback[opponent].RecieveGameError(ex.Message);
                    }
                }
            }
            //}
        }

        /// <summary>
        /// Computer Player Logic
        /// </summary>
        /// <param name="computer">name of comp player</param>
        /// <param name="opponent">name of user player</param>
        /// <param name="diceNumbers">number of dice</param>
        public void MoveCheckerAuto(string computer, string opponent)
        {

            if (_callback.ContainsKey(opponent))
            {
                var game = GameMng.GetGame(computer, opponent);
                if (game == null) return;

                var diceNumbers = game.DiceLogic.GetRandomDices();

                if (diceNumbers[0] == diceNumbers[1])
                    diceNumbers.AddRange(diceNumbers);

                _callback[opponent].RecieveRoll(Player.autoPlayer.ToString(), diceNumbers);

                bool result = false;

                try
                {
                    foreach (var number in diceNumbers)
                    {
                        result = GameMng.CheckersMoovingLogicAuto(computer, opponent, number);
                        if (result)
                        {
                            var gameTable = GameMng.GetGameTableForSend(computer, opponent);

                            if (gameTable != null)
                            {
                                var whiteCheckers = gameTable[0];
                                var blackCheckers = gameTable[1];
                                var barCheckers = gameTable[2];
                                _callback[opponent].UpdateTable(opponent, computer, whiteCheckers, blackCheckers, barCheckers);
                            }
                        }
                        if (GameMng.IsGameOver(opponent, computer))
                        {
                            _callback[opponent].GameOverFromServer(opponent, computer, false);

                            GameMng.DeleteGame(computer, opponent);
                        }


                    }
                    if (_callback.ContainsKey(opponent))
                        _callback[opponent].SetMasterFromServer(opponent, computer);
                }
                catch (Exception ex)
                {
                    _callback[opponent].RecieveError(ex.Message);
                    return;
                }
            }
        }

        public void MoveCheckerFromBar(string user, string opponent, int currIndex, int futuredIndex)
        {
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(opponent))
            {
                var callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
                try
                {


                    if (_callback.ContainsKey(user))
                    {
                        var game = GameMng.GetGame(user, opponent);
                        var doubleDice = game.DoubleDice;
                        var diffDice = game.DifferentDice;

                        bool result = false;

                        try
                        {
                            result = GameMng.CheckersMoovingFromBarLogic(user, opponent, currIndex, futuredIndex);

                        }
                        catch (Exception ex)
                        {
                            _callback[user].RecieveGameError(ex.Message);
                            _callback[user].MoveEnableFromServer(user, opponent);
                            if (_callback.ContainsKey(opponent))
                            {
                                _callback[opponent].SetWaiterFromServer(opponent, user);
                            }
                            return;
                        }
                        if (result)
                        {
                            var gameTable = GameMng.GetGameTableForSend(user, opponent);

                            if (gameTable != null)
                            {
                                var whiteCheckers = gameTable[0];
                                var blackCheckers = gameTable[1];
                                var barCheckers = gameTable[2];
                                _callback[user].UpdateTable(user, opponent, whiteCheckers, blackCheckers, barCheckers);
                                if (_callback.ContainsKey(opponent))
                                    _callback[opponent].UpdateTable(opponent, user, whiteCheckers, blackCheckers, barCheckers);

                                if (doubleDice != null && doubleDice.IsMoovingFinish())
                                {
                                    // GameMng.DeleteDoubleDice(user, opponent);
                                    _callback[user].SetWaiterFromServer(user, opponent);
                                    if (_callback.ContainsKey(opponent))
                                        _callback[opponent].SetMasterFromServer(opponent, user);
                                    //turn computer moving
                                    else if (opponent == Player.autoPlayer.ToString())
                                        MoveCheckerAuto(opponent, user);
                                }
                                else if (diffDice != null && diffDice.IsMoovingFinish())
                                {
                                    //GameMng.DeleteDifferentDice(user, opponent);
                                    _callback[user].SetWaiterFromServer(user, opponent);
                                    if (_callback.ContainsKey(opponent))
                                        _callback[opponent].SetMasterFromServer(opponent, user);
                                    //turn computer moving
                                    else if (opponent == Player.autoPlayer.ToString())
                                        MoveCheckerAuto(opponent, user);
                                }
                            }
                        }
                        else
                        {
                            _callback[user].SetMasterFromServer(user, opponent);
                            _callback[user].RecieveGameError("You can't move to this posision");
                        }
                    }
                }
                catch (Exception ex)
                {
                    callback.RecieveGameError(ex.Message);
                    if (_callback.ContainsKey(opponent))
                        _callback[opponent].RecieveGameError(ex.Message);
                }
            }
        }

        public void RollEnable(string userName, string opponentName)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(opponentName))
            {
                var callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
                try
                {
                    if (_callback.ContainsKey(opponentName))
                    {
                        _callback[opponentName].RollEnableFromServer(opponentName, userName);
                    }
                }
                catch (Exception ex)
                {
                    callback.RecieveError(ex.Message);
                }
            }
        }

        //Disable Roll Button
        public void RollDisable(string user, string opponent)
        {
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(opponent))
            {
                var callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
                try
                {
                    if (_callback.ContainsKey(opponent))
                    {
                        _callback[opponent].RollDisableFromServer(opponent, user);
                    }
                }
                catch (Exception ex)
                {
                    callback.RecieveError(ex.InnerException.Message);
                }
            }
        }

        //Update game Info
        public void GameInfoUpdate(string user, string opponent, string content)
        {
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(opponent) && !string.IsNullOrEmpty(content))
            {
                var callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
                try
                {
                    if (_callback.ContainsKey(user))
                    {
                        _callback[opponent].GameInfoUpdateFromServer(user, content);
                    }
                }
                catch (Exception ex)
                {
                    callback.RecieveError(ex.Message);
                }
            }
        }

        public void SetMaster(string user, string opponent)
        {
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(opponent))
            {
                var callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
                try
                {
                    if (_callback.ContainsKey(user))
                    {
                        if (_callback.ContainsKey(opponent))
                            _callback[opponent].SetMasterFromServer(opponent, user);
                        else
                        {
                            var computer = Player.autoPlayer.ToString();

                            MoveCheckerAuto(computer, user);
                        }
                    }
                }
                catch (Exception ex)
                {
                    callback.RecieveError(ex.Message);
                }
            }
        }

        public void SetWaiter(string user, string opponent)
        {
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(opponent))
            {
                var callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
                try
                {
                    if (_callback.ContainsKey(opponent))
                    {
                        _callback[opponent].SetWaiterFromServer(opponent, user);
                    }
                }
                catch (Exception ex)
                {
                    callback.RecieveError(ex.Message);
                }
            }
        }

        public void GetGameTable(string user, string opponent)
        {
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(opponent))
            {
                try
                {
                    if (_callback.ContainsKey(user))
                    {
                        var game = GameMng.GetGame(user, opponent);

                        var gameTable = GameMng.GetGameTableForSend(user, opponent);

                        if (gameTable != null)
                        {
                            var whiteCheckers = gameTable[0];
                            var blackCheckers = gameTable[1];
                            var barCheckers = gameTable[2];
                            _callback[user].UpdateTable(user, opponent, whiteCheckers, blackCheckers, barCheckers);
                            if (_callback.ContainsKey(opponent))
                                _callback[opponent].UpdateTable(opponent, user, whiteCheckers, blackCheckers, barCheckers);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _callback[user].RecieveError(ex.Message);
                    _callback[opponent].RecieveError(ex.Message);
                }
            }
        }
        #endregion

        #region How To First

        public void StartHowToFirst(string user, string opponent)
        {
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(opponent))
            {
                try
                {
                    if (_callback.ContainsKey(user))
                    {
                        if (_callback.ContainsKey(opponent))
                        {
                            GameMng.AddHowToFirst(user, opponent);
                            _callback[user].StartHowToFirstFromServer(user, opponent);
                            _callback[opponent].StartHowToFirstFromServer(opponent, user);

                        }
                        else if (opponent == "autoPlayer")
                        {
                            GameMng.AddHowToFirst(user, opponent);
                            _callback[user].StartHowToFirstFromServer(user, opponent);
                        }
                    }

                }
                catch (Exception ex)
                {
                    _callback[user].RecieveError(ex.Message);
                    _callback[opponent].RecieveError(ex.Message);
                }
            }
        }

        public void CloseHowToFirst(string user, string opponent)
        {
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(opponent))
            {
                try
                {
                    if (_callback.ContainsKey(user) && _callback.ContainsKey(opponent))
                    {
                        GameMng.DeleteHowToFirst(user, opponent);
                        //_callback[user].CloseHowToFirstFromServer(user, opponent, false);
                        //_callback[opponent].CloseHowToFirstFromServer(opponent, user, false);
                        BackToChat(user, opponent);
                    }
                }
                catch (Exception ex)
                {
                    _callback[user].RecieveError(ex.Message);
                    _callback[opponent].RecieveError(ex.Message);
                }
            }
        }

        public void RollHowFirst(string user, string opponent)
        {
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(opponent))
            {
                var callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
                try
                {
                    //lock (padlock)
                    //{
                    if (_callback.ContainsKey(user) && _callback.ContainsKey(opponent))
                    {
                        var arrayResult = GameMng.RollHowFirst(user, opponent);

                        var number1 = int.Parse(arrayResult[0]);
                        var number2 = int.Parse(arrayResult[1]);
                        int[] resultToSend = new int[2];

                        resultToSend[0] = (number1 == 0) ? 1 : number1;
                        resultToSend[1] = (number2 == 0) ? 1 : number2;

                        _callback[user].RecieveRollHowFirst(user, opponent, resultToSend);
                        _callback[opponent].RecieveRollHowFirst(opponent, user, resultToSend);

                        if (arrayResult[2] != string.Empty)
                        {
                            var winnerName = arrayResult[2];
                            if (winnerName == user)
                            {
                                _callback[user].CloseHowToFirstFromServer(user, opponent, true);
                                _callback[opponent].CloseHowToFirstFromServer(opponent, user, false);
                                GameMng.DeleteHowToFirst(user, opponent);
                            }
                            else
                            {
                                _callback[opponent].CloseHowToFirstFromServer(opponent, user, true);
                                _callback[user].CloseHowToFirstFromServer(user, opponent, false);
                                GameMng.DeleteHowToFirst(user, opponent);
                            }
                        }
                        else
                        {

                            if (number1 == number2)
                            {
                                _callback[user].GameInfoUpdateFromServer(opponent, "The numbers are equals. Try again");
                                _callback[opponent].GameInfoUpdateFromServer(user, "The numbers are equals. Try again");
                            }

                        }
                    }
                    else if (_callback.ContainsKey(user) && opponent == "autoPlayer")
                    {
                        RollHowFirstComp(user, opponent);
                    }

                    //}
                }
                catch (Exception ex)
                {
                    if (_callback.ContainsKey(user)) callback.RecieveError(ex.Message);
                    if (_callback.ContainsKey(opponent)) _callback[opponent].RecieveError(ex.Message);
                }
            }
        }
        #endregion

        #region Game with computer

        public void RollHowFirstComp(string user, string computer)
        {
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(computer))
            {
                var callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();

                var arrayResult = GameMng.RollHowFirst(user, computer);

                var number1 = int.Parse(arrayResult[0]);
                var number2 = int.Parse(arrayResult[1]);
                int[] resultToSend = new int[2];

                resultToSend[0] = (number1 == 0) ? 1 : number1;
                resultToSend[1] = (number2 == 0) ? 1 : number2;

                //Send to User result of first roll
                _callback[user].RecieveRollHowFirst(user, computer, resultToSend);

                //Computer rolling
                arrayResult = GameMng.RollHowFirst(computer, user);
                number1 = int.Parse(arrayResult[0]);
                number2 = int.Parse(arrayResult[1]);
                resultToSend = new int[2];

                resultToSend[0] = (number1 == 0) ? 1 : number1;
                resultToSend[1] = (number2 == 0) ? 1 : number2;

                //Send to User result of second roll
                _callback[user].RecieveRollHowFirst(user, computer, resultToSend);

                if (arrayResult[2] != string.Empty)
                {
                    var winnerName = arrayResult[2];
                    if (winnerName == user)
                    {
                        _callback[user].CloseHowToFirstFromServer(user, computer, true);
                        GameMng.DeleteHowToFirst(user, computer);
                        GameMng.AddGame(user, computer);
                    }
                    else
                    {
                        _callback[user].CloseHowToFirstFromServer(user, computer, false);
                        GameMng.DeleteHowToFirst(user, computer);
                        GameMng.AddGame(computer, user);
                        //MoveCheckerAuto(computer, user);
                    }
                }
                else
                {

                    if (number1 == number2)
                    {
                        _callback[user].GameInfoUpdateFromServer(computer, "The numbers are equals. Try again");
                    }

                }

            }

        }

        public void StartAutoPlayer(string userName)
        {
            if (!string.IsNullOrEmpty(userName) && _callback.ContainsKey(userName))
            {
                MoveCheckerAuto(Player.autoPlayer.ToString(), userName);
            }
        }
        #endregion

        /// <summary>
        /// Back to general chat
        /// </summary>
        /// <param name="user"></param>
        /// <param name="opponent"></param>
        public void BackToChat(string user, string opponent)
        {
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(opponent))
            {
                try
                {
                    GameMng.DeleteGame(user, opponent);
                    GameMng.DeleteHowToFirst(user, opponent);

                    if (_callback.ContainsKey(user))
                        _callback[user].BackToChatFromServer(user, opponent);

                    if (_callback.ContainsKey(opponent))
                        _callback[opponent].BackToChatFromServer(opponent, user);
                }
                catch (Exception ex)
                {
                    _callback[user].RecieveError(ex.Message);
                    _callback[opponent].RecieveError(ex.Message);
                }
            }
        }
    }

}
