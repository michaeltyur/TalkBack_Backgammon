﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.ChatServer {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ChatServer.IChatService", CallbackContract=typeof(Client.ChatServer.IChatServiceCallback))]
    public interface IChatService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/Register")]
        void Register(string username, string password, string firstName, string lastName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/Register")]
        System.Threading.Tasks.Task RegisterAsync(string username, string password, string firstName, string lastName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/Login")]
        void Login(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/Login")]
        System.Threading.Tasks.Task LoginAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/Logout")]
        void Logout(string username);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/Logout")]
        System.Threading.Tasks.Task LogoutAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/SendMessage")]
        void SendMessage(string username, string content);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/SendMessage")]
        System.Threading.Tasks.Task SendMessageAsync(string username, string content);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/SendErrorMessage")]
        void SendErrorMessage(string username, string content);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/SendErrorMessage")]
        System.Threading.Tasks.Task SendErrorMessageAsync(string username, string content);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/SendPrivateMsg")]
        void SendPrivateMsg(string from, string to, string content);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/SendPrivateMsg")]
        System.Threading.Tasks.Task SendPrivateMsgAsync(string from, string to, string content);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/PrivateChat")]
        void PrivateChat(string userName, string opponentName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/PrivateChat")]
        System.Threading.Tasks.Task PrivateChatAsync(string userName, string opponentName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/ExitPrivateChat")]
        void ExitPrivateChat(string userName, string opponentName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/ExitPrivateChat")]
        System.Threading.Tasks.Task ExitPrivateChatAsync(string userName, string opponentName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/ConfirmationPrivateChat")]
        void ConfirmationPrivateChat(string userName, string opponentName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/ConfirmationPrivateChat")]
        System.Threading.Tasks.Task ConfirmationPrivateChatAsync(string userName, string opponentName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/StartGame")]
        void StartGame(string userName, string opponentName, bool master);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/StartGame")]
        System.Threading.Tasks.Task StartGameAsync(string userName, string opponentName, bool master);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/StartAutoPlayer")]
        void StartAutoPlayer(string userName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/StartAutoPlayer")]
        System.Threading.Tasks.Task StartAutoPlayerAsync(string userName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/RequestGame")]
        void RequestGame(string userName, string opponentName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/RequestGame")]
        System.Threading.Tasks.Task RequestGameAsync(string userName, string opponentName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/GameOver")]
        void GameOver(string userName, string opponentName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/GameOver")]
        System.Threading.Tasks.Task GameOverAsync(string userName, string opponentName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/UserLeftGame")]
        void UserLeftGame(string userName, string opponentName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/UserLeftGame")]
        System.Threading.Tasks.Task UserLeftGameAsync(string userName, string opponentName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/Roll")]
        void Roll(string user, string opponent);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/Roll")]
        System.Threading.Tasks.Task RollAsync(string user, string opponent);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/RollEnable")]
        void RollEnable(string user, string opponent);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/RollEnable")]
        System.Threading.Tasks.Task RollEnableAsync(string user, string opponent);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/RollDisable")]
        void RollDisable(string user, string opponent);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/RollDisable")]
        System.Threading.Tasks.Task RollDisableAsync(string user, string opponent);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/SetMaster")]
        void SetMaster(string user, string opponent);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/SetMaster")]
        System.Threading.Tasks.Task SetMasterAsync(string user, string opponent);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/GameInfoUpdate")]
        void GameInfoUpdate(string user, string opponent, string content);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/GameInfoUpdate")]
        System.Threading.Tasks.Task GameInfoUpdateAsync(string user, string opponent, string content);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/SetWaiter")]
        void SetWaiter(string user, string opponent);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/SetWaiter")]
        System.Threading.Tasks.Task SetWaiterAsync(string user, string opponent);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/MoveChecker")]
        void MoveChecker(string user, string opponent, int currIndex, int number);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/MoveChecker")]
        System.Threading.Tasks.Task MoveCheckerAsync(string user, string opponent, int currIndex, int number);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/MoveCheckerFromBar")]
        void MoveCheckerFromBar(string user, string opponent, int currIndex, int number);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/MoveCheckerFromBar")]
        System.Threading.Tasks.Task MoveCheckerFromBarAsync(string user, string opponent, int currIndex, int number);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/GetGameTable")]
        void GetGameTable(string user, string opponent);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/GetGameTable")]
        System.Threading.Tasks.Task GetGameTableAsync(string user, string opponent);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/RollHowFirst")]
        void RollHowFirst(string user, string opponent);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/RollHowFirst")]
        System.Threading.Tasks.Task RollHowFirstAsync(string user, string opponent);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/StartHowToFirst")]
        void StartHowToFirst(string userName, string opponentName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/StartHowToFirst")]
        System.Threading.Tasks.Task StartHowToFirstAsync(string userName, string opponentName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/CloseHowToFirst")]
        void CloseHowToFirst(string userName, string opponentName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/CloseHowToFirst")]
        System.Threading.Tasks.Task CloseHowToFirstAsync(string userName, string opponentName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/BackToChat")]
        void BackToChat(string user, string opponent);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/BackToChat")]
        System.Threading.Tasks.Task BackToChatAsync(string user, string opponent);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IChatServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/RecieveMessage")]
        void RecieveMessage(string userName, string text);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/RecieveGameError")]
        void RecieveGameError(string text);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/RecieveError")]
        void RecieveError(string error);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/UsersOnLine")]
        void UsersOnLine([System.ServiceModel.MessageParameterAttribute(Name="usersOnLine")] string[] usersOnLine1, string[] usersOffLine);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/ResultActionfromLogout")]
        void ResultActionfromLogout();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/ResultActionfromLogoin")]
        void ResultActionfromLogoin(string username);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/RecievePrivateMsg")]
        void RecievePrivateMsg(string from, string key, string content);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/PrivateChatStart")]
        void PrivateChatStart(string userName, string opponentName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/PrivateChatStartFromOpponent")]
        void PrivateChatStartFromOpponent(string userName, string opponentName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/PrivateChatSlose")]
        void PrivateChatSlose(string opponentName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/RequestGameFromServer")]
        void RequestGameFromServer(string userName, string opponentName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/StartGameFromServer")]
        void StartGameFromServer(string userName, string opponentName, bool master);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/GameOverFromServer")]
        void GameOverFromServer(string userName, string opponentName, bool winner);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/RecieveRoll")]
        void RecieveRoll(string opponent, int[] numbers);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/RollEnableFromServer")]
        void RollEnableFromServer(string user, string opponentName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/RollDisableFromServer")]
        void RollDisableFromServer(string user, string opponentName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/MoveEnableFromServer")]
        void MoveEnableFromServer(string user, string opponentName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/GameInfoUpdateFromServer")]
        void GameInfoUpdateFromServer(string opponentName, string content);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/SetMasterFromServer")]
        void SetMasterFromServer(string user, string opponentName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/SetWaiterFromServer")]
        void SetWaiterFromServer(string user, string opponentName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/MoveCheckerFromServer")]
        void MoveCheckerFromServer(string user, string opponent, int currIndex, int futherIndex, bool IsbeatPiece);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/UpdateTable")]
        void UpdateTable(string user, string opponent, int[] whiteCheckers, int[] blackCheckers, int[] barCheckers);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/StartHowToFirstFromServer")]
        void StartHowToFirstFromServer(string userName, string opponentName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/CloseHowToFirstFromServer")]
        void CloseHowToFirstFromServer(string userName, string opponentName, bool winner);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/RecieveRollHowFirst")]
        void RecieveRollHowFirst(string user, string opponent, int[] numbers);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/BackToChatFromServer")]
        void BackToChatFromServer(string user, string opponent);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IChatServiceChannel : Client.ChatServer.IChatService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ChatServiceClient : System.ServiceModel.DuplexClientBase<Client.ChatServer.IChatService>, Client.ChatServer.IChatService {
        
        public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void Register(string username, string password, string firstName, string lastName) {
            base.Channel.Register(username, password, firstName, lastName);
        }
        
        public System.Threading.Tasks.Task RegisterAsync(string username, string password, string firstName, string lastName) {
            return base.Channel.RegisterAsync(username, password, firstName, lastName);
        }
        
        public void Login(string username, string password) {
            base.Channel.Login(username, password);
        }
        
        public System.Threading.Tasks.Task LoginAsync(string username, string password) {
            return base.Channel.LoginAsync(username, password);
        }
        
        public void Logout(string username) {
            base.Channel.Logout(username);
        }
        
        public System.Threading.Tasks.Task LogoutAsync(string username) {
            return base.Channel.LogoutAsync(username);
        }
        
        public void SendMessage(string username, string content) {
            base.Channel.SendMessage(username, content);
        }
        
        public System.Threading.Tasks.Task SendMessageAsync(string username, string content) {
            return base.Channel.SendMessageAsync(username, content);
        }
        
        public void SendErrorMessage(string username, string content) {
            base.Channel.SendErrorMessage(username, content);
        }
        
        public System.Threading.Tasks.Task SendErrorMessageAsync(string username, string content) {
            return base.Channel.SendErrorMessageAsync(username, content);
        }
        
        public void SendPrivateMsg(string from, string to, string content) {
            base.Channel.SendPrivateMsg(from, to, content);
        }
        
        public System.Threading.Tasks.Task SendPrivateMsgAsync(string from, string to, string content) {
            return base.Channel.SendPrivateMsgAsync(from, to, content);
        }
        
        public void PrivateChat(string userName, string opponentName) {
            base.Channel.PrivateChat(userName, opponentName);
        }
        
        public System.Threading.Tasks.Task PrivateChatAsync(string userName, string opponentName) {
            return base.Channel.PrivateChatAsync(userName, opponentName);
        }
        
        public void ExitPrivateChat(string userName, string opponentName) {
            base.Channel.ExitPrivateChat(userName, opponentName);
        }
        
        public System.Threading.Tasks.Task ExitPrivateChatAsync(string userName, string opponentName) {
            return base.Channel.ExitPrivateChatAsync(userName, opponentName);
        }
        
        public void ConfirmationPrivateChat(string userName, string opponentName) {
            base.Channel.ConfirmationPrivateChat(userName, opponentName);
        }
        
        public System.Threading.Tasks.Task ConfirmationPrivateChatAsync(string userName, string opponentName) {
            return base.Channel.ConfirmationPrivateChatAsync(userName, opponentName);
        }
        
        public void StartGame(string userName, string opponentName, bool master) {
            base.Channel.StartGame(userName, opponentName, master);
        }
        
        public System.Threading.Tasks.Task StartGameAsync(string userName, string opponentName, bool master) {
            return base.Channel.StartGameAsync(userName, opponentName, master);
        }
        
        public void StartAutoPlayer(string userName) {
            base.Channel.StartAutoPlayer(userName);
        }
        
        public System.Threading.Tasks.Task StartAutoPlayerAsync(string userName) {
            return base.Channel.StartAutoPlayerAsync(userName);
        }
        
        public void RequestGame(string userName, string opponentName) {
            base.Channel.RequestGame(userName, opponentName);
        }
        
        public System.Threading.Tasks.Task RequestGameAsync(string userName, string opponentName) {
            return base.Channel.RequestGameAsync(userName, opponentName);
        }
        
        public void GameOver(string userName, string opponentName) {
            base.Channel.GameOver(userName, opponentName);
        }
        
        public System.Threading.Tasks.Task GameOverAsync(string userName, string opponentName) {
            return base.Channel.GameOverAsync(userName, opponentName);
        }
        
        public void UserLeftGame(string userName, string opponentName) {
            base.Channel.UserLeftGame(userName, opponentName);
        }
        
        public System.Threading.Tasks.Task UserLeftGameAsync(string userName, string opponentName) {
            return base.Channel.UserLeftGameAsync(userName, opponentName);
        }
        
        public void Roll(string user, string opponent) {
            base.Channel.Roll(user, opponent);
        }
        
        public System.Threading.Tasks.Task RollAsync(string user, string opponent) {
            return base.Channel.RollAsync(user, opponent);
        }
        
        public void RollEnable(string user, string opponent) {
            base.Channel.RollEnable(user, opponent);
        }
        
        public System.Threading.Tasks.Task RollEnableAsync(string user, string opponent) {
            return base.Channel.RollEnableAsync(user, opponent);
        }
        
        public void RollDisable(string user, string opponent) {
            base.Channel.RollDisable(user, opponent);
        }
        
        public System.Threading.Tasks.Task RollDisableAsync(string user, string opponent) {
            return base.Channel.RollDisableAsync(user, opponent);
        }
        
        public void SetMaster(string user, string opponent) {
            base.Channel.SetMaster(user, opponent);
        }
        
        public System.Threading.Tasks.Task SetMasterAsync(string user, string opponent) {
            return base.Channel.SetMasterAsync(user, opponent);
        }
        
        public void GameInfoUpdate(string user, string opponent, string content) {
            base.Channel.GameInfoUpdate(user, opponent, content);
        }
        
        public System.Threading.Tasks.Task GameInfoUpdateAsync(string user, string opponent, string content) {
            return base.Channel.GameInfoUpdateAsync(user, opponent, content);
        }
        
        public void SetWaiter(string user, string opponent) {
            base.Channel.SetWaiter(user, opponent);
        }
        
        public System.Threading.Tasks.Task SetWaiterAsync(string user, string opponent) {
            return base.Channel.SetWaiterAsync(user, opponent);
        }
        
        public void MoveChecker(string user, string opponent, int currIndex, int number) {
            base.Channel.MoveChecker(user, opponent, currIndex, number);
        }
        
        public System.Threading.Tasks.Task MoveCheckerAsync(string user, string opponent, int currIndex, int number) {
            return base.Channel.MoveCheckerAsync(user, opponent, currIndex, number);
        }
        
        public void MoveCheckerFromBar(string user, string opponent, int currIndex, int number) {
            base.Channel.MoveCheckerFromBar(user, opponent, currIndex, number);
        }
        
        public System.Threading.Tasks.Task MoveCheckerFromBarAsync(string user, string opponent, int currIndex, int number) {
            return base.Channel.MoveCheckerFromBarAsync(user, opponent, currIndex, number);
        }
        
        public void GetGameTable(string user, string opponent) {
            base.Channel.GetGameTable(user, opponent);
        }
        
        public System.Threading.Tasks.Task GetGameTableAsync(string user, string opponent) {
            return base.Channel.GetGameTableAsync(user, opponent);
        }
        
        public void RollHowFirst(string user, string opponent) {
            base.Channel.RollHowFirst(user, opponent);
        }
        
        public System.Threading.Tasks.Task RollHowFirstAsync(string user, string opponent) {
            return base.Channel.RollHowFirstAsync(user, opponent);
        }
        
        public void StartHowToFirst(string userName, string opponentName) {
            base.Channel.StartHowToFirst(userName, opponentName);
        }
        
        public System.Threading.Tasks.Task StartHowToFirstAsync(string userName, string opponentName) {
            return base.Channel.StartHowToFirstAsync(userName, opponentName);
        }
        
        public void CloseHowToFirst(string userName, string opponentName) {
            base.Channel.CloseHowToFirst(userName, opponentName);
        }
        
        public System.Threading.Tasks.Task CloseHowToFirstAsync(string userName, string opponentName) {
            return base.Channel.CloseHowToFirstAsync(userName, opponentName);
        }
        
        public void BackToChat(string user, string opponent) {
            base.Channel.BackToChat(user, opponent);
        }
        
        public System.Threading.Tasks.Task BackToChatAsync(string user, string opponent) {
            return base.Channel.BackToChatAsync(user, opponent);
        }
    }
}
