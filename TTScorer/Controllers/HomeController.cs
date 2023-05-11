using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TTScorer.Models;
using Microsoft.EntityFrameworkCore;

namespace TTScorer.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        var players = _dbContext.Players.ToList();
        return View(players);
    }

    public async Task<IActionResult> Results()
    {
        var recentMatches = await _dbContext.Matches
            .Include(m => m.Scores)
            .ThenInclude(p => p.Player)
            .Take(10)
            .ToListAsync();

        return View(recentMatches);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public async Task<IActionResult> RegisterScore(ScoreEntryViewModel scoreEntry)
    {
        Player player1 = _dbContext.Players.Find(scoreEntry.Player1Id);
        Player player2 = _dbContext.Players.Find(scoreEntry.Player2Id);

        Match newMatch = new Match()
        {
            MatchDate = DateTime.Now
        };

        _dbContext.Matches.Add(newMatch);
        _dbContext.SaveChanges();

        var matchId = _dbContext.Matches.Find(newMatch.Id);

        Score player1Score = new Score()
        {
            Match = matchId,
            Player = player1,
            Points = scoreEntry.Player1Score
        };

        Score player2Score = new Score()
        {
            Match = matchId,
            Player = player2,
            Points = scoreEntry.Player2Score
        };

        _dbContext.Scores.Add(player1Score);
        _dbContext.Scores.Add(player2Score);
        _dbContext.SaveChanges();

        if (!ModelState.IsValid) return View("Index");

        return RedirectToAction("Index");
    }
}