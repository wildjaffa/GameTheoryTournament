using GameTheoryTournament.Models.Enums;
using GameTheoryTournament.Services;

namespace GameTheoryTournament.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public List<Match> Matches { get; set; }
        public int Score { get { return Matches.Sum(m => m.Player1 == this ? m.Player1Score : m.Player2Score); } }
        public PlayerStatusEnum PlayerStatusEnum 
        { 
            get 
            {
                var lastMatch = Matches.LastOrDefault();
                if (lastMatch == null)
                {
                    return PlayerStatusEnum.WaitingMatch;
                }
                if (Matches.Count == GameTheoryService.MatchLength && 
                    lastMatch.State == MatchStateEnum.Finished)
                {
                    return PlayerStatusEnum.Finished;
                }
                if (lastMatch.State == MatchStateEnum.Finished)
                {
                    return PlayerStatusEnum.WaitingMatch;
                }
                return PlayerStatusEnum.Playing;

            }
        }
    }
}
