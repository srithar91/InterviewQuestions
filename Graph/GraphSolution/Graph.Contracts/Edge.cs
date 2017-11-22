using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Contracts
{
    public class Edge<T>
    {
        public T Source;
        public T Destination;
        public int Weight;

        public Edge(T src, T dest, int wt)
        {
            this.Source = src;
            this.Destination = dest;
            this.Weight = wt;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", this.Source, this.Destination);
        }
    }
}
