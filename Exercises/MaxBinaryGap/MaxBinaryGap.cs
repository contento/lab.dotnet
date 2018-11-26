using System;

namespace Exercise
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] values = {
                805306373 
                // 1
                // ,5
                // ,16
                // ,19
                // ,1024
                // ,1162
                // ,66561
                // ,561892
                // ,6291457
                // ,74901729
                // ,805306373
                // ,1073741825
                // ,1610612737
                // ,2147483647
            };

            foreach (var n in values)
            {
                ExecuteSolution(n);
            }
        }

        public static void ExecuteSolution(int n)
        {
            Console.Write($"{n} [{Convert.ToString(n, 2)}] = > ");
            Console.WriteLine($"{Solution(n)}");
        }

        public static int Solution(int N)
        {
            int gap = 0;
            int maxGap = 0;
            bool potentialGap = false;
            bool inGap = false;

            while (N != 0)
            {
                bool bit = (N % 2) == 1;
                if (!bit && inGap)
                {
                    gap++;
                }
                else if (bit && inGap)
                {
                    if (gap > maxGap)
                    {
                        maxGap = gap;
                    }
                    gap = 0;
                    inGap = false;
                }
                else if (bit && !inGap)
                {
                    potentialGap = true;
                }
                else if (!bit && potentialGap)
                {
                    gap = 1;
                    inGap = true;
                    potentialGap = false;
                }
                else if (bit && !potentialGap)
                {
                    potentialGap = true;
                }

                N = N >> 1;
            }

            return maxGap;
        }
    }
}