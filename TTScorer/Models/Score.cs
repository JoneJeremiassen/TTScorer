namespace TTScorer.Models;

public class Score
{
    public int Id { get; set; }
    public Player Player { get; set; }
    public Match Match { get; set; }
    public int Points { get; set; }
}