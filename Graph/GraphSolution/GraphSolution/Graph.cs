using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSolution
{
    public class Graph<T>
    {
        private Dictionary<T, Vertex<T>> Vertices;
        private Dictionary<string, Edge<T>> Edges;
        private HashSet<T> VisitedSet;
        private int Time;

        public Graph()
        {
            this.Vertices = new Dictionary<T, Vertex<T>>();
            this.Edges = new Dictionary<string, Edge<T>>();
            this.VisitedSet = new HashSet<T>();
            this.Time = 0;
        }

        public void AddVertex(T vertex)
        {
            if (!Vertices.ContainsKey(vertex))
            {
                Vertices.Add(vertex, new Vertex<T>(vertex));
            }
        }

        public void AddEdge(T src, T dest, int wt = 0)
        {
            if (!Vertices.ContainsKey(src))
                Vertices.Add(src, new Vertex<T>(src));

            if (!Vertices.ContainsKey(dest))
                Vertices.Add(dest, new Vertex<T>(dest));

            Edge<T> edge = new Edge<T>(src, dest, wt);
            string edgeKey = edge.ToString();

            if (!Edges.ContainsKey(edgeKey))
                Edges.Add(edgeKey, edge);

            Vertices[src].AddAdjVertex(Vertices[dest]);
        }

        public void RemoveEdge(T src, T dest, int wt = 0)
        {
            Edge<T> edge = new Edge<T>(src, dest, wt);
            string edgeKey = edge.ToString();

            if (Edges.ContainsKey(edgeKey))
            {
                Edges.Remove(edgeKey);
                Vertices[src].RemoveAdjVertex(Vertices[dest]);
            }
        }

        public void DFS()
        {
            ClearVisitedSet();

            foreach (T key in Vertices.Keys)
            {
                if (!VisitedSet.Contains(key))
                    DFSUtil(key);
            }
        }

        public void BFS()
        {
            ClearVisitedSet();

            Queue<T> queue = new Queue<T>();

            foreach (T key in Vertices.Keys)
            {
                if (!VisitedSet.Contains(key))
                    BFSUtil(key, queue);
            }
        }

        public void TopologicalSort()
        {
            ClearVisitedSet();

            Stack<T> stack = new Stack<T>();

            foreach (T key in Vertices.Keys)
            {
                if (!VisitedSet.Contains(key))
                    TopologicalSortUtil(key, stack);
            }

            while (stack.Count > 0)
            {
                Console.WriteLine(stack.Pop());
            }
        }

        private void DFSUtil(T source)
        {
            VisitedSet.Add(source);

            Time++;

            Vertices[source].DiscoveryTime = Time;

            for (int i = 0; i < Vertices[source].AdjList.Count; i++)
            {
                if (!VisitedSet.Contains(Vertices[source].AdjList[i].Key))
                    DFSUtil(Vertices[source].AdjList[i].Key);
            }

            Time++;

            Vertices[source].FinishingTime = Time;

            Console.WriteLine(string.Format("{0} ({1}/{2})", source, Vertices[source].DiscoveryTime, Vertices[source].FinishingTime));
        }

        private void BFSUtil(T source, Queue<T> queue)
        {
            queue.Enqueue(source);
            this.VisitedSet.Add(source);
            Console.WriteLine(source);

            while (queue.Count > 0)
            {
                T vertex = queue.Dequeue();

                for (int i = 0; i < Vertices[vertex].AdjList.Count; i++)
                {
                    if (!VisitedSet.Contains(Vertices[vertex].AdjList[i].Key))
                    {
                        queue.Enqueue(Vertices[vertex].AdjList[i].Key);
                        this.VisitedSet.Add(Vertices[vertex].AdjList[i].Key);
                        Console.WriteLine(Vertices[vertex].AdjList[i].Key);
                    }
                }
            }
        }

        private void TopologicalSortUtil(T source, Stack<T> stack)
        {
            VisitedSet.Add(source);

            for (int i = 0; i < Vertices[source].AdjList.Count; i++)
            {
                if (!VisitedSet.Contains(Vertices[source].AdjList[i].Key))
                    TopologicalSortUtil(Vertices[source].AdjList[i].Key, stack);
            }

            stack.Push(source);
        }

        private void ClearVisitedSet()
        {
            this.VisitedSet.Clear();
        }
    }
}
