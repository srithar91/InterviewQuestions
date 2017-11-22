using Graph.Contracts;
using Graph.StronglyConnectedComponents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GraphConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph<string> graph = new Graph<string>();

            graph.AddEdge("A", "B");
            graph.AddEdge("B", "C");
            graph.AddEdge("C", "A");
            graph.AddEdge("B", "D");
            graph.AddEdge("D", "E");
            graph.AddEdge("E", "F");
            graph.AddEdge("F", "D");
            graph.AddEdge("G", "F");
            graph.AddEdge("G", "H");
            graph.AddEdge("H", "I");
            graph.AddEdge("I", "J");
            graph.AddEdge("J", "G");
            graph.AddEdge("G", "H");
            graph.AddEdge("J", "K");

            Console.WriteLine("******Depth First Search*****");

            graph.DFS();

            Console.WriteLine();

            Console.WriteLine("*****Breadth First Search*****");

            graph.BFS();

            Console.WriteLine();

            Console.WriteLine("*****Toplological Sort*****");

            graph.TopologicalSort();

            Console.WriteLine("*****Kosaraju SCC*****");

            ISCC<string> kosaraju = new KosarajuSCC<string>();
            List<List<string>> result = kosaraju.FindSCG(graph);

            foreach (List<string> vertices in result)
            {
                Console.WriteLine(string.Join(",", vertices));
            }

            Console.ReadLine();
        }
    }
}
