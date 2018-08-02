using Client.Game.GameData;
using System.Collections.ObjectModel;

namespace Client.Game
{
    /// <summary>
    /// Contains methods for performing basic logic functions of the checkers.
    /// </summary>
    public class СheckerLogic
    {
        private Backgammon backgammon;

        public СheckerLogic(Backgammon backgammon)
        {
            this.backgammon = backgammon;
        }

        //Set Color to selected checker
        public void CollSelected(ObservableCollection<Checker> collection)
        {
            if (collection != null && collection.Count > 0 && backgammon != null
                && collection[0].Color == backgammon.MyCheckerColor)//Just for my checkers
            {
                CollUnSelected();

                backgammon.IndexCollSelected = backgammon.ListAllCollections.IndexOf(collection);

                if (backgammon.MyCheckerColor == backgammon.White)//for White sector
                {
                    if (backgammon.IndexCollSelected >= 0 && backgammon.IndexCollSelected < 12)
                    {
                        if (collection.Count < 5)
                            collection[collection.Count - 1] = new Checker { Color = backgammon.Selected };
                        else collection[4] = new Checker { Color = backgammon.Selected };
                    }
                    else if (backgammon.IndexCollSelected >= 12 && backgammon.IndexCollSelected < 24)
                    {
                        if (collection.Count < 5)
                            collection[0] = new Checker { Color = backgammon.Selected };
                        else collection[collection.Count - 5] = new Checker { Color = backgammon.Selected };
                    }
                }
                else//For Black sector
                {
                    if (backgammon.IndexCollSelected >= 0 && backgammon.IndexCollSelected < 12)
                    {
                        if (collection.Count < 5)
                            collection[0] = new Checker { Color = backgammon.Selected };
                        else collection[collection.Count - 5] = new Checker { Color = backgammon.Selected };
                    }
                    else if (backgammon.IndexCollSelected >= 12 && backgammon.IndexCollSelected < 24)
                    {
                        if (collection.Count < 5)
                            collection[collection.Count - 1] = new Checker { Color = backgammon.Selected };
                        else collection[4] = new Checker { Color = backgammon.Selected };
                    }
                }
                //Selection for White Bar
                if (backgammon.IndexCollSelected == -1)
                {
                    if (backgammon.MyCheckerColor == backgammon.White)
                    {
                        collection[0] = new Checker { Color = backgammon.Selected };
                    }
                    else collection[0] = new Checker { Color = backgammon.Selected };
                }
            }
        }

        //Unselect the checker
        public void CollUnSelected()
        {
            var listAllColl = backgammon.ListAllCollections;

            foreach (var listOneColl in listAllColl)
            {
                for (int i = 0; i < listOneColl.Count; i++)
                {

                    if (backgammon.MyCheckerColor == backgammon.White)
                    {
                        if (listOneColl[i].Color != backgammon.Black)
                        {
                            listOneColl[i] = new Checker { Color = backgammon.MyCheckerColor };
                        }
                    }
                    else
                    {
                        if (listOneColl[i].Color != backgammon.White)
                        {

                            listOneColl[i] = new Checker { Color = backgammon.MyCheckerColor };
                        }
                    }
                }
            }
            //On Bar
            var listBarColl = backgammon.ListBarCollections;

            foreach (var listOneColl in listBarColl)
            {
                for (int i = 0; i < listOneColl.Count; i++)
                {
                    if (backgammon.MyCheckerColor == backgammon.White)
                    {
                        if (listOneColl[i].Color != backgammon.Black)
                        {
                            listOneColl[i] = new Checker { Color = backgammon.MyCheckerColor };
                        }
                    }
                    else
                    {
                        if (listOneColl[i].Color != backgammon.White)
                        {
                            listOneColl[i] = new Checker { Color = backgammon.MyCheckerColor };
                        }
                    }
                }
            }
            backgammon.IndexCollSelected = -5;
        }

        public void UpdateTable(int[] whiteCheckers, int[] blackCheckers, int[] barCheckers)
        {
            var listCollection = backgammon.ListAllCollections;
            var barCollection = backgammon.ListBarCollections;

            var whiteCheckerCounter = 0;
            var blackCheckerCounter = 0;

            if (whiteCheckers != null && blackCheckers != null && barCheckers != null)
            {
                ClearCollections();

                //Table
                for (int i = 0; i < listCollection.Count; i++)
                {
                    var collection = listCollection[i];
                    if (whiteCheckers[i] > 0)
                    {
                        for (int j = 0; j < whiteCheckers[i]; j++)
                        {
                            collection.Add(new Checker { Color = backgammon.White });
                            whiteCheckerCounter++;
                        }
                    }
                    else if (blackCheckers[i] > 0)
                    {
                        for (int j = 0; j < blackCheckers[i]; j++)
                        {
                            collection.Add(new Checker { Color = backgammon.Black });
                            blackCheckerCounter++;
                        }
                    }
                }
                //White Bar
                var whiteBar = backgammon.ListBarCollections[0];
                for (int i = 0; i < barCheckers[0]; i++)
                {
                    whiteBar.Add(new Checker { Color = backgammon.White });
                }

                //Black Bar
                var blackBar = backgammon.ListBarCollections[1];
                for (int i = 0; i < barCheckers[1]; i++)
                {
                    blackBar.Add(new Checker { Color = backgammon.Black });
                }

                if (backgammon.MyCheckerColor == backgammon.White)
                {
                    backgammon.BarGameInfo.TopCounter = whiteBar.Count;
                    backgammon.BarGameInfo.OutCounter = 15 - (whiteCheckerCounter + whiteBar.Count);
                }
                else
                {
                    backgammon.BarGameInfo.TopCounter = blackBar.Count;
                    backgammon.BarGameInfo.OutCounter = 15 - (blackCheckerCounter + blackBar.Count);
                }
            }
        }

        private void ClearCollections()
        {
            var listCollection = backgammon.ListAllCollections;
            var barCollection = backgammon.ListBarCollections;

            foreach (var collection in listCollection)
            {
                collection.Clear();
            }
            foreach (var collection in barCollection)
            {
                collection.Clear();
            }
        }

        public void GameOver(string opponent, bool winner)
        {

            GameFinish.GameOver gameOver = new GameFinish.GameOver(winner);

            backgammon.resignButton.IsEnabled = false;
            backgammon.rollButton.IsEnabled = false;
            backgammon.checkBoxLeftDice.IsEnabled = false;
            backgammon.checkBoxRightDice.IsEnabled = false;
            if (winner)
            {
                backgammon.GameInfo.Info = "You are winner !!!";
            }
            else backgammon.GameInfo.Info = "You are loser :(";
            backgammon.Timer.Start();
        }

        public void MoveEnable()
        {
            backgammon.moveButton.IsEnabled = true;
            backgammon.rollButton.IsEnabled = false;
            if (backgammon.checkBoxLeftDice.IsChecked == true) backgammon.checkBoxLeftDice.IsEnabled = true;
            if (backgammon.checkBoxRightDice.IsChecked == true) backgammon.checkBoxRightDice.IsEnabled = true;
        }

        public void ClearSelections()
        {
           backgammon.checkBoxLeftDice.IsChecked = false;
            backgammon.checkBoxRightDice.IsChecked = false;
            backgammon.CheckerLogic.CollUnSelected();
        }
    }

}
