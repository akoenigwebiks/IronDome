using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IronDome.Data;
using IronDome.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IronDome.Controllers;

public class VolleysController : Controller
{
    private readonly IronDomeContextV3 _context;

    public VolleysController(IronDomeContextV3 context)
    {
        _context = context;
    }

    // GET: Volleys
    public async Task<IActionResult> Index(int attackerId)
    {
        var volleys = await _context.Volley
            .Include(v => v.Launchers) // Include related Launchers
            .Where(v => v.Launchers.Any(l => l.AttackerId == attackerId))
            .ToListAsync();

        ViewBag.AttackerId = attackerId;
        return View(volleys);
    }

    // GET: Volleys/Details/5
    public async Task<IActionResult> Details(int? id, int attackerId)
    {
        if (id == null) return BadRequest("Volley ID is required.");

        var volley = await _context.Volley
            .Include(v => v.Launchers) // Include related Launchers
            .FirstOrDefaultAsync(v => v.Id == id && v.Launchers.Any(l => l.AttackerId == attackerId));

        if (volley == null) return NotFound();

        ViewBag.AttackerId = attackerId;
        return View(volley);
    }

    // GET: Volleys/Create
    public IActionResult Create(int attackerId)
    {
        var vmCreateVolley = new VMCreateVolley
        {
            LauncherAmounts = _context.Launcher
                .Where(l => l.AttackerId == attackerId)
                .Select(l => new LauncherAmount
                {
                    LauncherId = l.Id,
                    LauncherName = l.Name,
                    Amount = 1 // Default amount
                })
                .ToList()
        };

        ViewBag.AttackerId = attackerId;
        return View(vmCreateVolley);
    }

    // POST: Volleys/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int attackerId, VMCreateVolley vmCreateVolley)
    {
        ModelState.Remove("Attacker");
        if (ModelState.IsValid == false)
        {
            ViewBag.AttackerId = attackerId;
            return View(vmCreateVolley);
        }

        var volley = new Volley
        {
            Launchers = new List<Launcher>()
        };

        foreach (var la in vmCreateVolley.LauncherAmounts)
        {
            var launcher = await _context.Launcher.FindAsync(la.LauncherId);
            if (launcher == null) continue;

            for (int i = 0; i < la.Amount; i++)
            {
                volley.Launchers.Add(launcher);
            }

        }
        volley.att
        _context.Volley.Add(volley);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new { attackerId });
    }

    // GET: Volleys/Edit/5
    public async Task<IActionResult> Edit(int? id, int attackerId)
    {
        if (id == null) return BadRequest("Volley ID is required.");

        var volley = await _context.Volley
            .Include(v => v.Launchers)
            .FirstOrDefaultAsync(v => v.Id == id && v.Launchers.Any(l => l.AttackerId == attackerId));

        if (volley == null) return NotFound();

        var vmCreateVolley = new VMCreateVolley
        {
            Id = volley.Id,
            LauncherAmounts = volley.Launchers
                .GroupBy(l => l.Id)
                .Select(group => new LauncherAmount
                {
                    LauncherId = group.Key,
                    LauncherName = group.First().Name,
                    Amount = group.Count()
                })
                .ToList()
        };

        ViewBag.AttackerId = attackerId;
        ViewBag.Launchers = _context.Launcher
            .Where(l => l.AttackerId == attackerId)
            .Select(l => new LauncherAmount
            {
                LauncherId = l.Id,
                LauncherName = l.Name,
                Amount = 0 // Default to 0; this will be set in the form
            })
            .ToList();

        return View(vmCreateVolley);
    }


    // POST: Volleys/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, int attackerId, [Bind("Id,LaunchDate,LauncherAmounts")] VMCreateVolley vmCreateVolley)
    {
        if (id != vmCreateVolley.Id) return BadRequest("Mismatched Volley ID.");

        if (ModelState.IsValid == false)
        {
            ViewBag.AttackerId = attackerId;
            ViewBag.Launchers = _context.Launcher
                .Where(l => l.AttackerId == attackerId)
                .Select(l => new LauncherAmount
                {
                    LauncherId = l.Id,
                    LauncherName = l.Name,
                    Amount = 0 // Set to 0, actual amounts are handled in the form
                })
                .ToList();
            return View(vmCreateVolley);
        }

        try
        {
            var volley = await _context.Volley
                .Include(v => v.Launchers)
                .FirstOrDefaultAsync(v => v.Id == id && v.Launchers.Any(l => l.AttackerId == attackerId));

            if (volley == null) return NotFound();

            volley.LaunchDate = vmCreateVolley.LaunchDate;

            // Clear existing launchers and add the new ones
            volley.Launchers.Clear();

            foreach (var la in vmCreateVolley.LauncherAmounts)
            {
                var launcher = await _context.Launcher.FindAsync(la.LauncherId);
                if (launcher != null)
                {
                    for (int i = 0; i < la.Amount; i++)
                    {
                        volley.Launchers.Add(launcher);
                    }
                }
            }

            _context.Update(volley);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!VolleyExists(vmCreateVolley.Id)) return NotFound();
            throw;
        }
        return RedirectToAction(nameof(Index), new { attackerId });
    }


    // GET: Volleys/Delete/5
    public async Task<IActionResult> Delete(int? id, int attackerId)
    {
        if (id == null) return BadRequest("Volley ID is required.");

        var volley = await _context.Volley
            .Include(v => v.Launchers)
            .FirstOrDefaultAsync(v => v.Id == id && v.Launchers.Any(l => l.AttackerId == attackerId));

        if (volley == null) return NotFound();

        ViewBag.AttackerId = attackerId;
        return View(volley);
    }

    // POST: Volleys/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, int attackerId)
    {
        var volley = await _context.Volley
            .Include(v => v.Launchers)
            .FirstOrDefaultAsync(v => v.Id == id && v.Launchers.Any(l => l.AttackerId == attackerId));

        if (volley != null)
        {
            _context.Volley.Remove(volley);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index), new { attackerId });
    }

    private bool VolleyExists(int id) =>
        _context.Volley.Any(v => v.Id == id);
}
