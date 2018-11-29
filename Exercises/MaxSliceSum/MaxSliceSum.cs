using System;
using System.Linq;

namespace Exercise
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[][] tests = {
                new[] { 3, 2, -6, 4, 0 }
                ,new[] { 3, 2, -6, 4, 8 }
                ,new[] { 3, 2, -6, 4, 8, 20 }
                ,new[] { 3, 2, -6, 4, 8, -100 }
                ,new[] { 3, 2, -6, 4, 8, -100, 200, 10 }
                ,new[] { 500, 2, -6, 4, 8, -100, 200, 10 }
            };

            foreach (var test in tests)
            {
                ExecuteSolution(test);
            }
        }

        public static void ExecuteSolution(int[] test)
        {
            Console.Write($"{string.Join(',', test)} => ");
            Console.WriteLine($"{Solution(test)}");
        }

        public static int Solution(int[] values)
        {
            int n = values.Length;
            int maxSum = GetMaxSum(values, 0, 0, n);
            return maxSum;
        }

        private static int GetMaxSum(int[] values, int p, int q, int n)
        {
            int maxSum = 0;

            while (p < n && q < n)
            {
                int value = values[q];
                if (value == 0)
                    q++;
                if (value > 0)
                {
                    maxSum += value;
                    q++;
                }
                else if (value < 0)
                {
                    int sum = GetMaxSum(values, p + 1, q + 1, n);
                    if (sum > maxSum)
                    {
                        maxSum = sum;
                    }
                    q = n;
                }
            }

            return maxSum;
        }
    }
}
