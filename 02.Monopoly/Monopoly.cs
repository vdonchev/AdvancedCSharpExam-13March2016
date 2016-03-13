namespace _02.Monopoly
{
    using System;

    public static class Monopoly
    {
        private static int money = 50;
        private static int hotels = 0;
        private static int turns = 0;

        public static void Main()
        {
            var rows = int.Parse(Console.ReadLine().Split()[0].ToString());

            for (int row = 0; row < rows; row++)
            {
                var currentLine = Console.ReadLine();
                if (row % 2 == 0)
                {
                    for (int col = 0; col < currentLine.Length; col++)
                    {
                        MakeTurn(row, col, currentLine[col]);
                    }
                }
                else
                {
                    for (int col = currentLine.Length - 1; col >= 0; col--)
                    {
                        MakeTurn(row, col, currentLine[col]);
                    }
                }
            }

            Console.WriteLine($"Turns {turns}");
            Console.WriteLine($"Money {money}");
        }

        private static void MakeTurn(int row, int col, char type)
        {
            if (type == 'S')
            {
                var spent = Math.Min((row + 1) * (col + 1), money);
                Console.WriteLine($"Spent {spent} money at the shop.");
                money = Math.Max(money - spent, 0);
            }
            else if (type == 'J')
            {
                Console.WriteLine($"Gone to jail at turn {turns}.");

                money += 2 * (hotels * 10);
                turns += 2;
            }
            else if (type == 'H')
            {
                hotels++;
                Console.WriteLine($"Bought a hotel for {money}. Total hotels: {hotels}.");
                money = 0;
            }

            turns++;
            money += hotels * 10;
        }
    }
}