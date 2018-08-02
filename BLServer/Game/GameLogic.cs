using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLServer.Game
{

    public class GameLogic
    {
        private CheckersMoovingLogic _checkersMooving;
        private GameTable _gameTable;


        public GameLogic(GameTable gameTable)
        {
            _gameTable = gameTable;
            _checkersMooving = new CheckersMoovingLogic(_gameTable);
        }

        /// <summary>
        /// Moves checkers if this is posible end returns array represents checkers positions
        /// </summary>
        /// <param name="selectedIndexColl">index of collection that need to moving</param>
        /// <param name="futureIndexColl">index of destination collection</param>
        /// <param name="white">color of checkers</param>
        /// <returns></returns>
        public bool CheckersMoovingLogic(int selectedIndexColl, int number)
        {
            //if (selectedIndexColl > _gameTable.ListAllCollections.Count)
            //    Debugger.Break();
            try
            {
                var result = _checkersMooving.MoveCheckerLogic(selectedIndexColl, number);

                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool CheckersMoovingFromBarLogic(int selectedIndexColl, int number)
        {
            var bigArray = new int[3][];
            try
            {
                var result = _checkersMooving.MoveBarCheckerLogic(selectedIndexColl, number);

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}

