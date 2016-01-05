using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xna.Framework;

namespace EntryPoint
{
    class KdTree
    {
        public Node Root { get; set; }

        public KdTree(Node root)
        {
            this.Root = root;
            root.CompareX = true;
            root.Level = 0;
        }

        public List<Vector2> FindBetweenXandYVector2s(Vector2 house, float offset)
        {
            List<Vector2> returnList = new List<Vector2>();

            Node currentNode = Root;
            bool foundAll = false;


            Console.WriteLine("Start while loop!");
            while (!foundAll)
            {
                
                try
                {
                    Console.WriteLine(Vector2.Distance(currentNode.Value, house) + "<=" + offset);
                    if (Vector2.Distance(currentNode.Value, house) <= offset)
                    {
                        returnList.Add(currentNode.Value);
                    }
                    if (currentNode.CompareX)
                    {
                        if (currentNode.Value.X >= (house.X + offset))
                        {
                            currentNode = currentNode.LeftNode;
                        }
                        else
                        {
                            currentNode = currentNode.RightNode;
                        }

                    }
                    else
                    {
                        if (currentNode.Value.Y >= (house.Y + offset))
                        {
                            currentNode = currentNode.LeftNode;
                        }
                        else
                        {
                            currentNode = currentNode.RightNode;
                        }
                    }

                }
                catch (Exception)
                {
                    foundAll = true;
                }




            }
            Console.WriteLine(returnList.Count);
            return returnList;
        }



        public void Insert(Node node)
        {
            Node compareNode = Root;
            bool complete = false;
            while (!complete)
            {

                bool compareX = compareNode.CompareX;
                int level = compareNode.Level;
                if (compareNode.CompareX)
                {
                    if (compareNode.Value.X <= node.Value.X)
                    {
                        if (compareNode.LeftNode == null)
                        {
                            compareNode.LeftNode = node;

                            complete = true;
                        }
                        else
                        {
                            compareNode = compareNode.LeftNode;
                        }
                    }
                    else
                    {
                        if (compareNode.RightNode == null)
                        {
                            compareNode.RightNode = node;
                            complete = true;
                        }
                        else
                        {
                            compareNode = compareNode.RightNode;
                        }

                    }
                }
                else
                {
                    if (compareNode.Value.Y <= node.Value.Y)
                    {
                        if (compareNode.LeftNode == null)
                        {
                            compareNode.LeftNode = node;
                            complete = true;
                        }
                        else
                        {
                            compareNode = compareNode.LeftNode;
                        }
                    }
                    else
                    {
                        if (compareNode.RightNode == null)
                        {
                            compareNode.RightNode = node;
                            complete = true;
                        }
                        else
                        {
                            compareNode = compareNode.RightNode;
                        }

                    }

                }

                node.CompareX = !compareX;
                node.ParentNode = compareNode;
                node.Level = level + 1;
            }
            Console.WriteLine(node.ToString());
        }
    }

    class Node
    {
        public bool CompareX { get; set; }
        public Node ParentNode { get; set; }
        public Node LeftNode { get; set; }
        public Node RightNode { get; set; }
        public int Level { get; set; }
        public Vector2 Value { get; set; }
        public override string ToString()
        {
            string returnString = "Node {";
            returnString += " CompareX = " + CompareX.ToString();
            returnString += " Value = " + Value.ToString();
            returnString += " Level = " + Level;
            returnString += " }";

            return returnString;
        }
    }
}
