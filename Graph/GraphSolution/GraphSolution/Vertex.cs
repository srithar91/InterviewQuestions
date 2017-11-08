using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSolution
{
    public class Vertex<T>
    {
        public T Key;
        public List<Vertex<T>> AdjList;
        public int DiscoveryTime;
        public int FinishingTime;

        public Vertex(T key)
        {
            this.Key = key;
            this.AdjList = new List<Vertex<T>>();
        }

        public void AddAdjVertex(Vertex<T> adjVertex)
        {
            if (adjVertex == null)
                return;

            this.AdjList.Add(adjVertex);
        }

        public void RemoveAdjVertex(Vertex<T> adjVertex)
        {
            this.AdjList.Remove(adjVertex);
        }
    }
}
