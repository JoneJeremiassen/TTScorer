namespace TTScorer.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class TableTennisScorer
{
    [Column(Order = 1)]
    public int Id { get; set; }

    [Column(Order = 2)]
    public string Player1 { get; set; }

    [Column(Order = 3)]
    public string Player2 { get; set; }

    [Column(Order = 4)]
    public int Score1 { get; set; }
    
    [Column(Order = 5)]
    public int Score2 { get; set; }
}