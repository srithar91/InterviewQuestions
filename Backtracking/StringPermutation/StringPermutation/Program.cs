using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringPermutation
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] test1 = { 'A', 'B', 'C' , 'D'};
            Permute(test1, 0, test1.Length - 1);
            
            //****** Lexigraphically Sorted version which handles duplicates *******//

            char[] test2 = { 'A', 'A', 'B', 'B' };
            PermuteWithoutDuplicates(test2);

            Console.ReadLine();
        }

        public static void Permute(char[] str, int left, int right)
        {
            if (left >= right)
            {
                Console.WriteLine(new string(str));
                return;
            }
            for (int x = 0; x <= (right - left); x++)
            {
                Swap(str, left, left + x);
                Permute(str, left + 1, right);
                Swap(str, left, left + x);
            }
        }

        public static void PermuteWithoutDuplicates(char[] str)
        {
            int[] characterSet = new int[256];

            for (int i = 0; i < str.Length; i++)
            {
                characterSet[(int)str[i]]++;
            }

            StringBuilder sBuilder = new StringBuilder();
            FindPermutationsWithoutDuplicates(characterSet, sBuilder, str.Length);
        }

        private static void FindPermutationsWithoutDuplicates(int[] characterSet, StringBuilder sBuilder, int length)
        {
            if (sBuilder.Length == length)
            {
                Console.WriteLine(sBuilder.ToString());
                return;
            }
            for (int i = 0; i < 255; i++)
            {
                if (characterSet[i] > 0)
                {
                    sBuilder.Append((char)i);
                    characterSet[i]--;
                    
                    FindPermutationsWithoutDuplicates(characterSet, sBuilder, length);

                    sBuilder.Length--;
                    characterSet[i]++;
                }
            }
        }

        private static void Swap(char[] str, int l, int r)
        {
            char temp = str[l];
            str[l] = str[r];
            str[r] = temp;
        }
    }

    
}
