using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVLTreeSolution
{
    public class AVLTree
    {
        public Node Root;

        public void Insert(int key)
        {
            this.Root = Insert(this.Root, key);
        }

        public void Delete(int key)
        {
            this.Root = Delete(this.Root, key);
        }

        public bool Search(int key)
        {
            return Search(this.Root, key);
        }

        private Node Insert(Node root, int key)
        {
            if (root == null)
                return new Node(key);

            if (root.Key > key)
            {
                root.Left = Insert(root.Left, key);
            }
            else
            {
                root.Right = Insert(root.Right, key);
            }

            root.Height = 1 + Math.Max(GetHeight(root.Left), GetHeight(root.Right));

            int balance = GetBalance(root.Left, root.Right);

            if (balance > 1 && root.Left.Key > key)
            {
                return RightRotate(root);
            }
            else if (balance > 1 && root.Left.Key < key)
            {
                root.Left = LeftRotate(root.Left);
                return RightRotate(root);
            }
            else if (balance < -1 && root.Right.Key < key)
            {
                return LeftRotate(root);
            }
            else if (balance < -1 && root.Right.Key > key)
            {
                root.Right = RightRotate(root.Right);
                return LeftRotate(root);
            }

            return root;
        }

        private Node LeftRotate(Node x)
        {
            Node y = x.Right;
            Node T2 = y.Left;

            x.Right = T2;
            y.Left = x;

            x.Height = 1 + Math.Max(GetHeight(x.Left), GetHeight(x.Right));
            y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));

            return y;
        }

        private Node RightRotate(Node y)
        {
            Node x = y.Left;
            Node T2 = x.Right;

            y.Left = T2;
            x.Right = y;

            x.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));
            y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));

            return x;
        }

        private int GetBalance(Node left, Node right)
        {
            return GetHeight(left) - GetHeight(right);
        }

        private int GetHeight(Node node)
        {
            if (node == null)
                return 0;
            else
                return node.Height;
        }

        private Node Delete(Node root, int key)
        {
            if (root == null)
                return null;

            if (root.Key == key)
            {
                if (root.Left == null && root.Right == null)
                    return null;
                else if (root.Left == null || root.Right == null)
                {
                    return root.Left == null ? root.Right : root.Left;
                }
                else
                {
                    Node successor = FindSuccessor(root.Right);

                    root.Key = successor.Key;

                    root.Right = Delete(root.Right, successor.Key);
                }
            }

            if (root.Key > key)
            {
                root.Left = Delete(root.Left, key);
            }
            else
            {
                root.Right = Delete(root.Right, key);
            }

            root.Height = 1 + Math.Max(GetHeight(root.Left), GetHeight(root.Right));

            int balance = GetBalance(root.Left, root.Right);

            if (balance > 1 && root.Left.Key > key)
            {
                return RightRotate(root);
            }
            else if (balance > 1 && root.Left.Key < key)
            {
                root.Left = LeftRotate(root.Left);
                return RightRotate(root);
            }
            else if (balance < -1 && root.Right.Key < key)
            {
                return LeftRotate(root);
            }
            else if (balance < -1 && root.Right.Key > key)
            {
                root.Right = RightRotate(root.Right);
                return LeftRotate(root);
            }

            return root;
        }

        private Node FindSuccessor(Node root)
        {
            while (root.Left != null)
                root = root.Left;

            return root;
        }

        private bool Search(Node root, int key)
        {
            if (root == null)
                return false;

            if (root.Key == key)
                return true;
            else if (root.Key > key)
            {
                return Search(root.Left, key);
            }
            else
            {
                return Search(root.Right, key);
            }
        }
    }
}
