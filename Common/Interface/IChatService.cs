using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Data;

namespace Common.Interface
{
    [ServiceContract(CallbackContract = typeof(IClientCallback))]

    public interface IChatService
    {
        #region Authentication
        [OperationContract(IsOneWay = true)]
        void Register(string username, string password,string firstName, string lastName );

        [OperationContract(IsOneWay = true)]
        void Login(string username, string password);

        [OperationContract(IsOneWay = true)]
        void Logout(string username);
        #endregion

        #region Message
        [OperationContract(IsOneWay = true)]
        void SendMessage(string username, string content);

        [OperationContract(IsOneWay = true)]
        void SendErrorMessage(string username, string content);

        [OperationContract(IsOneWay = true)]
        void SendPrivateMsg(string from,string to, string content);
        #endregion

        #region Private Chat
        [OperationContract(IsOneWay = true)]
        void PrivateChat(string userName, string opponentName);

        [OperationContract(IsOneWay = true)]
        void ExitPrivateChat(string userName,string opponentName);

        [OperationContract(IsOneWay = true)]
        void ConfirmationPrivateChat(string userName, string opponentName);
        #endregion

        #region Game
        //Game request
        [OperationContract(IsOneWay = true)]
        void StartGame(string userName, string opponentName,bool master);

        [OperationContract(IsOneWay = true)]
        void StartAutoPlayer(string userName);

        [OperationContract(IsOneWay = true)]
        void RequestGame(string userName, string opponentName);

        [OperationContract(IsOneWay = true)]
        void GameOver(string userName, string opponentName);

        //User left the game
        [OperationContract(IsOneWay = true)]
        void UserLeftGame(string userName, string opponentName);



        //Game Operations
        [OperationContract(IsOneWay = true)]
        void Roll(string user, string opponent);

        [OperationContract(IsOneWay = true)]
        void RollEnable( string user, string opponent);

        [OperationContract(IsOneWay = true)]
        void RollDisable( string user, string opponent);

        [OperationContract(IsOneWay = true)]
        void SetMaster(string user, string opponent);

        [OperationContract(IsOneWay = true)]
        void GameInfoUpdate(string user, string opponent, string content);

        [OperationContract(IsOneWay = true)]
        void SetWaiter(string user, string opponent);

        [OperationContract(IsOneWay = true)]
        void MoveChecker(string user, string opponent, int currIndex, int number);

        [OperationContract(IsOneWay = true)]
        void MoveCheckerFromBar(string user, string opponent, int currIndex, int number);

        [OperationContract(IsOneWay = true)]
        void GetGameTable(string user, string opponent);

        #endregion

        #region How To First
        [OperationContract(IsOneWay = true)]
        void RollHowFirst(string user, string opponent);

        [OperationContract(IsOneWay = true)]
        void StartHowToFirst(string userName, string opponentName);


        [OperationContract(IsOneWay = true)]
        void CloseHowToFirst(string userName, string opponentName);

        #endregion

        #region Game with Computer
        //[OperationContract(IsOneWay = true)]
        //void StartHowToFirstComp(string userName, string opponentName);

        //[OperationContract(IsOneWay = true)]
        //void RollHowFirstComp(string user, string opponent);
        #endregion

        [OperationContract(IsOneWay = true)]
        void BackToChat(string user, string opponent);
    }

}
