using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalSorting.Heaps
{
    public class HeapNode
    {
        public int Value;
        public int Chunk;

        public HeapNode(int val, int chunk)
        {
            this.Value = val;
            this.Chunk = chunk;
        }
    }
}
