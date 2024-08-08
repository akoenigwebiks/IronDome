using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IronDome.Data;
using IronDome.Models;

namespace IronDome.Controllers
{
    public class DefensesController : Controller
    {
        private readonly IronDomeContext _context;

        public DefensesController(IronDomeContext context)
        {
            _context = context;
        }

        // GET: Defenses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Defense.ToListAsync());
        }

        // GET: Defenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defense = await _context.Defense
                .FirstOrDefaultAsync(m => m.ID == id);
            if (defense == null)
            {
                return NotFound();
            }

            return View(defense);
        }

        // GET: Defenses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Defenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Ammunition")] Defense defense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(defense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(defense);
        }

        // GET: Defenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defense = await _context.Defense.FindAsync(id);
            if (defense == null)
            {
                return NotFound();
            }
            return View(defense);
        }

        // POST: Defenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Ammunition")] Defense defense)
        {
            if (id != defense.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(defense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DefenseExists(defense.ID))
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
            return View(defense);
        }

        // GET: Defenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defense = await _context.Defense
                .FirstOrDefaultAsync(m => m.ID == id);
            if (defense == null)
            {
                return NotFound();
            }

            return View(defense);
        }

        // POST: Defenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var defense = await _context.Defense.FindAsync(id);
            if (defense != null)
            {
                _context.Defense.Remove(defense);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DefenseExists(int id)
        {
            return _context.Defense.Any(e => e.ID == id);
        }
    }
}
