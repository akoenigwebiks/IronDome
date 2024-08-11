using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IronDome.Data;
using IronDome.Models;

namespace IronDome.Controllers;
public class AttackersController : Controller
{
    private readonly IronDomeContextV3 _context;

    public AttackersController(IronDomeContextV3 context)
    {
        _context = context;
    }

    // GET: Attackers
    public async Task<IActionResult> Index()
    {
        var attackers = await _context.Attacker.ToListAsync();
        return View(attackers);
    }

    // GET: Attackers/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return BadRequest("Attacker ID is required.");

        var attacker = await _context.Attacker.FirstOrDefaultAsync(a => a.Id == id);
        if (attacker == null) return NotFound();

        return View(attacker);
    }

    // GET: Attackers/Create
    public IActionResult Create() => View();

    // POST: Attackers/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Distance")] Attacker attacker)
    {
        ModelState.Remove("Volleys");
        if (ModelState.IsValid == false) return View(attacker);
        _context.Attacker.Add(attacker);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: Attackers/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return BadRequest("Attacker ID is required.");

        var attacker = await _context.Attacker.FindAsync(id);
        if (attacker == null) return NotFound();

        return View(attacker);
    }

    // POST: Attackers/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Distance")] Attacker attacker)
    {
        if (id != attacker.Id) return BadRequest("Mismatched Attacker ID.");

        if (ModelState.IsValid == false) return View(attacker);
        try
        {
            _context.Update(attacker);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AttackerExists(attacker.Id))
            {
                return NotFound();
            }
            throw;
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Attackers/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return BadRequest("Attacker ID is required.");

        var attacker = await _context.Attacker.FirstOrDefaultAsync(a => a.Id == id);
        if (attacker == null) return NotFound();

        return View(attacker);
    }

    // POST: Attackers/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var attacker = await _context.Attacker.FindAsync(id);
        if (attacker != null)
        {
            _context.Attacker.Remove(attacker);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private bool AttackerExists(int id) => _context.Attacker.Any(e => e.Id == id);

}

