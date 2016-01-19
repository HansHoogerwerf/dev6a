using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xna.Framework;

namespace EntryPoint
{
    class KdTree
    {
        public Node Root { get; set; }
        private List<Vector2> NodeList = new List<Vector2>();

        public KdTree(Node root)
        {
            this.Root = root;
            root.CompareX = true;
            root.Level = 0;
            root.LeftNode = null;
            root.RightNode = null;
            root.ParentNode = null;
        }

        public List<Vector2> Find(Vector2 house, float offset)
        {
            return FindNodes(house, offset, this.Root);
        }

        

        private List<Vector2> FindNodes(Vector2 house, float offset, Node node)
        {
//            if (Vector2.Distance(node.Value, house) <= offset)
//            {
//                NodeList.Add(node.Value);
//            }
//            if (node.LeftNode != null)
//            {
//                FindNodes(house, offset, node.LeftNode);
//            }
//
//            if (node.RightNode != null)
//            {
//                FindNodes(house, offset, node.RightNode);
//            }
//            return NodeList;


//            Console.WriteLine(house.ToString());
//            Console.WriteLine(node.ToString());
//            Console.WriteLine("Distance = " +  Program.Distance(node.Value, house));
//            Console.WriteLine("Offset = " + offset);


            if (Program.Distance(node.Value, house) <= offset)
            {
                Console.WriteLine("House in offset!");
                NodeList.Add(node.Value);
                if (node.LeftNode != null)
                {
                    FindNodes(house, offset, node.LeftNode);
                }
                if (node.RightNode != null)
                {
                    FindNodes(house, offset, node.RightNode);
                }    

            }
            else
            {

                if (node.CompareX)
                {
                    if (node.Value.X <= (house.X + offset) && node.Value.X >= (house.X - offset))
                    {
                        if (node.LeftNode != null)
                        {
                            FindNodes(house, offset, node.LeftNode);
                        }
                        if (node.RightNode != null)
                        {
                            FindNodes(house, offset, node.RightNode);
                        }
                    }
                    else if (node.Value.X >= (house.X + offset))
                    {
                        if (node.LeftNode != null)
                        {
                            FindNodes(house, offset, node.LeftNode);
                        }
                        

                    }
                    else
                    {
                        if (node.RightNode != null)
                        {
                            FindNodes(house, offset, node.RightNode);
                        }
                        
                    }

                }
                else
                {
                    if (node.Value.Y <= (house.Y + offset) && node.Value.Y >= (house.Y - offset))
                    {
                        if (node.LeftNode != null)
                        {
                            FindNodes(house, offset, node.LeftNode);
                        }
                        if (node.RightNode != null)
                        {
                            FindNodes(house, offset, node.RightNode);
                        }
                    }
                    else if (node.Value.Y >= (house.Y + offset))
                    {
                        if (node.LeftNode != null)
                        {
                            FindNodes(house, offset, node.LeftNode);
                        }
                        

                    }
                    else
                    {
                        if (node.RightNode != null)
                        {
                            FindNodes(house, offset, node.RightNode);
                        }
                    }

                }
            }

            return NodeList;
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
                    if (compareNode.Value.X >= node.Value.X)
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
                    if (compareNode.Value.Y >= node.Value.Y)
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
                node.LeftNode = null;
                node.RightNode = null;
                node.Level = level + 1;

            }
            //Console.WriteLine(node.ToString());
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
            if(ParentNode != null)
            {
                returnString += " ParentNode.Value = " + ParentNode.Value;
            }
            else
            {
                returnString += " ParentNode.Value = null";
            }
            if (LeftNode != null)
            {
                returnString += " LeftNode.Value = " + LeftNode.Value;
            }
            else
            {
                returnString += " LeftNode.Value = null";
            }
            if (RightNode != null)
            {
                returnString += " RightNode.Value = " + RightNode.Value;
            }
            else
            {
                returnString += " RightNode.Value = null";
            }


            returnString += " }";

            return returnString;
        }
    }
}
