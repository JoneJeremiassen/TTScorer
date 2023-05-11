namespace TTScorer.Models;

public class Match
{
    public int Id { get; set; }
    public DateTime MatchDate { get; set; }

    public ICollection<Score> Scores { get; set; }

}