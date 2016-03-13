namespace _01.ArrangeIntegers
{
    using System;
    using System.Linq;

    public static class ArrangeIntegers
    {
        private static readonly string[] NumWithWords =
            {"zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};

        public static void Main()
        {
            var nums = Console.ReadLine()
                .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .OrderBy(WordComparator)
                .ThenByDescending(long.Parse)
                .ToList();

            Console.WriteLine(string.Join(", ", nums));
        }

        private static string WordComparator(string str)
        {
            return string.Join("-", str.Select(ch => NumWithWords[byte.Parse(ch.ToString())]));
        }
    }
}