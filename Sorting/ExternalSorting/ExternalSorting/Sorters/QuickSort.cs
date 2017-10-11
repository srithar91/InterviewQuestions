using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalSorting.Sorters
{
    public class QuickSort : SortBase
    {
        public override int[] Sort(int[] arr)
        {
            QuickSortImpl(arr, 0, arr.Length - 1);
            return arr;
        }

        private void QuickSortImpl(int[] arr, int p, int r)
        {
            if (p < r)
            {
                int pivot = Partition(arr, p, r);
                QuickSortImpl(arr, p, pivot - 1);
                QuickSortImpl(arr, pivot + 1, r);
            }
        }

        private int Partition(int[] arr, int p, int r)
        {
            int i = p - 1;

            for (int k = p; k <= r - 1; k++)
            {
                if (arr[k] <= arr[r])
                {
                    Swap(arr, k, i + 1);
                    i = i + 1;
                }
            }

            Swap(arr, i + 1, r);

            return i + 1;
        }

        private void Swap(int[] arr, int k, int v)
        {
            int temp = arr[k];
            arr[k] = arr[v];
            arr[v] = temp;
        }
    }
}
