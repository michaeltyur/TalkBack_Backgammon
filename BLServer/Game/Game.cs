using BLServer.Game.AutoPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLServer.Game
{
    public class Game
    {
        public string WhiteUser { get; }
        public string BlackUser { get; }
        public GameTable GameTable { get; }
        public GameLogic GameLogic { get;  }
        public DiceLogic DiceLogic { get; }
        public AutoPlayerLogic AutoPlayer { get; }
        public DoubleDice DoubleDice { get; set; }
        public DifferentDice DifferentDice { get; set; }


        public Game(string whiteUser,string blackUser,Random random)
        {
            WhiteUser = whiteUser;
            BlackUser = blackUser;
            GameTable = new GameTable();
            GameLogic = new GameLogic(GameTable);
            DiceLogic = new DiceLogic(random);

            if(whiteUser==Player.autoPlayer.ToString())
            {
                AutoPlayer = new AutoPlayerLogic(GameTable,GameLogic,DiceLogic,true);
            }
            else if(blackUser == Player.autoPlayer.ToString())
            {
                AutoPlayer = new AutoPlayerLogic(GameTable, GameLogic, DiceLogic, false);
            }

        }
    }
}
