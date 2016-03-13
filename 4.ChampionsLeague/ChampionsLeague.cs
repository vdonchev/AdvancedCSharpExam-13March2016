namespace _4.ChampionsLeague
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ChampionsLeague
    {
        private const char TokensSeparator = '|';
        private const char ResultSeparator = ':';

        private static IDictionary<string, int> teamWins;
        private static IDictionary<string, IList<string>> teamOpponents;

        public static void Main()
        {
            teamWins = new SortedDictionary<string, int>();
            teamOpponents = new SortedDictionary<string, IList<string>>();

            var input = Console.ReadLine();
            while (input != "stop")
            {
                var matchTokens = input
                    .Split(TokensSeparator)
                    .Select(t => t.Trim())
                    .ToArray();

                var teamA = matchTokens[0];
                var teamB = matchTokens[1];
                var teamAhomeMatch = matchTokens[2]
                    .Split(ResultSeparator)
                    .Select(int.Parse)
                    .ToArray();

                var teamBhomeMatch = matchTokens[3]
                    .Split(ResultSeparator)
                    .Select(int.Parse)
                    .ToArray();

                var teamATotalGoals = teamAhomeMatch[0] + teamBhomeMatch[1];
                var teamBTotalGoals = teamAhomeMatch[1] + teamBhomeMatch[0];

                var winnerIndex = teamATotalGoals > teamBTotalGoals ? 0 : 1;
                if (teamATotalGoals == teamBTotalGoals)
                {
                    winnerIndex = teamAhomeMatch[1] > teamBhomeMatch[1] ? 1 : 0;
                }

                var winner = matchTokens[winnerIndex];
                var loser = matchTokens[1 - winnerIndex];

                if (!teamWins.ContainsKey(winner))
                {
                    teamWins[winner] = 0;
                }

                if (!teamWins.ContainsKey(loser))
                {
                    teamWins[loser] = 0;
                }

                teamWins[winner] += 1;

                if (!teamOpponents.ContainsKey(winner))
                {
                    teamOpponents[winner] = new List<string>();
                }

                teamOpponents[winner].Add(loser);

                if (!teamOpponents.ContainsKey(loser))
                {
                    teamOpponents[loser] = new List<string>();
                }

                teamOpponents[loser].Add(winner);

                input = Console.ReadLine();
            }

            foreach (var team in teamWins.OrderByDescending(t => t.Value))
            {
                Console.WriteLine(team.Key);
                Console.WriteLine($"- Wins: {team.Value}");
                Console.WriteLine($"- Opponents: {string.Join(", ", teamOpponents[team.Key].OrderBy(t => t))}");
            }
        }
    }
}
