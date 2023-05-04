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
        return View();
    }

    public async Task<IActionResult> Results()
    {
        var recentMatches = await _dbContext.TableTennisScores.OrderByDescending(m => m.Id).Take(10).ToListAsync();
        return View(recentMatches);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public async Task<IActionResult> RegisterScore(TableTennisScorer scoreEntry)
    {
        if (!ModelState.IsValid) return View("Index", scoreEntry);
        
        _dbContext.TableTennisScores.Add(scoreEntry);
        await _dbContext.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}