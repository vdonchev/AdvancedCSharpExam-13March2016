namespace _01.ArrangeIntegers_OneExpressionSolution
{
    using System;
    using System.Linq;

    public static class ArrangeIntegers
    {
        private static readonly string[] NumWithWords =
            {"zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};

        public static void Main()
        {
            Console.WriteLine(
                string.Join(
                    ", ",
                    Console.ReadLine()
                    .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .OrderBy(n => string.Join("-", n.Select(ch => NumWithWords[byte.Parse(ch.ToString())])))
                    .ThenByDescending(int.Parse)));
        }
    }
}