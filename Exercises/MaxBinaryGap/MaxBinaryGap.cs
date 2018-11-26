using System;

namespace Exercise
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] values = {
                1
                ,5
                ,16
                ,19
                ,1024
                ,1162
                ,561892
                ,74901729
                ,805306373
                ,2147483647
            };

            foreach (var n in values)
            {
                ExecuteSolution(n);
            }
        }

        public static void ExecuteSolution(int n)
        {
            Console.WriteLine("{0} [{1}] = > {2}", n, Convert.ToString(n, 2), Solution(n));
        }

        public static int Solution(int N)
        {
            int gap = 0;
            int maxGap = 0;
            bool potentialGap = false;
            bool inGap = false;

            while (N != 0)
            {
                bool bit = (N % 2) == 1 ? false : true;
                if (bit && inGap)
                {
                    gap++;
                }
                else if (!bit && inGap)
                {
                    if (gap > maxGap)
                    {
                        maxGap = gap;
                    }
                    gap = 0;
                    inGap = false;
                }
                else if (!bit && potentialGap)
                {
                    inGap = true;
                    potentialGap = false;
                }
                else if (bit && !inGap)
                {
                    potentialGap = true;
                }

                N = N >> 1;
            }

            return maxGap;
        }
    }
}