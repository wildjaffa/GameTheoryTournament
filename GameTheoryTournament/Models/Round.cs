using GameTheoryTournament.Models.Enums;

namespace GameTheoryTournament.Models
{
    public class Round
    {
        public ActionEnum? Player1Action { get; set; }
        public ActionEnum? Player2Action { get; set; }
        public int? Player1Score { get; set; }
        public int? Player2Score { get; set; }
    }
}
