using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalSorting.Heaps
{
    public class MinHeap : Heap
    {
        private List<HeapNode> elements;

        public MinHeap()
        {
            elements = new List<HeapNode>();
        }

        public int Size
        {
            get
            {
                return elements.Count;
            }
        }

        public void Insert(int num, int listIndex)
        {
            elements.Add(new HeapNode(num, listIndex));

            int i = elements.Count - 1;

            int parent = GetParent(elements.Count - 1);

            while (parent >= 0 && elements[parent].Value > elements[i].Value)
            {
                Swap(elements, parent, i);
                i = parent;
                parent = GetParent(i);
            }
        }

        public void ReplaceMin(int num, int listIndex)
        {
            elements[0] = new HeapNode(num, listIndex);
            MinHeapify(0);
        }

        public HeapNode GetTop()
        {
            return Size > 0 ? elements[0] : null;
        }

        public HeapNode ExtractTop()
        {
            HeapNode top = null;

            if (Size > 0)
            {
                top = elements[0];

                Swap(elements, 0, elements.Count - 1);

                elements.RemoveAt(elements.Count - 1);

                MinHeapify(0);
            }

            return top;
        }

        public void MinHeapify(int i)
        {
            int left = GetLeft(i);
            int right = GetRight(i);

            int smallest = i;

            if (left < Size && elements[left].Value < elements[i].Value)
            {
                smallest = left;
            }

            if (right < Size && elements[right].Value < elements[smallest].Value)
            {
                smallest = right;
            }

            if (smallest != i)
            {
                Swap(elements, smallest, i);
                MinHeapify(smallest);
            }
        }
    }
}
