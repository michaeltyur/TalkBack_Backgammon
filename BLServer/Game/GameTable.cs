using Common.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BLServer.Game
{
    public class GameTable
    {

        private static readonly object padlock = new object();
        //Checkers collection
        public List<ObservableCollection<Checker>> ListAllCollections { get; set; }
        //List Of Bar Collection
        public List<ObservableCollection<Checker>> ListBarCollections { get; set; }

        //Bars
        public ObservableCollection<Checker> WhiteBar { get; set; }
        public ObservableCollection<Checker> BlackBar { get; set; }

        public int WhiteCounter;
        public int BlackCounter;

        //Colors
        public Brush Black { get; set; }
        public Brush White { get; set; }
        public Brush Selected { get; set; }

        public GameTable()
        {
            lock (padlock)
            {
                ListAllCollections = new List<ObservableCollection<Checker>>();
                ListBarCollections = new List<ObservableCollection<Checker>>();

                Black = new SolidColorBrush(Colors.Black);
                White = new SolidColorBrush(Colors.Moccasin);
                Selected = new SolidColorBrush(Colors.Brown);
                SetCollection();

                CheckerStartPosition();
                //TestCheckerStartPosition();

                WhiteBar = ListBarCollections[0];
                BlackBar = ListBarCollections[1];

            }
            WhiteCounter = 15;
            BlackCounter = 15;

        }

        private void SetCollection()
        {
            //Checkers collection
            for (int i = 0; i < 24; i++)
            {
                ListAllCollections.Add(new ObservableCollection<Checker>());
            }

            for (int i = 0; i < 2; i++)
            {
                ListBarCollections.Add(new ObservableCollection<Checker>());
            }
        }

        //Start Position
        public void CheckerStartPosition()
        {
            //5 checkers
            for (int i = 0; i < 5; i++)
            {
                ListAllCollections[5].Add(new Checker { Color = Black });
                ListAllCollections[12].Add(new Checker { Color = Black });

                ListAllCollections[11].Add(new Checker { Color = White });
                ListAllCollections[18].Add(new Checker { Color = White });
            }
            //3 checkers 
            for (int i = 0; i < 3; i++)
            {
                ListAllCollections[16].Add(new Checker { Color = White });
                ListAllCollections[7].Add(new Checker { Color = Black });
            }
            //2 checkers
            for (int i = 0; i < 2; i++)
            {
                ListAllCollections[0].Add(new Checker { Color = White });

                ListAllCollections[23].Add(new Checker { Color = Black });
            }
        }

        public void TestCheckerStartPosition()
        {

            ListAllCollections[0].Add(new Checker { Color = Black });
            ListAllCollections[0].Add(new Checker { Color = Black });

            //ListAllCollections[1].Add(new Checker { Color = Black });
            //ListAllCollections[1].Add(new Checker { Color = Black });

            //ListAllCollections[2].Add(new Checker { Color = Black });
            //ListAllCollections[2].Add(new Checker { Color = Black });

            //ListAllCollections[3].Add(new Checker { Color = Black });
            //ListAllCollections[3].Add(new Checker { Color = Black });

            //ListAllCollections[4].Add(new Checker { Color = Black });
            //ListAllCollections[4].Add(new Checker { Color = Black });

            //ListAllCollections[5].Add(new Checker { Color = Black });
            //ListAllCollections[5].Add(new Checker { Color = Black });

            //ListAllCollections[6].Add(new Checker { Color = Black });
            //ListAllCollections[6].Add(new Checker { Color = Black });


            ListAllCollections[16].Add(new Checker { Color = White });
            ListAllCollections[16].Add(new Checker { Color = White });

            ListAllCollections[17].Add(new Checker { Color = White });
            ListAllCollections[17].Add(new Checker { Color = White });

            ListAllCollections[18].Add(new Checker { Color = White });
            ListAllCollections[18].Add(new Checker { Color = White });

            ListAllCollections[19].Add(new Checker { Color = White });
            ListAllCollections[19].Add(new Checker { Color = White });

            ListAllCollections[20].Add(new Checker { Color = White });
            ListAllCollections[20].Add(new Checker { Color = White });

            ListAllCollections[21].Add(new Checker { Color = White });
            ListAllCollections[21].Add(new Checker { Color = White });

            ListAllCollections[22].Add(new Checker { Color = White });
            ListAllCollections[22].Add(new Checker { Color = White });

            ListAllCollections[23].Add(new Checker { Color = Black });
            ListAllCollections[23].Add(new Checker { Color = Black });
        }

        /// <summary>
        /// Returns array contains white checkers positions
        /// </summary>
        /// <returns></returns>
        public int[] GetWhiteCollArray()
        {
            int[] array = new int[24];
            var listCollectons = ListAllCollections;
            for (int i = 0; i < listCollectons.Count; i++)
            {
                var collection = listCollectons[i];
                if (collection.Count > 0 && collection.First().Color == White)
                {
                    array[i] = collection.Count;
                }
            }
            return array;
        }

        /// <summary>
        /// Returns array contains black checkers positions
        /// </summary>
        /// <returns></returns>
        public int[] GetBlackCollArray()
        {
            int[] array = new int[24];
            var listCollectons = ListAllCollections;
            for (int i = 0; i < listCollectons.Count; i++)
            {
                var collection = listCollectons[i];
                if (collection.Count > 0 && collection.First().Color == Black)
                {
                    array[i] = collection.Count;
                }
            }
            return array;
        }

        /// <summary>
        /// Returns array contains bar checkers positions
        /// </summary>
        /// <returns></returns>
        public int[] GetBarCollArray()
        {
            int[] array = new int[2];
            var listBarectons = ListBarCollections;
            for (int i = 0; i < listBarectons.Count; i++)
            {
                var collection = listBarectons[i];

                array[i] = collection.Count;

            }
            return array;
        }

        public int[][] GetGameTableForSend()
        {
            var bigArray = new int[3][];

            bigArray[0] = GetWhiteCollArray();
            bigArray[1] = GetBlackCollArray();
            bigArray[2] = GetBarCollArray();
            return bigArray;
        }
        public bool IsGameOver()
        {
            if (WhiteCounter == 0 || BlackCounter == 0)
                return true;
            else return false;
        }
    }

}
