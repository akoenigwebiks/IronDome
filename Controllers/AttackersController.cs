using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IronDome.Data;
using IronDome.Models;

namespace IronDome.Controllers
{
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
            return View(await _context.Attacker.ToListAsync());
        }

        // GET: Attackers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attacker = await _context.Attacker
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attacker == null)
            {
                return NotFound();
            }

            return View(attacker);
        }

        // GET: Attackers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Attackers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Distance")] Attacker attacker)
        {
            ModelState.Remove("Volleys");
            if (ModelState.IsValid)
            {
                _context.Add(attacker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(attacker);
        }

        // GET: Attackers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attacker = await _context.Attacker.FindAsync(id);
            if (attacker == null)
            {
                return NotFound();
            }
            return View(attacker);
        }

        // POST: Attackers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Distance")] Attacker attacker)
        {
            if (id != attacker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(attacker);
        }

        // GET: Attackers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attacker = await _context.Attacker
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attacker == null)
            {
                return NotFound();
            }

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
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttackerExists(int id)
        {
            return _context.Attacker.Any(e => e.Id == id);
        }
    }
}
