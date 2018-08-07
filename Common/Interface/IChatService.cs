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

        [OperationContract(IsOneWay = true)]
        void BackToChat(string user, string opponent);
    }

}
