using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IronDome.Data;
using IronDome.Models;

namespace IronDome.Controllers;

public class LaunchersController : Controller
{
    private readonly IronDomeContextV3 _context;

    public LaunchersController(IronDomeContextV3 context)
    {
        _context = context;
    }

    // GET: Launchers
    public async Task<IActionResult> Index(int attackerId)
    {
        var launchers = await _context.Launcher
            .Where(l => l.AttackerId == attackerId)
            .ToListAsync();

        ViewBag.AttackerId = attackerId;
        return View(launchers);
    }

    // GET: Launchers/Details/5
    public async Task<IActionResult> Details(int? id, int attackerId)
    {
        if (id == null) return BadRequest("Launcher ID is required.");

        var launcher = await _context.Launcher
            .FirstOrDefaultAsync(l => l.Id == id && l.AttackerId == attackerId);

        if (launcher == null) return NotFound();

        ViewBag.AttackerId = attackerId;
        return View(launcher);
    }

    // GET: Launchers/Create
    public IActionResult Create(int attackerId)
    {
        ViewBag.AttackerId = attackerId;
        return View();
    }

    // POST: Launchers/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int attackerId, [Bind("Name,Range,Velocity,AttackerId")] Launcher launcher)
    {
        ModelState.Remove("Attacker");
        if (ModelState.IsValid == false)
        {
            ViewBag.AttackerId = attackerId;
            return View(launcher);
        }

        launcher.AttackerId = attackerId;
        _context.Launcher.Add(launcher);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new { attackerId });
    }

    // GET: Launchers/Edit/5
    public async Task<IActionResult> Edit(int? id, int attackerId)
    {
        if (id == null) return BadRequest("Launcher ID is required.");

        var launcher = await _context.Launcher
            .FirstOrDefaultAsync(l => l.Id == id && l.AttackerId == attackerId);

        if (launcher == null) return NotFound();

        ViewBag.AttackerId = attackerId;
        return View(launcher);
    }

    // POST: Launchers/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, int attackerId, [Bind("Id,Name,Range,Velocity")] Launcher launcher)
    {
        if (id != launcher.Id) return BadRequest("Mismatched Launcher ID.");

        if (ModelState.IsValid == false) return View(launcher);

        try
        {
            launcher.AttackerId = attackerId;
            _context.Update(launcher);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LauncherExists(launcher.Id, attackerId)) return NotFound();
            throw;
        }
        return RedirectToAction(nameof(Index), new { attackerId });
    }

    // GET: Launchers/Delete/5
    public async Task<IActionResult> Delete(int? id, int attackerId)
    {
        if (id == null) return BadRequest("Launcher ID is required.");

        var launcher = await _context.Launcher
            .FirstOrDefaultAsync(l => l.Id == id && l.AttackerId == attackerId);

        if (launcher == null) return NotFound();

        ViewBag.AttackerId = attackerId;
        return View(launcher);
    }

    // POST: Launchers/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, int attackerId)
    {
        var launcher = await _context.Launcher
            .FirstOrDefaultAsync(l => l.Id == id && l.AttackerId == attackerId);

        if (launcher != null)
        {
            _context.Launcher.Remove(launcher);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index), new { attackerId });
    }

    private bool LauncherExists(int id, int attackerId) =>
        _context.Launcher.Any(l => l.Id == id && l.AttackerId == attackerId);
}
