using Common.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BLServer.Game
{
    public class CheckersMoovingLogic
    {
        private GameTable _gameTable;

        public CheckersMoovingLogic(GameTable gameTable)
        {
            _gameTable = gameTable;
        }

        public bool MoveCheckerLogic(int selectedIndexColl, int number)
        {

            if (selectedIndexColl < -2 || selectedIndexColl > 23 || number < 0 && number > 6)
                return false;

            var listCollectons = _gameTable.ListAllCollections;

            var futureIndexColl = FutureCollIndex(selectedIndexColl, number);

            if (futureIndexColl >= 0 && futureIndexColl < 24)
            {
                var futureColl = listCollectons[futureIndexColl];
            }
            IsCanMoveResult canMove;
            try
            {
                canMove = IsCanMove(selectedIndexColl, futureIndexColl);

            }
            catch (Exception ex)
            {

                throw ex;
            }

            if (canMove == IsCanMoveResult.can_move_easy)
            {
                CheckerStandartMoving(selectedIndexColl, futureIndexColl);
                //UpdateGametable();
                return true;
            }
            else if (canMove == IsCanMoveResult.can_move_and_beat)
            {
                BeatAPieceAndMove(selectedIndexColl, futureIndexColl);
                //UpdateGametable();
                return true;
            }
            else if (canMove == IsCanMoveResult.can_move_out_white)//move out White Checker
            {
                MoveOutChecker(selectedIndexColl);
                _gameTable.WhiteCounter--;
                //UpdateGametable();
                return true;
            }
            else if (canMove == IsCanMoveResult.can_move_out_black)//move out Black Checker
            {
                MoveOutChecker(selectedIndexColl);
                _gameTable.BlackCounter--;
                //UpdateGametable();
                return true;
            }
            else return false;
        }

        public bool MoveBarCheckerLogic(int selectedIndexColl, int number)
        {
            if (selectedIndexColl >= 0 && selectedIndexColl < 3)
            {
                var listCollectons = _gameTable.ListAllCollections;
                var selectedColl = listCollectons[selectedIndexColl];

                var futuredIndex = (selectedIndexColl == 0) ? FutureCollIndex(-1, number) : FutureCollIndex(-2, number);

                var futureColl = listCollectons[futuredIndex];

                IsCanMoveResult canMove;
                try
                {
                    canMove = IsCanMoveFromBar(selectedIndexColl, futuredIndex);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                if (canMove == IsCanMoveResult.can_move_easy)//Retrieve Checker from Bar to Table
                {
                    RetrieveChecker(selectedIndexColl, futuredIndex);
                    return true;
                }
                else if (canMove == IsCanMoveResult.can_move_and_beat)
                {
                    RetrieveChecker(selectedIndexColl, futuredIndex);
                    return true;
                }
                else return false;
            }
            else return false;
        }

        /// <summary>
        /// Returns the number according the mooving situation
        /// </summary>
        /// <returns>
        /// 0 - checker can move standart
        /// 1 - checker can move and put out the checker of opponent
        /// 3 - white checker can exit out of table
        /// 4 - black checker can exit out of table
        /// 5 - white checker can exit from bar and return to table
        /// -5 - moving is imbosible
        /// </returns>
        private IsCanMoveResult IsCanMove(int selectedIndexColl, int futureIndexColl)
        {
            var startPosition = selectedIndexColl;
            var finishPosition = futureIndexColl;

            //Stack is full
            if (startPosition >= 0 && startPosition < 24 && finishPosition >= 0 && finishPosition < 24)
            {
                var listCollectons = _gameTable.ListAllCollections;

                var selectedColl = listCollectons[startPosition];
                var futureColl = listCollectons[finishPosition];

                if (selectedColl.Count == 0) return IsCanMoveResult.non;
                else if (futureColl.Count == 0 || futureColl.First().Color == selectedColl.First().Color)
                {
                    return IsCanMoveResult.can_move_easy;//future place empty 
                }
                else if (futureColl.Count >= 1 && futureColl.First().Color != selectedColl.First().Color)//Checkers of opponent
                {
                    if (futureColl.Count == 1)
                    {
                        return IsCanMoveResult.can_move_and_beat;//one checker of opponent
                    }
                    else throw new Exception("You can't move to this position. Opponent's checker more that one.");
                }
                return IsCanMoveResult.non;
            }
            //White checkers on bar
            else if (startPosition >= 0 && startPosition < 24 && (finishPosition >= 24 || finishPosition < 0))
            {
                var listCollectons = _gameTable.ListAllCollections;
                var selectedColl = listCollectons[selectedIndexColl];

                if (selectedColl.First().Color == _gameTable.White)//white checker
                {
                    if (_gameTable.WhiteBar.Count > 0)
                    {
                        throw new Exception("You can't move out the ckeckers. You have the checkers on the bar.");
                    }
                    else if (!IsMyCheckersAtHome(true))
                    {
                        throw new Exception("You can't move out the ckeckers. Not all your checkers is at home place.");
                    }
                    else return IsCanMoveResult.can_move_out_white;
                }
                else if (selectedColl.First().Color == _gameTable.Black)
                {
                    if (_gameTable.BlackBar.Count > 0)
                    {
                        //messageBoxResult = MessageBox.Show("You have the chekers on the bar\nPlease reuse that", "Atention!");
                        //CollUnSelected();
                        throw new Exception("You can't move out the ckeckers. You have the checkers on the bar.");
                    }
                    else if (!IsMyCheckersAtHome(false))
                    {
                        // messageBoxResult = MessageBox.Show("Not all your checkers is at home place", "Atention!");
                        throw new Exception("You can't move out the ckeckers. Not all your checkers is at home place.");
                    }
                    else return IsCanMoveResult.can_move_out_black;
                }
                else return IsCanMoveResult.non;
            }
            else return IsCanMoveResult.non; //moving is imposible

        }

        private IsCanMoveResult IsCanMoveFromBar(int selectedIndexColl, int futureIndexColl)
        {
            if (selectedIndexColl >= 0 && selectedIndexColl < 2 && futureIndexColl >= 0 && futureIndexColl < 24)
            {
                var selectedCollection = _gameTable.ListBarCollections[selectedIndexColl];
                var futureCollection = _gameTable.ListAllCollections[futureIndexColl];
                var myColor = selectedCollection.First().Color;

                if (futureCollection.Count == 0 || futureCollection.Count > 0 && futureCollection.First().Color == myColor)
                {
                    return IsCanMoveResult.can_move_easy;
                }
                else if (futureCollection.Count == 1 && futureCollection.First().Color != myColor)
                {
                    return IsCanMoveResult.can_move_and_beat;
                }
                else throw new Exception("You can't move to this position. Opponent's checker more that one.");
            }
            return IsCanMoveResult.non;
        }

        /// <summary>
        /// Returns calculated futured index of checker that mooving
        /// </summary>
        /// <returns></returns>
        private int FutureCollIndex(int selectedIndexColl, int number)
        {
            var _selectedIndexColl = selectedIndexColl;

            if (_selectedIndexColl >= -2 && number > 0 && number < 7)
            {
                var listCollectons = _gameTable.ListAllCollections;
                //var selectedColl = listCollectons[selectedIndexColl];
                bool white = false;

                if (_selectedIndexColl >= 0)
                {
                    if (listCollectons[_selectedIndexColl].Count > 0)
                    {
                        if (listCollectons[_selectedIndexColl].First().Color == _gameTable.White)
                            white = true;
                        else white = false;
                    }
                }
                else if (_selectedIndexColl == -1)
                {
                    white = true;
                    _selectedIndexColl = -1;
                }
                else if (_selectedIndexColl == -2)
                {
                    white = false;
                    _selectedIndexColl = -1;
                }

                var startPosition = _selectedIndexColl;
                var finishPosition = startPosition;

                if (startPosition >= 0)
                {
                    if (white) finishPosition = startPosition + number;
                    else finishPosition = startPosition - number;
                }
                else
                {
                    if (white) finishPosition = startPosition + number;
                    else finishPosition = 24 - number;
                }
                return finishPosition;
            }

            else return int.MinValue;
        }

        private void CheckerStandartMoving(int selectedIndexColl, int futureIndexColl)
        {
            if (selectedIndexColl >= 0 && selectedIndexColl < 24 && futureIndexColl >= 0 && futureIndexColl < 24)
            {
                var list = _gameTable.ListAllCollections;
                var selectedColl = list[selectedIndexColl];
                var futureColl = list[futureIndexColl];
                //Brush _color;

                //if (selectedColl.Count>0)
                Brush _color = selectedColl.First().Color;

                futureColl.Add(new Checker { Color = _color });
                if (selectedColl.Count > 0)
                {
                    selectedColl.Remove(selectedColl.First());
                }
            }
        }

        /// <summary>
        /// Moves the selected checher and remove to bar beated one
        /// </summary>
        /// <param name="selectedIndexColl"></param>
        /// <param name="futureIndexColl"></param>
        private void BeatAPieceAndMove(int selectedIndexColl, int futureIndexColl)
        {
            if (selectedIndexColl >= 0 && selectedIndexColl < 24 && futureIndexColl >= 0 && futureIndexColl < 24)
            {
                var list = _gameTable.ListAllCollections;
                var selectedColl = list[selectedIndexColl];
                var futureColl = list[futureIndexColl];
                var whiteBar = _gameTable.WhiteBar;
                var blackBar = _gameTable.BlackBar;

                var removedChecker = futureColl.First();
                //White removed
                if (removedChecker.Color == _gameTable.White)
                {
                    whiteBar.Add(removedChecker);
                }
                //Black removed
                else
                {
                    blackBar.Add(removedChecker);
                }

                futureColl[0] = selectedColl[0];

                selectedColl.Remove(selectedColl.First());
            }
        }

        private void MoveOutChecker(int selectedIndexColl)
        {
            if (selectedIndexColl >= 0 && selectedIndexColl < 24)
            {
                var list = _gameTable.ListAllCollections;
                var selectedColl = list[selectedIndexColl];

                if (selectedColl.Count > 0) selectedColl.Remove(selectedColl.First());
            }
        }

        private void RetrieveChecker(int selectedIndexColl, int futureIndexColl)
        {
            //selectedIndexColl - index of bar collection
            //futureIndexColl - index of list collections
            if (selectedIndexColl >= 0 && selectedIndexColl < 24 && futureIndexColl >= 0 && futureIndexColl < 24)
            {
                var barCollection = _gameTable.ListBarCollections[selectedIndexColl];
                var futureCollection = _gameTable.ListAllCollections[futureIndexColl];

                if (barCollection.Count > 0 && futureCollection.Count > 0)
                {
                    if (barCollection.First().Color != futureCollection.First().Color)
                    {
                        var color = futureCollection[0].Color;
                        futureCollection[0] = barCollection[0];
                        if (color == _gameTable.White)
                            _gameTable.ListBarCollections[0].Add(new Checker { Color = color });
                        else _gameTable.ListBarCollections[1].Add(new Checker { Color = color });
                    }
                    else
                    {
                        futureCollection.Add(barCollection.First());
                    }
                    barCollection.Remove(barCollection.First());
                }
                else if (barCollection.Count > 0 && futureCollection.Count == 0)
                {
                    futureCollection.Add(barCollection.First());
                    barCollection.Remove(barCollection.First());
                }
            }
        }

        //Check if all checkers are at home place
        private bool IsMyCheckersAtHome(bool whiteCheckers)
        {
            var allCollList = _gameTable.ListAllCollections;

            if (whiteCheckers)
            {
                var myColor = _gameTable.White;
                for (int i = 0; i < allCollList.Count; i++)
                {
                    var oneCol = allCollList[i];
                    foreach (var checher in oneCol)
                    {
                        if (i < 18)
                        {
                            if (checher.Color == myColor)
                                return false;
                        }
                    }
                }
                return true;
            }
            else
            {
                var myColor = _gameTable.Black;
                for (int i = 0; i < allCollList.Count; i++)
                {
                    var oneCol = allCollList[i];
                    foreach (var checher in oneCol)
                    {
                        if (i > 6)
                        {
                            if (checher.Color == myColor)
                                return false;
                        }
                    }

                }
                return true;
            }

        }

        //private void UpdateGametable()
        //{

        //    _gameTable.SetTable();
        //}
    }


    public enum IsCanMoveResult
    {
        non = 1,
        can_move_easy,
        can_move_and_beat,
        can_move_out_white,
        can_move_out_black
    }

}
