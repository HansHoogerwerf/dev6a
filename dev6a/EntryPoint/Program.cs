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

            Vector2[] specialbuildingsarray = specialBuildings.ToArray();

            int first = 0;
            int last = specialBuildings.Count() - 1;

            Vector2[] sortedbuildings = MergeSort(specialbuildingsarray, first, last, house);

            return sortedbuildings;
            //return specialBuildings.OrderBy(v => Vector2.Distance(v, house));
        }

        private static Vector2[] MergeSort(Vector2[] VectorArray, int MinValue, int MaxValue, Vector2 house)
        {
            Vector2[] returnarray = new Vector2[1];


            if (MinValue < MaxValue)
            {
                int Middle = (MinValue + MaxValue) / 2;
                MergeSort(VectorArray, MinValue, Middle, house);
                MergeSort(VectorArray, Middle + 1, MaxValue, house);
                returnarray = Merge(VectorArray, MinValue, Middle, MaxValue, house);
            }
            return returnarray;
        }

        private static Vector2[] Merge(Vector2[] VectorArray, int Min, int Middle, int Max, Vector2 house)
        {
            int leftArraySize = Middle - Min + 1;
            int rightArraySize = Max - Middle;
            Vector2[] leftArray = new Vector2[leftArraySize + 1];
            Vector2[] rightArray = new Vector2[rightArraySize + 1];
            int i = 0;
            int j = 0;
            for (i = 0; i < leftArraySize; i++)
            {
                leftArray[i] = VectorArray[Min + i];

            }

            for (j = 0; j < rightArraySize; j++)
            {
                rightArray[j] = VectorArray[Middle + 1 + j];

            }
            leftArray[leftArraySize] = new Vector2(float.MaxValue, float.MaxValue);
            rightArray[rightArraySize] = new Vector2(float.MaxValue, float.MaxValue);
            i = 0;
            j = 0;

            double leftdifference = Distance(house, leftArray[i]);
            double rightdifference = Distance(house, rightArray[j]);

            for (int k = Min; k <= Max; k++)
            {

                if (leftdifference <= rightdifference)
                {
                    VectorArray[k] = leftArray[i];
                    i++;
                    leftdifference = Distance(house, leftArray[i]);

                }
                else
                {
                    VectorArray[k] = rightArray[j];
                    j++;
                    rightdifference = Distance(house, rightArray[j]);
                }
            }
            return VectorArray;

        }

        private static double Distance(Vector2 FirstBuilding, Vector2 SecondBuilding)
        {
            float xLenght = FirstBuilding.X - SecondBuilding.X;
            float yLenght = FirstBuilding.Y - SecondBuilding.Y;
            double difference = Math.Sqrt((xLenght * xLenght) + (yLenght * yLenght));
            return difference;
        }




        private static IEnumerable<IEnumerable<Vector2>> FindSpecialBuildingsWithinDistanceFromHouse(
          IEnumerable<Vector2> specialBuildings,
          IEnumerable<Tuple<Vector2, float>> housesAndDistances)
        {
            IEnumerator<Vector2> specialBuildingEnumerator = specialBuildings.GetEnumerator();
            int count = 0;
            KdTree specialBuildingsKdTree = new KdTree(new Node());
            if (specialBuildingEnumerator.MoveNext())
            {
                count++;
                Node rootNode = new Node { Value = specialBuildingEnumerator.Current };
                specialBuildingsKdTree = new KdTree(rootNode);
                while (specialBuildingEnumerator.MoveNext())
                {
                    count++;
                    specialBuildingsKdTree.Insert(new Node { Value = specialBuildingEnumerator.Current });
                }
                Console.WriteLine("Count = " + count);
            }


            IEnumerator<Tuple<Vector2, float>> housesAndDistancesEnumerator = housesAndDistances.GetEnumerator();

            List<List<Vector2>> returnList = new List<List<Vector2>>();

            while (housesAndDistancesEnumerator.MoveNext())
            {
                Vector2 house = housesAndDistancesEnumerator.Current.Item1;
                float offset = housesAndDistancesEnumerator.Current.Item2;

                returnList.Add(specialBuildingsKdTree.FindBetweenXandYVector2s(house, offset));


                Console.WriteLine("found a house with Distance!");
                Console.WriteLine(housesAndDistancesEnumerator.Current.ToString());
            }

            return returnList;

//            return
//                from h in housesAndDistances
//                select
//                  from s in specialBuildings
//                  where Vector2.Distance(h.Item1, s) <= h.Item2
//                  select s;
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


















