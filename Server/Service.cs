using BLServer;
using Common.Data;
using Common.Interface;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;

namespace Server
{
    [CallbackBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]

    /// <summary>
    /// Contains all methods without logic for performing connecting users and DB.
    /// </summary>
    public class Service: IChatService
    {
        public Service() { }


        #region Private Chat

        public void ExitPrivateChat(string userName, string opponentName)
        {
            ServerLogic.Instance.ExitPrivateChat(userName, opponentName);          
        }

        public void PrivateChat(string userName, string opponentName)
        {
            ServerLogic.Instance.PrivateChatStart(userName,opponentName);
        }
        public void ConfirmationPrivateChat(string userName, string opponentName)
        {
            ServerLogic.Instance.ConfirmationPrivateChat(userName,opponentName);
        }

        #endregion

        #region Authentication
        public void Login(string username, string password)
        {
            var user = username.ToLower();
            ServerLogic.Instance.Login(username, password);
        }

        public void Logout(string username)
        {
            ServerLogic.Instance.Logout(username);
        }

        public void Register( string username, string password,string firstName, string lastName)
        {
            var user = username.ToLower();
            ServerLogic.Instance.Register( username, password,firstName, lastName);
        }
        #endregion

        #region Messages
        public void SendMessage(string username, string content)
        {
            ServerLogic.Instance.SendMessage(username, content);
        }

        public void SendPrivateMsg(string from, string to, string content)
        {
            ServerLogic.Instance.SendPrivateMsg(from,to,content);
        }
        #endregion

        #region Game
        //Game
        public void StartGame(string userName, string opponentName,bool master)
        {
            ServerLogic.Instance.StartGame(userName, opponentName,master);
        }

        public void GameOver(string userName, string opponentName)
        {
            ServerLogic.Instance.GameOver(userName,opponentName);
        }

        public void Roll(string user, string opponent)
        {
            ServerLogic.Instance.Roll(user, opponent);
        }

        public void SendErrorMessage(string username, string content)
        {
            ServerLogic.Instance.SendErrorMessage(username,content);
        }

        public void RequestGame(string userName, string opponentName)
        {
            ServerLogic.Instance.RequestGame(userName, opponentName);
        }

        public void UserLeftGame(string userName, string opponentName)
        {
            ServerLogic.Instance.UserLeftGame(userName, opponentName);
        }

        public void RollEnable(string user, string opponent)
        {
            ServerLogic.Instance.RollEnable(user,opponent);
        }

        public void RollDisable(string user, string opponent)
        {
            ServerLogic.Instance.RollDisable(user, opponent);
        }

        public void GameInfoUpdate(string user, string opponent, string content)
        {
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(content))
            {
                ServerLogic.Instance.GameInfoUpdate(user,opponent, content);
            }
        }

        public void SetMaster(string user, string opponent)
        {
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(opponent))
            {
                ServerLogic.Instance.SetMaster(user, opponent);
            }
        }

        public void SetWaiter(string user, string opponent)
        {
            ServerLogic.Instance.SetWaiter(user, opponent);
        }

        public void MoveChecker(string user, string opponent, int currIndex, int futherIndex)
        {
            ServerLogic.Instance.MoveChecker(user, opponent, currIndex, futherIndex) ;
        }

        public void GetGameTable(string user, string opponent)
        {
            ServerLogic.Instance.GetGameTable(user, opponent);
        }

        public void MoveCheckerFromBar(string user, string opponent, int currIndex, int futuredIndex)
        {
            ServerLogic.Instance.MoveCheckerFromBar( user,  opponent,  currIndex,  futuredIndex);
        }

        public void StartAutoPlayer(string userName)
        {
            ServerLogic.Instance.StartAutoPlayer(userName);
        }
        #endregion

        #region How to First
        public void StartHowToFirst(string userName, string opponentName)
        {
            ServerLogic.Instance.StartHowToFirst(userName, opponentName);
        }

        public void CloseHowToFirst(string userName, string opponentName)
        {
            ServerLogic.Instance.CloseHowToFirst(userName, opponentName);
        }

        public void RollHowFirst(string user, string opponent)
        {
            ServerLogic.Instance.RollHowFirst(user, opponent);
        }

        #endregion

        public void BackToChat(string user, string opponent)
        {
            ServerLogic.Instance.BackToChat(user, opponent);
        }

    }
}
