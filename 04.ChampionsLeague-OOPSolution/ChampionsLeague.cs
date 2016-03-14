namespace _04.ChampionsLeague_OOPSolution
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class ChampionsLeague
    {
        public static void Main()
        {
            var championsLeague = new League();

            var input = string.Empty;
            while ((input = Console.ReadLine()) != "stop")
            {
                var matchDetails = GetMatchDetails(input);

                var teamA = championsLeague.GetTeam(matchDetails[0]);
                var teamB = championsLeague.GetTeam(matchDetails[1]);

                var firstMatch = GetMatchResult(matchDetails[2]);
                var secondMatch = GetMatchResult(matchDetails[3]);

                var match = new HomeAwayMatch(teamA, teamB);
                match.Play(firstMatch, secondMatch);
            }

            championsLeague.PrintScoreBoard();
        }

        private static int[] GetMatchResult(string teamNames)
        {
            return teamNames
                .Split(':')
                .Select(int.Parse)
                .ToArray();
        }

        private static string[] GetMatchDetails(string input)
        {
            return input
                .Split('|')
                .Select(t => t.Trim())
                .ToArray();
        }

        private class League
        {
            private readonly ISet<Team> teams;

            public League()
            {
                this.teams = new SortedSet<Team>();
            }

            public Team GetTeam(string name)
            {
                if (!this.teams.Any(t => t.Name.Equals(name)))
                {
                    this.teams.Add(new Team(name));
                }

                return this.teams.First(t => t.Name.Equals(name));
            }

            public void PrintScoreBoard()
            {
                foreach (var team in this.teams.OrderByDescending(t => t.Wins))
                {
                    Console.WriteLine(team);
                }
            }
        }

        private class Team : IComparable<Team>
        {
            private readonly ISet<Team> opponents;

            public Team(string name)
            {
                this.Name = name;
                this.opponents = 
                    new SortedSet<Team>(Comparer<Team>.Create((team1, team2) => team1.Name.CompareTo(team2.Name)));
            }

            public string Name { get; private set; }

            public int Wins { get; set; }

            public void AddOpponent(Team opponent)
            {
                this.opponents.Add(opponent);
            }

            public int CompareTo(Team other)
            {
                return string.Compare(this.Name, other.Name, StringComparison.Ordinal);
            }

            public override string ToString()
            {
                var res = new StringBuilder();
                res.AppendLine(this.Name);
                res.AppendLine($"- Wins: {this.Wins}");
                res.AppendLine($"- Opponents: {string.Join(", ", this.opponents.Select(o => o.Name))}");

                return res.ToString().Trim();
            }
        }

        private class HomeAwayMatch
        {
            private readonly Team teamA;
            private readonly Team teamB;

            public HomeAwayMatch(Team teamA, Team teamB)
            {
                this.teamA = teamA;
                this.teamB = teamB;

                teamA.AddOpponent(teamB);
                teamB.AddOpponent(teamA);
            }

            public void Play(int[] resultA, int[] resultB)
            {
                var teamAGoals = resultA[0] + resultB[1];
                var teamBGoals = resultA[1] + resultB[0];

                var winnerIndex = teamAGoals > teamBGoals ? 0 : 1;
                if (teamAGoals == teamBGoals)
                {
                    winnerIndex = resultA[1] > resultB[1] ? 1 : 0;
                }

                if (winnerIndex == 0)
                {
                    this.teamA.Wins++;
                }
                else
                {
                    this.teamB.Wins++;
                }
            }
        }
    }
}
