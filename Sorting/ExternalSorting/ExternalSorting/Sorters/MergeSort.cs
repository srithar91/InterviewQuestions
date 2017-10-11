using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalSorting.Sorters
{
    public class MergeSort : SortBase
    {
        public override int[] Sort(int[] arr)
        {
            MergeSortImpl(arr, 0, arr.Length - 1);
            return arr;
        }

        private void MergeSortImpl(int[] arr, int p, int r)
        {
            if (p < r)
            {
                int q = (p + r) / 2;
                MergeSortImpl(arr, p, q);
                MergeSortImpl(arr, q + 1, r);
                Merge(arr, p, q, r);
            }
        }

        private void Merge(int[] arr, int p, int q, int r)
        {
            int[] L = new int[q - p + 1];
            int[] R = new int[r - q];

            for (int i = p, j = 0; i <= q; i++, j++)
                L[j] = arr[i];

            for (int i = q + 1, j = 0; i <= r; i++, j++)
                R[j] = arr[i];

            int l = 0, m = 0, k = p;

            while (l < L.Length && m < R.Length)
            {
                if (L[l] <= R[m])
                {
                    arr[k] = L[l];
                    l++;
                }
                else
                {
                    arr[k] = R[m];
                    m++;
                }

                k++;
            }

            while (l < L.Length)
            {
                arr[k] = L[l];
                l++;
                k++;
            }

            while (m < R.Length)
            {
                arr[k] = R[m];
                m++;
                k++;
            }
        }
    }
}
