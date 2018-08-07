using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interface
{
    [ServiceContract(CallbackContract = typeof(IClientCallback))]
    public  interface IGameService
    {
        #region Game
        //Game request
        [OperationContract(IsOneWay = true)]
        void StartGame(string userName, string opponentName, bool master);

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
        void RollEnable(string user, string opponent);

        [OperationContract(IsOneWay = true)]
        void RollDisable(string user, string opponent);

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
    }
}
