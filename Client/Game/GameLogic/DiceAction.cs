using Client.Game.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Game.GameLogic
{
    public class DiceAction
    {
        private Backgammon _game;

        public DiceAction(Backgammon game)
        {
            _game = game;
        }
        public void SetMaster()
        {
            _game.Master = true;
            _game.rollButton.IsEnabled = true;
            _game.checkBoxLeftDice.IsEnabled = false;
            _game.checkBoxRightDice.IsEnabled = false;
            if (_game.GameInfo != null)
            {
                _game.GameInfo.Info = "You Roll";
            }
        }

        public void SetWaiter()
        {
            _game.Master = false;
            if (_game.rollButton != null) _game.rollButton.IsEnabled = false;
            if (_game.moveButton != null) _game.moveButton.IsEnabled = false;
            if (_game.checkBoxLeftDice != null)
            {
                _game.checkBoxLeftDice.IsEnabled = false;
                _game.checkBoxLeftDice.IsChecked = false;
            }
            if (_game.checkBoxRightDice != null)
            {
                _game.checkBoxRightDice.IsEnabled = false;
                _game.checkBoxRightDice.IsChecked = false;
            }
            if (_game.GameInfo != null)
            {
                _game.GameInfo.Info = "Wait to Roll";
            }

        }

        public void RollEnable()
        {
            _game.rollButton.IsEnabled = true;
        }

        public void RollDisable()
        {
            //Timer.Start();
            _game.rollButton.IsEnabled = false;
        }

        public void Roll(int[] numbers)
        {
            if (numbers[0] > 0 && numbers[0] < 7)
            {
                _game.Dice1.Image = Dice.GetDiceImage(numbers[0]);
                _game.Dice1.Number = numbers[0];
            }
            if (numbers[1] > 0 && numbers[1] < 7)
            {
                _game.Dice2.Image = Dice.GetDiceImage(numbers[1]);
                _game.Dice2.Number = numbers[1];
            }

        }
    }
}
