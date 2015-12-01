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
            //int counter = 0;
//            List<Vector2> specialB = specialBuildings.ToList();

            
            System.Console.WriteLine("specialB.count = " + specialBuildings.Count());
            //split(specialBuildings, house);
            //int counter = 0;
            //List<Vector2> UnorderedLeftList = new List<Vector2>();
            //List<Vector2> UnorderedRightList = new List<Vector2>();
            //while (specialBuildings.Count() / 2 > counter && em.MoveNext())
            //{
            //    UnorderedLeftList.Add(em.Current);
            //    //System.Console.WriteLine("Adding left item! Counter = " + counter);
            //    counter++;
            //}
            //while (em.MoveNext())
            //{
            //    UnorderedRightList.Add(em.Current);
            //    //System.Console.WriteLine("Adding right item! Counter = " + counter);
            //    counter++;
            //}

            IEnumerable<Vector2> testlist = split(specialBuildings,house);

            //IEnumerable<Vector2> testlist = MergeList(UnorderedLeftList, UnorderedRightList, house);

            System.Console.WriteLine("testlist.Count() = " + testlist.Count());

            return testlist;
            //return specialBuildings.OrderBy(v => Vector2.Distance(v, house));
        }

        private static IEnumerable<Vector2> split(IEnumerable<Vector2> Vector2List, Vector2 house)
        {
            //System.Console.WriteLine("Start split!");

            IEnumerator<Vector2> Vector2ListIEnumerator = Vector2List.GetEnumerator();

            if (Vector2List.Count() == 1)
            {
                System.Console.WriteLine("splitting = 1");
                return Vector2List;
            }


            int counter = 0;
            List<Vector2> UnorderedLeftList = new List<Vector2>();
            List<Vector2> UnorderedRightList = new List<Vector2>();
            while (Vector2List.Count() / 2 > counter && Vector2ListIEnumerator.MoveNext())
            {
                UnorderedLeftList.Add(Vector2ListIEnumerator.Current);
                //System.Console.WriteLine("Adding left item! Counter = " + counter);
                counter++;
            }
            while (Vector2ListIEnumerator.MoveNext())
            {
                UnorderedRightList.Add(Vector2ListIEnumerator.Current);
                //System.Console.WriteLine("Adding right item! Counter = " + counter);
                counter++;
            }
            System.Console.WriteLine("splitting = " + counter);

            //System.Console.WriteLine("UnorderedLeftList.Count = " +  UnorderedLeftList.Count);
            //System.Console.WriteLine("UnorderedRightList.Count = " + UnorderedRightList.Count);


            IEnumerable<Vector2> OrderedLeftList = splitleft(UnorderedLeftList, house);
            System.Console.WriteLine("Done left split!  count = " + OrderedLeftList.Count());


            IEnumerable<Vector2> OrderedRightList = splitright(UnorderedRightList, house);
            System.Console.WriteLine("Done right split! count = " + OrderedRightList.Count());

            System.Console.WriteLine("Final right split!  count = " + OrderedRightList.Count());
            System.Console.WriteLine("Final left split!  count = " + OrderedLeftList.Count());

            return MergeList(OrderedLeftList, OrderedRightList, house);
        }

        private static IEnumerable<Vector2> splitleft(IEnumerable<Vector2> Vector2List, Vector2 house)
        {
            //System.Console.WriteLine("Start split!");

            IEnumerator<Vector2> Vector2ListIEnumerator = Vector2List.GetEnumerator();

            if (Vector2List.Count() == 1)
            {
                System.Console.WriteLine("splitting = 1");
                return Vector2List;
            }


            int counter = 0;
            List<Vector2> UnorderedLeftList = new List<Vector2>();
            List<Vector2> UnorderedRightList = new List<Vector2>();
            while (Vector2List.Count() / 2 > counter && Vector2ListIEnumerator.MoveNext())
            {
                UnorderedLeftList.Add(Vector2ListIEnumerator.Current);
                //System.Console.WriteLine("Adding left item! Counter = " + counter);
                counter++;
            }
            while (Vector2ListIEnumerator.MoveNext())
            {
                UnorderedRightList.Add(Vector2ListIEnumerator.Current);
                //System.Console.WriteLine("Adding right item! Counter = " + counter);
                counter++;
            }
            System.Console.WriteLine("splitting = " + counter);

            //System.Console.WriteLine("UnorderedLeftList.Count = " +  UnorderedLeftList.Count);
            //System.Console.WriteLine("UnorderedRightList.Count = " + UnorderedRightList.Count);


            IEnumerable<Vector2> OrderedLeftList = splitleft(UnorderedLeftList, house);
            //System.Console.WriteLine("Done left split! OrderedLeftList.Count = " + OrderedLeftList.Count());

            IEnumerable<Vector2> OrderedRightList = splitright(UnorderedRightList, house);
            //System.Console.WriteLine("Done splitting! OrderedRightList.Count = " + OrderedRightList.Count() + " OrderedLeftList.Count = " + OrderedLeftList.Count());

            return MergeList(OrderedLeftList, OrderedRightList, house);
        }

        private static IEnumerable<Vector2> splitright(IEnumerable<Vector2> Vector2List, Vector2 house)
        {
            //System.Console.WriteLine("Start split!");

            IEnumerator<Vector2> Vector2ListIEnumerator = Vector2List.GetEnumerator();

            if (Vector2List.Count() == 1)
            {
                System.Console.WriteLine("splitting = 1");
                return Vector2List;
            }


            int counter = 0;
            List<Vector2> UnorderedLeftList = new List<Vector2>();
            List<Vector2> UnorderedRightList = new List<Vector2>();
            while (Vector2List.Count() / 2 > counter && Vector2ListIEnumerator.MoveNext())
            {
                UnorderedLeftList.Add(Vector2ListIEnumerator.Current);
                //System.Console.WriteLine("Adding left item! Counter = " + counter);
                counter++;
            }
            while (Vector2ListIEnumerator.MoveNext())
            {
                UnorderedRightList.Add(Vector2ListIEnumerator.Current);
                //System.Console.WriteLine("Adding right item! Counter = " + counter);
                counter++;
            }
            System.Console.WriteLine("splitting = " + counter);

            //System.Console.WriteLine("UnorderedLeftList.Count = " +  UnorderedLeftList.Count);
            //System.Console.WriteLine("UnorderedRightList.Count = " + UnorderedRightList.Count);


            IEnumerable<Vector2> OrderedLeftList = splitleft(UnorderedLeftList, house);
            //System.Console.WriteLine("Done left split!");

            IEnumerable<Vector2> OrderedRightList = splitright(UnorderedRightList, house);
            //System.Console.WriteLine("Done right split!");

            return MergeList(OrderedLeftList, OrderedRightList, house);
        }


        private static List<Vector2> MergeList(IEnumerable<Vector2> LeftList, IEnumerable<Vector2> RightList, Vector2 house)
        {
            IEnumerator<Vector2> LeftEnumerator = LeftList.GetEnumerator();
            IEnumerator<Vector2> RightEnumerator = RightList.GetEnumerator();

            int maxleft = LeftList.Count();
            int maxright = RightList.Count();

            int leftcounter = 0;
            int rightcounter = 0;

            List<Vector2> MergeList = new List<Vector2>();

            LeftEnumerator.MoveNext();
            RightEnumerator.MoveNext();

            float houseY = house.Y;
            float houseX = house.X;


            int shouldMerge = LeftList.Count() + RightList.Count() + 1000;
            

            for (int i = 0; i <= shouldMerge; i++)
            {
                System.Console.WriteLine("Merging number =  " + (i+1));

                float leftX = LeftEnumerator.Current.X;
                float leftY = LeftEnumerator.Current.Y;

                float rightX = RightEnumerator.Current.X;
                float rightY = RightEnumerator.Current.Y;

                float leftXDifference = houseX - leftX;
                float leftYDifference = houseY - leftY;

                float leftDifference = (leftXDifference * leftXDifference) + (leftYDifference * leftYDifference);

                float rightXDifference = houseX - rightX;
                float rightYDifference = houseY - rightY;

                float rightDifference = (rightXDifference * rightXDifference) + (rightYDifference * rightYDifference);

                if (leftDifference <= rightDifference && maxleft > leftcounter)
                {
                    leftcounter++;
                    MergeList.Add(LeftEnumerator.Current);
                    if (!LeftEnumerator.MoveNext())
                    {
                        while (RightEnumerator.MoveNext())
                        {
                            MergeList.Add(RightEnumerator.Current);
                            rightcounter++;
                            return MergeList;
                        }
                    }
                }
                else if (maxright > rightcounter)
                {
                    MergeList.Add(RightEnumerator.Current);
                    rightcounter++;
                    if (!RightEnumerator.MoveNext())
                    {
                        while (LeftEnumerator.MoveNext())
                        {
                            MergeList.Add(LeftEnumerator.Current);
                            leftcounter++;
                            return MergeList;
                        }
                    }
                }
                else
                {
                    return MergeList;
                }
            }

            return MergeList;

        }


        private static List<Vector2> MergeLists(IEnumerable<Vector2> LeftList, IEnumerable<Vector2> RightList, Vector2 house)
        {
            //System.Console.WriteLine("start mergelist");
            List<Vector2> MergeList = new List<Vector2>();
            IEnumerator<Vector2> LeftEnumerator = LeftList.GetEnumerator();
            IEnumerator<Vector2> RightEnumerator = RightList.GetEnumerator();

            float housex = house.X;
            float housey = house.Y;
            int shouldMerge = LeftList.Count() + RightList.Count();
            System.Console.WriteLine("Should merge = " + shouldMerge);
           
            LeftEnumerator.MoveNext();
            RightEnumerator.MoveNext();
            for (int i = 0; i <= shouldMerge; i++)
            {

                
                float leftx = LeftEnumerator.Current.X;
                float lefty = LeftEnumerator.Current.Y;
                float rightx = RightEnumerator.Current.X;
                float righty = RightEnumerator.Current.Y;
                if (rightx == 0&& righty == 0)
                {
                    System.Console.WriteLine("detected zero value");
                    break;
                }

                if (leftx == 0 && lefty == 0)
                {
                    System.Console.WriteLine("detected zero value");
                    break;
                }

                //System.Console.WriteLine("leftx = " + leftx + "lefty = " + lefty + "rightx = " + rightx + "righty = " + righty);


                double leftDistance = Math.Sqrt(Math.Pow(leftx - housex, 2)) + Math.Sqrt(Math.Pow(lefty - housey, 2));
                double rightDistance = Math.Sqrt(Math.Pow(rightx - housex, 2)) + Math.Sqrt(Math.Pow(righty - housey, 2));

                //System.Console.WriteLine("leftDistance = " + leftDistance);
                //System.Console.WriteLine("rightDistance = " + rightDistance);

                if (leftDistance <= rightDistance)
                {
                    //System.Console.WriteLine("leftDistance = " + leftDistance + " <= rightDistance = " + rightDistance);
                    MergeList.Add(LeftEnumerator.Current);
                    
                    if (!LeftEnumerator.MoveNext())
                    {
                        while (RightEnumerator.MoveNext())
                        {
                            System.Console.WriteLine("adding all right vectors");
                            MergeList.Add(RightEnumerator.Current);
                            return MergeList;
                        }

                    }
                }
                else
                {
                    //System.Console.WriteLine("leftDistance = " + leftDistance + " > rightDistance = " + rightDistance);
                    MergeList.Add(RightEnumerator.Current);
                    if (!RightEnumerator.MoveNext())
                    {
                        while (LeftEnumerator.MoveNext())
                        {
                            MergeList.Add(LeftEnumerator.Current);
                            System.Console.WriteLine("adding all left vectors");
                            return MergeList;
                            
                        }
                    }

                }
            }
            System.Console.WriteLine("Merging =  " + MergeList.Count());
            return MergeList;
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


















