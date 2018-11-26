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
                ,9
                ,16
                ,19
                ,1024
                ,1162
                ,66561
                ,561892
                ,6291457
                ,74901729
                ,805306373
                ,1073741825
                ,1610612737
                ,2147483647
            };

            foreach (var n in values)
            {
                ExecuteSolution(n);
            }
        }

        public static void ExecuteSolution(int n)
        {
            Console.Write($"{n} [{Convert.ToString(n, 2)}] => ");
            Console.WriteLine($"{Solution(n)}");
        }

        public static int Solution(int n)
        {
            int gap = 0;
            int maxGap = 0;
            bool potentialGap = false;
            bool inGap = false;
            // var binary = Convert.ToString(n, 2); // DEBUG

            while (n != 0)
            {
                bool bit = (n % 2) == 1;

                if (bit)
                {
                    if (inGap)
                    {
                        if (gap > maxGap)
                            maxGap = gap;
                        gap = 0;
                        inGap = false;
                    }
                    if (!potentialGap)
                        potentialGap = true;
                }
                else
                {
                    if (inGap)
                        gap++;
                    else if (potentialGap)
                    {
                        gap = 1;
                        inGap = true;
                        potentialGap = false;
                    }
                }

                n = n >> 1;
            }

            return maxGap;
        }
    }
}