using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTScorer.Models;

namespace TTScorer.Controllers;

public class PlayerController : Controller
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<PlayerController> _logger;

    public PlayerController(ILogger<PlayerController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<IActionResult> Index()
    {
        var players = await _dbContext.Players.ToListAsync();

        return View(players);
    }

    public ActionResult CreatePlayer()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlayer(Player player)
    {
        _dbContext.Players.Add(player);
        await _dbContext.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> PlayerDetails(int playerId)
    {
        var player = await _dbContext.Players.FindAsync(playerId);
        return View(player);
    }

    public async Task<IActionResult> DeletePlayer(int playerId)
    {
        var player = await _dbContext.Players.FindAsync(playerId);
        _dbContext.Players.Remove(player);
        await _dbContext.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}