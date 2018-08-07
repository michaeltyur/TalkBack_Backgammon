using Client.Game;
using Client.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Client
{
    public class ClientInstances
    {
        //Singleton Instance
        private static ClientInstances _instance = null;
        private static readonly object padlock = new object();

        private DispatcherTimer TimeTimer;
        public Time Time { get; }
        private bool second = false;

        public Login Login { get; set; }

        public Registration Registration { get; set; }

        public Chat Chat { get; set; }


        public TalkBackService.IChatService ChatHost { get; }
        public TalkBackService.IGameService GameHost { get; }


        public Dictionary<string, PrivateChat> PrivateChats;
        public Dictionary<string, Backgammon> GameList;
        public Dictionary<string, HowIsFirst> HowFirstsList; 

        private ClientInstances()
        {
            var callback = new ClientCallback();

            ChatHost = new TalkBackService.ChatServiceClient(new System.ServiceModel.InstanceContext(callback));
            GameHost = new TalkBackService.GameServiceClient(new System.ServiceModel.InstanceContext(callback));

            PrivateChats = new Dictionary<string, PrivateChat>();
            GameList = new Dictionary<string, Backgammon>();
            HowFirstsList = new Dictionary<string, HowIsFirst>();
            Time = new Time();
            TimeTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)

            };
            TimeTimer.Tick += TimeTimer_Tick;
            TimeTimer.Start();
        }

        //Get Singleton Instance
        public static ClientInstances Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (padlock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ClientInstances();
                        }                        
                    }
                }
                return _instance;
            }
        }

        private void TimeTimer_Tick(object sender, EventArgs e)
        {
            second = !second;
            Time.Hour = DateTime.Now.TimeOfDay.Hours.ToString();
            var minutes= DateTime.Now.TimeOfDay.Minutes;
            if (second)
            {
                if (minutes / 10 == 0)
                {
                    Time.Minutes = "0" + minutes.ToString();
                }
                else Time.Minutes = minutes.ToString();
                Time.Seconds = ":";
            }
            else Time.Seconds = string.Empty;
        }

        public void StartChat(string user)
        {
            Chat = new Chat(user);
        }
    }
   public enum Player
    {
        singlePlayer=1,
        autoPlayer
    }

}
