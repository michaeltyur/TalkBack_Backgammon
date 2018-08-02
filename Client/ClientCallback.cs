using Client.Game;
using Client.Game.GameLogic;
using Client.Windows;
using Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Сontains methods that send and receive information from the server.
    /// </summary>
    public class ClientCallback : ChatServer.IChatServiceCallback
    {
        #region Errors
        //public void ErrorUserAlreadyOnline(string error)
        //{
        //    MessageBoxResult messageBoxResult = MessageBox.Show(error, "Error!");

        //    if (ClientInstances.Instance.Login != null)
        //    {
        //        ClientInstances.Instance.Login.ButtonsOnOff();
        //    }
        //}

        public void RecieveError(string error)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(error, "Error!");

            if (ClientInstances.Instance.Login != null)
            {
                ClientInstances.Instance.Login.ButtonsOnOff();
            }
            else if (ClientInstances.Instance.Registration != null)
            {
                ClientInstances.Instance.Registration.ButtonsOnOff();
            }
        }

        public void RecieveGameError(string error)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(error, "Error!");
        }
        #endregion

        #region Private Chat
        public void PrivateChatSlose(string opponentName)
        {           
            if ( ClientInstances.Instance.PrivateChats.ContainsKey(opponentName))
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"User {opponentName} is exited. The chat will close.");

                //ClientInstances.Instance.Chat.EndPrivateChat(opponentName);
                ClientInstances.Instance.PrivateChats[opponentName].Close();
                ClientInstances.Instance.PrivateChats.Remove(opponentName);
                ClientInstances.Instance.Chat.UpdateUserList();
            }
        }

        public void PrivateChatStart(string userName, string opponentName)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show($"Do you want to start private chat with {opponentName} ?", "Private Chat Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                //ClientInstances.Instance.StartChat(userName);
                //ClientInstances.Instance.Chat.StartPrivateChat(opponentName);
                if (!ClientInstances.Instance.PrivateChats.ContainsKey(opponentName))
                {
                    ClientInstances.Instance.PrivateChats.Add(opponentName, new PrivateChat(userName, opponentName));
                }
                ClientInstances.Instance.Chat.Host.ConfirmationPrivateChatAsync(userName, opponentName);
                ClientInstances.Instance.Chat.UpdateUserList();
            }
            else ClientInstances.Instance.Chat.Host.SendErrorMessage(opponentName, $"The user {userName} not want to chat with you");
        }

        public void PrivateChatStartFromOpponent(string userName, string opponentName)
        {
            if (!ClientInstances.Instance.PrivateChats.ContainsKey(opponentName))
            {
                ClientInstances.Instance.PrivateChats.Add(opponentName,new PrivateChat(userName, opponentName));
                ClientInstances.Instance.Chat.UpdateUserList();
            }            
        }

        //Recieve Private Message
        public void RecievePrivateMsg(string from, string key, string content)
        {
            var user = ClientInstances.Instance.Chat.User;

            if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(content))
            {
                if (ClientInstances.Instance.PrivateChats.ContainsKey(key))
                {
                    ClientInstances.Instance.PrivateChats[key].RecieveMsg(from, content);
                }
            }
        }
        #endregion

        #region Message

        public void RecieveMessage(string userName, string content)
        {
           ClientInstances.Instance.Chat.RecieveMessage(userName, content);
        }

        #endregion

        #region Authentication

        public void ResultActionfromLogoin(string userName)
        {
            ClientInstances.Instance.StartChat(userName);
           if (ClientInstances.Instance.Login != null) ClientInstances.Instance.Login.CloseLoginWindow();
           if(ClientInstances.Instance.Registration!=null) ClientInstances.Instance.Registration.CloseRegWindow();
        }

        public void ResultActionfromLogout()
        {
            ClientInstances.Instance.Chat.Close();
            ClientInstances.Instance.Login = new Login();
            ClientInstances.Instance.Login.Show();
        }

        public void UsersOnLine([MessageParameter(Name = "usersOnLine")] string[] usersOnLine1, string[] usersOffLine)
        {
            ClientInstances.Instance.Chat.UpdateUser(usersOnLine1.ToList(), usersOffLine.ToList());
        }
        #endregion

        #region Game
        //Game
        public void StartGameFromServer(string user, string opponent, bool master)
        {
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(opponent))
            {
                if (!ClientInstances.Instance.GameList.ContainsKey(opponent))
                {
                    ClientInstances.Instance.GameList.Add(opponent, new Backgammon(user, opponent, master));
                }
                if (ClientInstances.Instance.Chat != null)
                    ClientInstances.Instance.Chat.UpdateUserList();
            }
        }

        //Request to start game
        public void RequestGameFromServer(string user, string opponent)
        {
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(opponent))
            {
                var host = ClientInstances.Instance.Chat.Host;

                MessageBoxResult messageBoxResult = MessageBox.Show($"Do you want to start the game with {opponent} ?", "Game Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    //if (ClientInstances.Instance.PrivateChats.ContainsKey(opponent))
                    //{
                    //    ClientInstances.Instance.PrivateChats[opponent]._gameButton.IsEnabled = false;
                    //}
                    if (!ClientInstances.Instance.HowFirstsList.ContainsKey(opponent))
                    {
                        host.StartHowToFirstAsync(user, opponent);
                        if (ClientInstances.Instance.Chat != null)
                            ClientInstances.Instance.Chat.UpdateUserList();
                    }
                    else messageBoxResult = MessageBox.Show($"You can't start same game again", "Warning!!!");
                }
                else ClientInstances.Instance.Chat.Host.SendErrorMessage(opponent, $"The user {user} not want to game with you");
            }

        }

        //Game Operations
        public void RecieveRoll(string opponent, int[] numbers)
        {
            if (ClientInstances.Instance.GameList.ContainsKey(opponent))
            {
                ClientInstances.Instance.GameList[opponent].DiceAction.Roll(numbers); ;
            }

        }

        public void RollEnableFromServer(string user, string opponentName)
        {
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(opponentName))
            {
                if (ClientInstances.Instance.GameList.ContainsKey(opponentName))
                {
                    ClientInstances.Instance.GameList[opponentName].DiceAction.RollEnable();
                }
            }
        }

        //Disable Roll Button from Server
        public void RollDisableFromServer(string user, string opponentName)
        {
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(opponentName))
            {

                if (ClientInstances.Instance.GameList.ContainsKey(opponentName))
                {
                    var game = ClientInstances.Instance.GameList[opponentName];

                    if (game != null)
                    {
                        game.DiceAction.RollDisable();
                    }
                };
            }
        }

        //Update current info fo user
        public void GameInfoUpdateFromServer(string opponentName, string content)
        {
            if (!string.IsNullOrEmpty(opponentName) && !string.IsNullOrEmpty(content))
            {
                if (ClientInstances.Instance.GameList.ContainsKey(opponentName))
                    ClientInstances.Instance.GameList[opponentName].GameInfoUpdate(content);

                if (ClientInstances.Instance.HowFirstsList.ContainsKey(opponentName))
                    ClientInstances.Instance.HowFirstsList[opponentName].GameInfoUpdate(content);
            }
        }

        public void SetMasterFromServer(string user, string opponentName)
        {
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(opponentName))
            {

                if (ClientInstances.Instance.GameList.ContainsKey(opponentName))
                {
                    ClientInstances.Instance.GameList[opponentName].DiceAction.SetMaster();
                }
            }
        }

        public void SetWaiterFromServer(string user, string opponentName)
        {
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(opponentName))
            {
                if (ClientInstances.Instance.GameList.ContainsKey(opponentName))
                {
                    ClientInstances.Instance.GameList[opponentName].DiceAction.SetWaiter();
                }
            }
        }

        public void MoveCheckerFromServer(string user, string opponent, int currIndex, int futherIndex, bool IsbeatPiece)
        {
            //if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(opponent))
            //{
            //    var game = ClientInstances.Instance.Chat.GetGameInstance(opponent);
            //    if (game != null)
            //    {
            //        var checkerLogic = new СheckerLogic(game);

            //        if (IsbeatPiece)
            //        {
            //            checkerLogic.BeatAPiece(currIndex, futherIndex);
            //        }
            //        else checkerLogic.MoveChecker(currIndex, futherIndex, false);
            //    }
            //}
        }

        public void UpdateTable(string user, string opponent, int[] whiteCheckers, int[] blackCheckers, int[] barCheckers)
        {
            if (whiteCheckers != null && blackCheckers != null && barCheckers != null &&
                !string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(opponent))
            {
                if(ClientInstances.Instance.GameList.ContainsKey(opponent))
                {
                    ClientInstances.Instance.GameList[opponent].CheckerLogic.UpdateTable(whiteCheckers, blackCheckers, barCheckers);
                }
            }
        }

        public void GameOverFromServer(string userName, string opponentName, bool winner)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(opponentName))
            {
                if (ClientInstances.Instance.GameList.ContainsKey(opponentName))
                {
                    ClientInstances.Instance.GameList[opponentName].CheckerLogic.GameOver(opponentName, winner);
                }
            }
        }

        public void MoveEnableFromServer(string user, string opponent)
        {
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(opponent))
            {
                if (ClientInstances.Instance.GameList.ContainsKey(opponent))
                {
                    ClientInstances.Instance.GameList[opponent].CheckerLogic.MoveEnable();
                }
            }
        }
        #endregion

        #region How To First
        public void RecieveRollHowFirst(string user, string opponent, int[] numbers)
        {
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(opponent))
            {
                if (ClientInstances.Instance.HowFirstsList.ContainsKey(opponent))
                {
                    ClientInstances.Instance.HowFirstsList[opponent].RecieveRoll(numbers);
                }
            }
        }

        public void StartHowToFirstFromServer(string user, string opponent)
        {
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(opponent))
            {
                if (ClientInstances.Instance.PrivateChats.ContainsKey(opponent))
                {
                    ClientInstances.Instance.PrivateChats[opponent].gameButton.IsEnabled = false;
                }
                if (!ClientInstances.Instance.HowFirstsList.ContainsKey(opponent))
                {
                    ClientInstances.Instance.HowFirstsList.Add(opponent, new HowIsFirst(user, opponent));
                }
            }
        }

        public void CloseHowToFirstFromServer(string user, string opponent, bool winner)
        {
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(opponent))
            {
                if (ClientInstances.Instance.HowFirstsList.ContainsKey(opponent))
                {
                    ClientInstances.Instance.HowFirstsList[opponent].HowFirstClose(winner);
                    ClientInstances.Instance.HowFirstsList.Remove(opponent);
                }
            }
        }
        #endregion

        public void BackToChatFromServer(string user, string opponent)
        {
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(opponent))
            {
                if (ClientInstances.Instance.HowFirstsList.ContainsKey(opponent))
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show($"The user {opponent} did left. The game will close.", "Warning!!!");
                    ClientInstances.Instance.HowFirstsList[opponent].HowFirstClose();
                }
                if (ClientInstances.Instance.GameList.ContainsKey(opponent))
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show($"The user {opponent} did left. The game will close.", "Warning!!!");
                    ClientInstances.Instance.GameList[opponent].CloseGame();
                }
                if (ClientInstances.Instance.PrivateChats.ContainsKey(opponent))
                {
                    ClientInstances.Instance.PrivateChats[opponent].gameButton.IsEnabled = true;                   
                }
                if(ClientInstances.Instance.Chat!=null)
                    ClientInstances.Instance.Chat.UpdateUserList();
            }
        }
    }
}

