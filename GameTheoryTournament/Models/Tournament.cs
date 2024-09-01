namespace GameTheoryTournament.Models
{
    public class Tournament
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public List<User> Users { get; set; } = new();
        public List<Match> Matches { get; set; } = new();
    }
}
