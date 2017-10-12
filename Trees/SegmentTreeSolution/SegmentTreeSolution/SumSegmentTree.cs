using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegmentTreeSolution
{
    public class SumSegmentTree
    {
        private int[] tree;
        private int[] input;
        public SumSegmentTree(int[] input)
        {
            this.input = input;
            this.tree = new int[4 * input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                this.tree[i] = 0;
            }

            this.BuildTree(0, input.Length - 1, 0);
        }

        private void BuildTree(int low, int high, int pos)
        {
            if (low == high)
            {
                tree[pos] = input[low];
                return;
            }

            int mid = low + ((high - low) / 2);

            BuildTree(low, mid, (2 * pos) + 1);
            BuildTree(mid + 1, high, (2 * pos) + 2);

            tree[pos] = tree[(2 * pos) + 1] + tree[(2 * pos) + 2];
        }

        public int RangeSum(int qLow, int qHigh)
        {
            return this.RangeSum(0, input.Length - 1, qLow, qHigh, 0);
        }

        private int RangeSum(int low, int high, int qLow, int qHigh, int pos)
        {
            if (qHigh < low || qLow > high)
            {
                return 0;
            }
            else if (qLow <= low && qHigh >= high)
            {
                return tree[pos];
            }
            else
            {
                int mid = low + ((high - low) / 2);

                int left = RangeSum(low, mid, qLow, qHigh, (2 * pos) + 1);
                int right = RangeSum(mid + 1, high, qLow, qHigh, (2 * pos) + 2);

                return left + right;
            }
        }
    }
}
