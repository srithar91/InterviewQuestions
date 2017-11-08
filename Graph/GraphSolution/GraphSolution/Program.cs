using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GraphSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph<string> graph = new Graph<string>();

            graph.AddEdge("undershorts", "pants");
            graph.AddEdge("undershorts", "shoes");
            graph.AddEdge("socks", "shoes");
            graph.AddEdge("pants", "shoes");
            graph.AddEdge("pants", "belt");
            graph.AddEdge("shirt", "belt");
            graph.AddEdge("shirt", "tie");
            graph.AddEdge("tie", "jacket");
            graph.AddEdge("belt", "jacket");

            Console.WriteLine("******Depth First Search*****");

            graph.DFS();

            Console.WriteLine();

            Console.WriteLine("*****Breadth First Search*****");

            graph.BFS();

            Console.WriteLine();

            Console.WriteLine("*****Toplological Sort*****");

            graph.TopologicalSort();

            Console.ReadLine();
        }
    }
}
