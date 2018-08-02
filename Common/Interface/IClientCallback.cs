using Common.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interface
{
    [ServiceContract]
    public interface IClientCallback
    {
        #region Message
        [OperationContract(IsOneWay = true)]
        void RecieveMessage(string userName, string text);

        [OperationContract(IsOneWay = true)]
        void RecieveGameError( string text);

        [OperationContract(IsOneWay = true)]
        void RecieveError( string error);
        #endregion

        [OperationContract(IsOneWay = true)]
        void UsersOnLine(List<string> usersOnLine,List<string> usersOffLine);

        #region Authentication
        [OperationContract(IsOneWay = true ,Name ="ResultActionfromLogout")]
        void Logout();

        [OperationContract(IsOneWay = true, Name = "ResultActionfromLogoin")]
        void Login(string username);
        #endregion

        #region Private Chat
        [OperationContract(IsOneWay = true)]
        void RecievePrivateMsg(string from,string key, string content);

        [OperationContract(IsOneWay = true)]
        void PrivateChatStart(string userName, string opponentName);

        [OperationContract(IsOneWay = true)]
        void PrivateChatStartFromOpponent(string userName, string opponentName);

        [OperationContract(IsOneWay = true)]
        void PrivateChatSlose(string opponentName);
        #endregion

        #region Game
        //Game
        [OperationContract(IsOneWay = true)]
        void RequestGameFromServer(string userName, string opponentName);

        [OperationContract(IsOneWay = true)]
        void StartGameFromServer(string userName, string opponentName,bool master);

        [OperationContract(IsOneWay = true)]
        void GameOverFromServer(string userName, string opponentName,bool winner);

        [OperationContract(IsOneWay = true)]
        void RecieveRoll(string opponent,List<int> numbers);

        [OperationContract(IsOneWay = true)]
        void RollEnableFromServer(string user, string opponentName);

        [OperationContract(IsOneWay = true)]
        void RollDisableFromServer(string user, string opponentName);

        [OperationContract(IsOneWay = true)]
        void MoveEnableFromServer(string user, string opponentName);

        [OperationContract(IsOneWay = true)]
        void GameInfoUpdateFromServer(string opponentName, string content);

        [OperationContract(IsOneWay = true)]
        void SetMasterFromServer(string user, string opponentName);

        [OperationContract(IsOneWay = true)]
        void SetWaiterFromServer(string user, string opponentName);

        [OperationContract(IsOneWay = true)]
        void MoveCheckerFromServer(string user, string opponent, int currIndex, int futherIndex, bool IsbeatPiece);

        [OperationContract(IsOneWay = true)]
        void UpdateTable(string user, string opponent, int[] whiteCheckers, int[] blackCheckers, int[] barCheckers);

        #endregion

        #region How to First

        [OperationContract(IsOneWay = true)]
        void StartHowToFirstFromServer(string userName, string opponentName);

        [OperationContract(IsOneWay = true)]
        void CloseHowToFirstFromServer(string userName, string opponentName, bool winner);

        [OperationContract(IsOneWay = true)]
        void RecieveRollHowFirst(string user, string opponent, int[] numbers);

        #endregion

        [OperationContract(IsOneWay = true)]
        void BackToChatFromServer(string user, string opponent);
    }
}
