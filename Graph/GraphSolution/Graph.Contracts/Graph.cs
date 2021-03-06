﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Contracts
{
    public class Graph<T>
    {
        public Dictionary<T, Vertex<T>> Vertices;
        public Dictionary<string, Edge<T>> Edges;
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

        public List<Vertex<T>> DFS()
        {
            List<Vertex<T>> dfsResult = new List<Vertex<T>>();

            ClearVisitedSet();

            foreach (T key in Vertices.Keys)
            {
                if (!VisitedSet.Contains(key))
                    DFSUtil(key, dfsResult);
            }

            return dfsResult;
        }

        public List<Vertex<T>> BFS()
        {
            List<Vertex<T>> bfsResult = new List<Vertex<T>>();

            ClearVisitedSet();

            Queue<T> queue = new Queue<T>();

            foreach (T key in Vertices.Keys)
            {
                if (!VisitedSet.Contains(key))
                    BFSUtil(key, queue, bfsResult);
            }

            return bfsResult;
        }

        public List<Vertex<T>> TopologicalSort()
        {
            ClearVisitedSet();

            Stack<Vertex<T>> stack = new Stack<Vertex<T>>();

            foreach (T key in Vertices.Keys)
            {
                if (!VisitedSet.Contains(key))
                    TopologicalSortUtil(key, stack);
            }

            while (stack.Count > 0)
            {
                Console.WriteLine(stack.Pop());
            }

            return stack.ToList();
        }

        private void DFSUtil(T source, List<Vertex<T>> dfsResult)
        {
            VisitedSet.Add(source);

            Time++;

            Vertices[source].DiscoveryTime = Time;

            for (int i = 0; i < Vertices[source].AdjList.Count; i++)
            {
                if (!VisitedSet.Contains(Vertices[source].AdjList[i].Key))
                    DFSUtil(Vertices[source].AdjList[i].Key, dfsResult);
            }

            Time++;

            Vertices[source].FinishingTime = Time;

            dfsResult.Add(Vertices[source]);
        }

        private void BFSUtil(T source, Queue<T> queue, List<Vertex<T>> bfsResult)
        {
            queue.Enqueue(source);

            this.VisitedSet.Add(source);

            bfsResult.Add(Vertices[source]);

            while (queue.Count > 0)
            {
                T vertex = queue.Dequeue();

                for (int i = 0; i < Vertices[vertex].AdjList.Count; i++)
                {
                    if (!VisitedSet.Contains(Vertices[vertex].AdjList[i].Key))
                    {
                        queue.Enqueue(Vertices[vertex].AdjList[i].Key);
                        this.VisitedSet.Add(Vertices[vertex].AdjList[i].Key);
                        bfsResult.Add(Vertices[vertex].AdjList[i]);
                    }
                }
            }
        }

        private void TopologicalSortUtil(T source, Stack<Vertex<T>> stack)
        {
            VisitedSet.Add(source);

            for (int i = 0; i < Vertices[source].AdjList.Count; i++)
            {
                if (!VisitedSet.Contains(Vertices[source].AdjList[i].Key))
                    TopologicalSortUtil(Vertices[source].AdjList[i].Key, stack);
            }

            stack.Push(Vertices[source]);
        }

        private void ClearVisitedSet()
        {
            this.VisitedSet.Clear();
        }

        public Graph<T> CloneGraph()
        {
            Graph<T> cloneGraph = new Graph<T>();

            foreach (KeyValuePair<T, Vertex<T>> vertex in this.Vertices)
            {
                cloneGraph.AddVertex(vertex.Key);
            }

            foreach (KeyValuePair<string, Edge<T>> edge in this.Edges)
            {
                cloneGraph.AddEdge(edge.Value.Source, edge.Value.Destination);
            }

            return cloneGraph;
        }

        public Graph<T> ReverseGraph()
        {
            Graph<T> graph = CloneGraph();

            List<string> keys = this.Edges.Keys.ToList();

            foreach(string key in keys)
            {
                graph.RemoveEdge(graph.Edges[key].Source, graph.Edges[key].Destination);
            }

            foreach (KeyValuePair<string, Edge<T>> edge in this.Edges)
            {
                graph.AddEdge(edge.Value.Destination, edge.Value.Source);
            }

            return graph;
        }
    }
}
