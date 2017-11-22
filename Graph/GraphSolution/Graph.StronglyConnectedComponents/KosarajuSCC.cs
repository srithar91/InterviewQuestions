using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph.Contracts;

namespace Graph.StronglyConnectedComponents
{
    public class KosarajuSCC<T> : ISCC<T>
    {
        public List<List<T>> FindSCG(Graph<T> graph)
        {
            List<List<T>> sccResult = new List<List<T>>();

            List<Vertex<T>> dfs = graph.DFS();

            Stack<Vertex<T>> originalGraphStack = new Stack<Vertex<T>>();

            foreach (Vertex<T> vertex in dfs)
            {
                originalGraphStack.Push(vertex);
            }

            Graph<T> reversedGraph = graph.ReverseGraph();
            HashSet<T> visited = new HashSet<T>();

            while (originalGraphStack.Count != 0)
            {
                Vertex<T> start = originalGraphStack.Pop();

                if (!visited.Contains(start.Key))
                {
                    Stack<T> sccStack = new Stack<T>();

                    DFSUtil(start.Key, reversedGraph, visited, sccStack);

                    List<T> sccVertices = new List<T>();

                    while (sccStack.Count > 0)
                    {
                        sccVertices.Add(sccStack.Pop());
                    }

                    sccResult.Add(sccVertices);
                }
            }

            return sccResult;
        }

        private void DFSUtil(T source, Graph<T> reversedGraph, HashSet<T> visited, Stack<T> result)
        {
            visited.Add(source);

            foreach (Vertex<T> adjVertex in reversedGraph.Vertices[source].AdjList)
            {
                if (!visited.Contains(adjVertex.Key))
                    DFSUtil(adjVertex.Key, reversedGraph, visited, result);
            }

            result.Push(source);
        }
    }
}
