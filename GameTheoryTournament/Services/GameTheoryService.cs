using GameTheoryTournament.Models;
using GameTheoryTournament.Models.Enums;

namespace GameTheoryTournament.Services
{
    public class GameTheoryService
    {
        public static Dictionary<Guid, Tournament> Tournaments = new();

        public static readonly int MatchLength = 10;

        public Tournament? GetTournamentByName(string name)
        {
            return Tournaments.Values.FirstOrDefault(t => t.Name.ToLower() == name.ToLower());
        }

        public Tournament CreateTournament(string name)
        {
            var tournament = new Tournament
            {
                Id = Guid.NewGuid(),
                Name = name,
                Users = new List<User>(),
                Matches = new List<Match>()
            };

            Tournaments.Add(tournament.Id, tournament);
            return tournament;
        }

        public User AddUserToTournament(Guid tournamentId, string userName)
        {
            var tournament = Tournaments[tournamentId];
            if (tournament == null)
            {
                return null;
            }
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = userName,
                Matches = new List<Match>()
            };

            tournament.Users.Add(user);
            StateHasChanged(tournamentId);
            return user;
        }

        public void StartTournament(Guid tournamentId)
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
            StateHasChanged(tournamentId);
        }   

        public void UserPlayRound(User user, Round round, ActionEnum action, Match match, Tournament tournament)
        {
            if (round.Player1 == user)
            {
                round.Player1Action = action;
            }
            else if (round.Player2 == user)
            {
                round.Player2Action = action;
            }

            if (round.Player1Action == null || round.Player2Action == null)
            {
                return;
            }

            round.CalculateAction();
            if (match.Rounds.Count < MatchLength)
            {
                var newRound = new Round
                {
                    Player1 = round.Player1,
                    Player2 = round.Player2
                };
                match.Rounds.Add(newRound);
                StateHasChanged(tournament.Id);
                return;
            }

            ReassignUser(round.Player1, tournament);
            ReassignUser(round.Player2, tournament);

        }

        private void ReassignUser(User user, Tournament tournament)
        {
            user.PlayerStatusEnum = PlayerStatusEnum.WaitingMatch;
            if (user.Matches.Count == tournament.Users.Count - 1)
            {
                user.PlayerStatusEnum = PlayerStatusEnum.Finished;
                StateHasChanged(tournament.Id);
                return;
            }

            var playedUsers = user.Matches.SelectMany(m => new List<User> { m.Player1, m.Player2 }).Distinct().ToList();
            var waitingUsers = tournament.Users.Where(u => u.PlayerStatusEnum == PlayerStatusEnum.WaitingMatch && !playedUsers.Contains(u));
            if (!waitingUsers.Any())
            {
                return;
            }

            var opponent = waitingUsers.First();
            var match = new Match
            {
                Player1 = user,
                Player2 = opponent,
                Rounds = new List<Round>
                {
                    new() 
                    {
                        Player1 = user,
                        Player2 = waitingUsers.First()
                    }
                }
            };

            user.Matches.Add(match);
            opponent.Matches.Add(match);

            StateHasChanged(tournament.Id);
        }

        public async void StateHasChanged(Guid tournamentId)
        {
            if (Notify != null)
            {
                await Notify.Invoke(tournamentId);
            }
        }


        public event Func<Guid, Task>? Notify;
    }
}
