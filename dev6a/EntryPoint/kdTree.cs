using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace EntryPoint
{
    class KdTree
    {
        public Node Root { get; set;}

        public KdTree(Node root)
        {
            this.Root = root;
            root.CompareX = true;
            root.Level = 0;
        }


        public void Insert(Node node)
        {
            Node compareNode = Root;
            bool complete = false;
            while(!complete)
            {
                
                bool compareX = compareNode.CompareX;
                int level = compareNode.Level;
                if(compareNode.CompareX)
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
                node.Level = level + 1;
            }
            Console.WriteLine(node.ToString());
        }
    }

    class Node
    {
        public bool CompareX { get; set; }
        public Node ParentNode { get; set;}
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
