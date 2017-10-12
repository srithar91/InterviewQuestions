using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegmentTreeSolution
{
    public class RMQSegmentTree
    {
        private int[] tree;
        private int[] input;
        public RMQSegmentTree(int[] input)
        {
            this.input = input;
            this.tree = new int[4 * input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                this.tree[i] = int.MaxValue;
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

            tree[pos] = Math.Min(tree[(2 * pos) + 1], tree[(2 * pos) + 2]);
        }

        public int RMQ(int qLow, int qHigh)
        {
            return this.RMQ(0, input.Length - 1, qLow, qHigh, 0);
        }

        private int RMQ(int low, int high, int qLow, int qHigh, int pos)
        {
            if (qHigh < low || qLow > high)
            {
                return int.MaxValue;
            }
            else if (qLow <= low && qHigh >= high)
            {
                return tree[pos];
            }
            else
            {
                int mid = low + ((high - low) / 2);

                int left = RMQ(low, mid, qLow, qHigh, (2 * pos) + 1);
                int right = RMQ(mid + 1, high, qLow, qHigh, (2 * pos) + 2);

                return Math.Min(left, right);
            }
        }
    }
}
