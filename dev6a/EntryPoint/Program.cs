using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EntryPoint
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            var fullscreen = false;
        read_input:
            switch (Microsoft.VisualBasic.Interaction.InputBox("Which assignment shall run next? (1, 2, 3, 4, or q for quit)", "Choose assignment", VirtualCity.GetInitialValue()))
            {
                case "1":
                    using (var game = VirtualCity.RunAssignment1(SortSpecialBuildingsByDistance, fullscreen))
                        game.Run();
                    break;
                case "2":
                    using (var game = VirtualCity.RunAssignment2(FindSpecialBuildingsWithinDistanceFromHouse, fullscreen))
                        game.Run();
                    break;
                case "3":
                    using (var game = VirtualCity.RunAssignment3(FindRoute, fullscreen))
                        game.Run();
                    break;
                case "4":
                    using (var game = VirtualCity.RunAssignment4(FindRoutesToAll, fullscreen))
                        game.Run();
                    break;
                case "q":
                    return;
            }
            goto read_input;
        }

        private static IEnumerable<Vector2> SortSpecialBuildingsByDistance(Vector2 house, IEnumerable<Vector2> specialBuildings)
        {
            System.Console.WriteLine("x and y: " + house.X + "," + house.Y);
            IEnumerator<Vector2> em = specialBuildings.GetEnumerator();
            int counter = 0;
            List<Vector2> specialB = specialBuildings.ToList();

            MergeSortList(specialB, house);
            while (false && em.MoveNext())
            {
                counter++;
                Vector2 specialBuilding = em.Current;
                System.Console.WriteLine("Special building number: " + counter);
                float x = specialBuilding.X;
                float y = specialBuilding.Y;


            }



            return specialBuildings.OrderBy(v => Vector2.Distance(v, house));
        }

        private static List<Vector2> split(List<Vector2> Vector2List, Vector2 house)
        {
            if (Vector2List.Count == 2)
            {
                IEnumerator<Vector2>  Vector2ListIEnumerator = Vector2List.GetEnumerator();
                Vector2ListIEnumerator.MoveNext();
                Vector2 LeftVector2List = Vector2ListIEnumerator.Current;
                Vector2ListIEnumerator.MoveNext();
                Vector2 RightVector2List = Vector2ListIEnumerator.Current;
                return MergeLists(LeftVector2List,RightVector2List, house);
            }
            else
            {
                List<Vector2> SplitList = split(LeftVector2List, RightVector2List, house);
                SplitList.GetEnumerator();
                return MergeLists();
            }
        }

        private static List<Vector2> MergeSort(List<Vector2> leftVector2List, List<Vector2> rightVector2List, Vector2 house)
        {
            
        }

        private static List<Vector2> MergeSortList(List<Vector2> Vector2List, Vector2 house)
        {

            List<List<Vector2>> ListofLists = new List<List<Vector2>>();
            ListofLists.Add(Vector2List);



            IEnumerator<Vector2> emVector2 = Vector2List.GetEnumerator();
            IEnumerator<List<Vector2>> emList = ListofLists.GetEnumerator();

            System.Console.WriteLine("count listoflist: " + ListofLists.Count());
            if (emList.MoveNext())
            {
                List<Vector2> list = emList.Current;
                System.Console.WriteLine("count list: " + list.Count());
            }


            while (emList.Current.Count() > 1)
            {
                System.Console.WriteLine("emList.Current.Count() = " + emList.Current.Count());
                List<List<Vector2>> TempListofLists = new List<List<Vector2>>();
                emList.Reset();
                while (emList.MoveNext())
                {

                    List<Vector2> DevideList = emList.Current;
                    //System.Console.WriteLine("DevideList.Count() = " + DevideList.Count());
                    //ListofLists.Remove(emList.Current);
                    int Counter = 0;
                    List<Vector2> LeftList = new List<Vector2>();
                    List<Vector2> RightList = new List<Vector2>();
                    IEnumerator<Vector2> DevideListEnumerator = Vector2List.GetEnumerator();
                    while (DevideList.Count() / 2 > Counter && DevideListEnumerator.MoveNext())
                    {
                        System.Console.WriteLine("left while loop. Counter = " + Counter);
                        Vector2 vector = DevideListEnumerator.Current;
                        LeftList.Add(vector);
                        Counter++;
                    }
                    while (DevideListEnumerator.MoveNext() && Counter < DevideList.Count())
                    {
                        System.Console.WriteLine("right while loop. Counter = " + Counter);
                        Vector2 vector = DevideListEnumerator.Current;
                        RightList.Add(vector);
                        Counter++;
                    }
                    if (LeftList.Count > 0)
                    {
                        TempListofLists.Add(LeftList);
                    }
                    else
                    {
                        System.Console.WriteLine("Not adding empty lists!");
                    }

                    if (RightList.Count > 0)
                    {
                        TempListofLists.Add(RightList);
                    }
                    else
                    {
                        System.Console.WriteLine("Not adding empty lists!");
                    }





                }
                IEnumerator<List<Vector2>> TempListofListIEnumerator = TempListofLists.GetEnumerator();
                TempListofListIEnumerator.MoveNext();
                System.Console.WriteLine("TempListofListIEnumerator.Current.Count() = " + TempListofListIEnumerator.Current.Count());
                ListofLists = TempListofLists;
                emList = ListofLists.GetEnumerator();
                emList.MoveNext();

            }
            System.Console.WriteLine("end. emList.Current.Count() = " + emList.Current.Count());

            emList = ListofLists.GetEnumerator();
            emList.Reset();
            List<Vector2> LeftMergeList = new List<Vector2>();
            List<Vector2> RightMergeList = new List<Vector2>();
            List<Vector2> MergedList = new List<Vector2>();
            while (ListofLists.Count > 1)
            {
                List<List<Vector2>> TempListofLists = new List<List<Vector2>>();
                System.Console.WriteLine("ListofLists.Count = " + ListofLists.Count);
                while (emList.MoveNext())
                {
                    System.Console.WriteLine("in while loop");
                    LeftMergeList = emList.Current;
                    if (emList.MoveNext())
                    {
                        RightMergeList = emList.Current;
                        MergedList = MergeLists(LeftMergeList, RightMergeList, house);
                        TempListofLists.Add(MergedList);
                    }
                    else
                    {
                        System.Console.WriteLine("Found a loneley list!");
                        TempListofLists.Add(LeftMergeList);
                    }


                    emList = ListofLists.GetEnumerator();
                }
                ListofLists = TempListofLists;

            }
            emList = ListofLists.GetEnumerator();
            emList.MoveNext();

            return emList.Current;
        }

        private static List<Vector2> MergeLists(List<Vector2> LeftList, List<Vector2> RightList, Vector2 house)
        {
            System.Console.WriteLine("start mergelist");
            List<Vector2> MergList = new List<Vector2>();
            IEnumerator<Vector2> LeftEnumerator = LeftList.GetEnumerator();
            IEnumerator<Vector2> RightEnumerator = RightList.GetEnumerator();

            float housex = house.X;
            float housey = house.Y;
            System.Console.WriteLine("LeftList.Count = " + LeftList.Count);
            for (int i = 0; i < LeftList.Count; i++)
            {


                float leftx = LeftEnumerator.Current.X;
                float lefty = LeftEnumerator.Current.Y;
                float rightx = RightEnumerator.Current.X;
                float righty = RightEnumerator.Current.Y;
                System.Console.WriteLine("leftx = " + leftx + "lefty = " + lefty + "rightx = " + rightx + "righty = " + righty);


                double leftDistance = Math.Sqrt(Math.Pow(leftx - housex, 2) + Math.Pow(lefty - housey, 2));
                double rightDistance = Math.Sqrt(Math.Pow(rightx - housex, 2) + Math.Pow(righty - housey, 2));

                System.Console.WriteLine("leftDistance = " + leftDistance);
                System.Console.WriteLine("rightDistance = " + rightDistance);

                if (leftDistance >= rightDistance)
                {
                    MergList.Add(LeftEnumerator.Current);
                    if (!LeftEnumerator.MoveNext())
                    {
                        while (RightEnumerator.MoveNext())
                        {
                            MergList.Add(RightEnumerator.Current);
                        }

                    }
                }
                else
                {
                    MergList.Add(RightEnumerator.Current);
                    if (!RightEnumerator.MoveNext())
                    {
                        while (LeftEnumerator.MoveNext())
                        {
                            MergList.Add(LeftEnumerator.Current);
                        }
                    }

                }
            }

            return MergList;
        }



        private static IEnumerable<IEnumerable<Vector2>> FindSpecialBuildingsWithinDistanceFromHouse(
          IEnumerable<Vector2> specialBuildings,
          IEnumerable<Tuple<Vector2, float>> housesAndDistances)
        {
            return
                from h in housesAndDistances
                select
                  from s in specialBuildings
                  where Vector2.Distance(h.Item1, s) <= h.Item2
                  select s;
        }

        private static IEnumerable<Tuple<Vector2, Vector2>> FindRoute(Vector2 startingBuilding,
          Vector2 destinationBuilding, IEnumerable<Tuple<Vector2, Vector2>> roads)
        {
            var startingRoad = roads.Where(x => x.Item1.Equals(startingBuilding)).First();
            List<Tuple<Vector2, Vector2>> fakeBestPath = new List<Tuple<Vector2, Vector2>>() { startingRoad };
            var prevRoad = startingRoad;
            for (int i = 0; i < 30; i++)
            {
                prevRoad = (roads.Where(x => x.Item1.Equals(prevRoad.Item2)).OrderBy(x => Vector2.Distance(x.Item2, destinationBuilding)).First());
                fakeBestPath.Add(prevRoad);
            }
            return fakeBestPath;
        }

        private static IEnumerable<IEnumerable<Tuple<Vector2, Vector2>>> FindRoutesToAll(Vector2 startingBuilding,
          IEnumerable<Vector2> destinationBuildings, IEnumerable<Tuple<Vector2, Vector2>> roads)
        {
            List<List<Tuple<Vector2, Vector2>>> result = new List<List<Tuple<Vector2, Vector2>>>();
            foreach (var d in destinationBuildings)
            {
                var startingRoad = roads.Where(x => x.Item1.Equals(startingBuilding)).First();
                List<Tuple<Vector2, Vector2>> fakeBestPath = new List<Tuple<Vector2, Vector2>>() { startingRoad };
                var prevRoad = startingRoad;
                for (int i = 0; i < 30; i++)
                {
                    prevRoad = (roads.Where(x => x.Item1.Equals(prevRoad.Item2)).OrderBy(x => Vector2.Distance(x.Item2, d)).First());
                    fakeBestPath.Add(prevRoad);
                }
                result.Add(fakeBestPath);
            }
            return result;
        }
    }
#endif
}
