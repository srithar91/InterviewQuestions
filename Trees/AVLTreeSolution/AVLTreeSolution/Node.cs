using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVLTreeSolution
{
    public class Node
    {
        public Node Left;
        public Node Right;
        public int Key;
        public int Height;

        public Node(int key)
        {
            this.Key = key;
            this.Height = 1;
        }
    }
}
