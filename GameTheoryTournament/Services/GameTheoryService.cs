using GameTheoryTournament.Models;
using GameTheoryTournament.Models.Enums;

namespace GameTheoryTournament.Services
{
    public static class GameTheoryService
    {
        public static Dictionary<Guid, Tournament> Tournaments = new();

        public static readonly int MatchLength = 10;

        public static void CreateTournament(string name)
        {
            var tournament = new Tournament
            {
                Id = Guid.NewGuid(),
                Name = name,
                Users = new List<User>(),
                Matches = new List<Match>()
            };

            Tournaments.Add(tournament.Id, tournament);
        }

        public static void AddUserToTournament(Guid tournamentId, string userName)
        {
            var tournament = Tournaments[tournamentId];
            if (tournament == null)
            {
                return;
            }
            var user = new User
            {
                Name = userName,
                Matches = new List<Match>()
            };

            tournament.Users.Add(user);
        }

        public static void StartTournament(Guid tournamentId)
        {
            var tournament = Tournaments[tournamentId];
            if (tournament == null)
            {
                return;
            }

            var users = tournament.Users;
            var matches = new List<Match>();
            var half = users.Count / 2;
            for(var i = 0; i < half; i++)
            {
                var player1 = users[i];
                var player2 = users[i + half];
                var match = new Match
                {
                    Player1 = player1,
                    Player2 = player2,
                    Rounds = new List<Round>()
                };
                player1.Matches.Add(match);
                player2.Matches.Add(match);

                matches.Add(match);
            }

            tournament.Matches = matches;
        }   

        public static void UserPlayRound(Guid tournamentId, Guid userId, ActionEnum player1Action)
        {
            var tournament = Tournaments[tournamentId];
            if (tournament == null)
            {
                return;
            }

            var user = tournament.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return;
            }

            var match = user.Matches.LastOrDefault();
            if (match == null)
            {
                return;
            }

            var round = new Round
            {
                Player1Action = player1Action,
                Player2Action = player2Action,
                Player1Score = GetScore(player1Action, player2Action),
                Player2Score = GetScore(player2Action, player1Action)
            };

            match.Rounds.Add(round);
        }
    }
}
