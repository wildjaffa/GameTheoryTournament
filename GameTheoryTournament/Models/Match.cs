using GameTheoryTournament.Models.Enums;
using GameTheoryTournament.Services;

namespace GameTheoryTournament.Models
{
    public class Match
    {
        public User Player1 { get; set; }
        public User Player2 { get; set; }
        public List<Round> Rounds { get; set; }
        public MatchStateEnum State { get
            {
                if (Rounds.Count == 0)
                {
                    return MatchStateEnum.NotStarted;
                }
                if (Rounds.Count == GameTheoryService.MatchLength)
                {
                    return MatchStateEnum.Finished;
                }
                return MatchStateEnum.InProgress;
            } 
        }
        public int Player1Score { get { return Rounds.Sum(r => r.Player1Score ?? 0); } }
        public int Player2Score { get { return Rounds.Sum(r => r.Player2Score ?? 0); } }
        public int CurrentRound { get { return Rounds.Count; } }
    }
}
