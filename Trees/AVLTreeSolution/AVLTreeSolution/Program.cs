using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVLTreeSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = Enumerable.Range(1, 1000).ToList();

            AVLTree avlTree = new AVLTree();

            foreach (int number in numbers)
            {
                avlTree.Insert(number);
            }

            Console.WriteLine(string.Format("FindKey({0}): {1}", 100, avlTree.Search(100)));

            avlTree.Delete(100);

            Console.WriteLine(string.Format("FindKey({0}): {1}", 100, avlTree.Search(100)));

            Console.ReadLine();
        }
    }
}
