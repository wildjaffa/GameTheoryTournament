using GameTheoryTournament.Models.Enums;

namespace GameTheoryTournament.Models
{
    public class Round
    {
        public User Player1 { get; set; }
        public User Player2 { get; set; }
        public ActionEnum? Player1Action { get; set; }
        public ActionEnum? Player2Action { get; set; }
        public int? Player1Score { get; set; }
        public int? Player2Score { get; set; }

        public void CalculateAction ()
        {
            if (Player1Action == ActionEnum.Cooperate && Player2Action == ActionEnum.Cooperate)
            {
                Player1Score = 1;
                Player2Score = 1;
            }
            else if (Player1Action == ActionEnum.Cooperate && Player2Action == ActionEnum.Defect)
            {
                Player1Score = 0;
                Player2Score = 2;
            }
            else if (Player1Action == ActionEnum.Defect && Player2Action == ActionEnum.Cooperate)
            {
                Player1Score = 2;
                Player2Score = 0;
            }
            else
            {
                Player1Score = 0;
                Player2Score = 0;
            }
        }
    }
}
