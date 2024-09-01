using GameTheoryTournament.Models.Enums;
using GameTheoryTournament.Services;

namespace GameTheoryTournament.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public List<Match> Matches { get; set; } = new();
        public int Score { get { return Matches.Sum(m => m.Player1 == this ? m.Player1Score : m.Player2Score); } }
        public PlayerStatusEnum PlayerStatusEnum { get; set; } = PlayerStatusEnum.WaitingMatch;
    }
}
