using System;

namespace Exercise
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] values = {
                0,
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
            int gap = -1;
            int maxGap = 0;
            // var binary = Convert.ToString(n, 2); // DEBUG

            while (n != 0)
            {
                bool bit = (n % 2) == 1;

                if (!bit)
                {
                    if (gap >= 0)
                        gap++;
                }
                else
                {
                    if (gap == -1)
                        gap = 0;
                    else if (gap > 0)
                    {
                        if (gap > maxGap)
                            maxGap = gap;
                        gap = 0;
                    }
                }

                n = n >> 1;
            }

            return maxGap;
        }
    }
}