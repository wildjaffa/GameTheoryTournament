namespace GameTheoryTournament.Models
{
    public class Tournament
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public List<Match> Matches { get; set; }
    }
}
