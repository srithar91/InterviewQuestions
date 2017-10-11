using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalSorting.Heaps
{
    public class Heap
    {
        public int GetParent(int i)
        {
            return (i - 1) / 2;
        }

        public int GetLeft(int i)
        {
            return (2 * i) + 1;
        }

        public int GetRight(int i)
        {
            return (2 * i) + 2;
        }

        public void Swap(List<HeapNode> elements, int i, int j)
        {
            HeapNode temp = elements[i];
            elements[i] = elements[j];
            elements[j] = temp;
        }
    }
}
