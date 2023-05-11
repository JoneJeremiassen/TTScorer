namespace TTScorer.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Score> Scores { get; set; }
    }
}