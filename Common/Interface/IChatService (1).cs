using System;
using System.Collections.Generic;
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
        [OperationContract/*(IsOneWay = true)*/]
        void Register(string username, string password,string firstName, string lastName );

        //[OperationContract]
        //string GetOnLineUsers();

        //[OperationContract]
        //string GetOffLineUsers();

        [OperationContract]
        void Login(string username, string password);

        [OperationContract(IsOneWay = true)]
        void SendMessage(string username, string content);
    }

}
