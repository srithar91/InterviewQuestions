using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegmentTreeSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] input = { 2, 5, 1, 3, 7, 4, 6 };

            RMQSegmentTree rmqSegTree = new RMQSegmentTree(input);

            //Test Cases

            Console.WriteLine(string.Format("RangeMinimum({0},{1}): Expected: {2} Result: {3}", 0, 6, 1, rmqSegTree.RMQ(0, 6)));
            Console.WriteLine(string.Format("RangeMinimum({0},{1}): Expected: {2} Result: {3}", 2, 5, 1, rmqSegTree.RMQ(2, 5)));
            Console.WriteLine(string.Format("RangeMinimum({0},{1}): Expected: {2} Result: {3}", 4, 6, 4, rmqSegTree.RMQ(4, 6)));
            Console.WriteLine(string.Format("RangeMinimum({0},{1}): Expected: {2} Result: {3}", 0, 1, 2, rmqSegTree.RMQ(0, 1)));
            Console.WriteLine(string.Format("RangeMinimum({0},{1}): Expected: {2} Result: {3}", 3, 6, 3, rmqSegTree.RMQ(3, 6)));

            Debug.WriteLine("--------------------------------------------------------------------------------------------------------");

            SumSegmentTree sumSegTree = new SumSegmentTree(input);

            //Test Cases

            Console.WriteLine(string.Format("RangeSum({0},{1}): Expected: {2} Result: {3}", 0, 6, 28, sumSegTree.RangeSum(0, 6)));
            Console.WriteLine(string.Format("RangeSum({0},{1}): Expected: {2} Result: {3}", 2, 5, 15, sumSegTree.RangeSum(2, 5)));
            Console.WriteLine(string.Format("RangeSum({0},{1}): Expected: {2} Result: {3}", 4, 6, 17, sumSegTree.RangeSum(4, 6)));
            Console.WriteLine(string.Format("RangeSum({0},{1}): Expected: {2} Result: {3}", 0, 1, 7, sumSegTree.RangeSum(0, 1)));
            Console.WriteLine(string.Format("RangeSum({0},{1}): Expected: {2} Result: {3}", 3, 6, 20, sumSegTree.RangeSum(3, 6)));

            Console.ReadLine();
        }
    }
}
