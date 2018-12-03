using System;
using System.Linq;

namespace Exercise
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[][] tests = {
                new []{ -10 },
                new []{ -1, -1 },
                new []{ -1, 2 },
                new []{ 3, -2 },
                new []{ 3, -2, 1 },
                new []{ 3, -2, 3 },
                new []{ 6, -6, 4 },
                new []{ -1, -2, -3 },
                new []{ -3, -2, -1 },
                new []{ -1, -2, -3, -4 },
                new []{ -4, -3, -2, -1 },
                new [] { 3, 2, -6, 4, 0 },
                new [] { 3, 2, -6, 4, 8 },
                new [] { 3, 2, -6, 4, 8, 20 },
                new [] { 3, 2, -6, 4, 8, -100 },
                new [] { 3, 2, -6, 4, 8, -100, 200, 10 },
                new [] { 500, 2, -6, 4, 8, -100, 200, 10 },
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
            int sum = values[0];
            int maxSum = sum;

            for (int i = 1; i < values.Length; i++)
            {
                var value = values[i];

                sum = Math.Max(value, sum + value);
                maxSum = Math.Max(maxSum, sum);
            }
            return maxSum;
        }
    }

}
