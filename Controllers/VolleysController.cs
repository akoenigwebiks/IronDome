using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IronDome.Data;
using IronDome.Models;

namespace IronDome.Controllers
{
    public class VolleysController : Controller
    {
        private readonly IronDomeContextV3 _context;

        public VolleysController(IronDomeContextV3 context)
        {
            _context = context;
        }

        public async Task<List<Volley>> GetVolleysAsync(int attackerId) =>
            await _context.Volley
                    .Include(v => v.Launchers)
                    .Where(v => v.Launchers.Any(l => l.Attacker.Id == attackerId))
                    .ToListAsync();


        // GET: Volleys
        [Route("Volleys/Index/{attackerId}")]
        public async Task<IActionResult> Index(int attackerId)
        {
            if (attackerId == 0) return NotFound();

            var attacker = await _context.Attacker.FindAsync(attackerId);
            if (attacker == null) return NotFound();
            var volleys = await GetVolleysAsync(attackerId);

            if (volleys == null) return NotFound();
            ViewData["AttackerName"] = attacker.Name;
            ViewData["AttackerId"] = attacker.Id;
            return View(volleys);
        }


        // GET: Volleys/Details/5
        [Route("Volleys/Details/{volleyId}")]
        public async Task<IActionResult> Details(int? volleyId)
        {
            if (volleyId == null)
            {
                return NotFound();
            }

            var volley = await _context.Volley
                .FirstOrDefaultAsync(m => m.Id == volleyId);
            if (volley == null)
            {
                return NotFound();
            }

            return View(volley);
        }

        // GET: Volleys/Create
        [Route("Volleys/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Volleys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LaunchDate")] Volley volley)
        {
            if (ModelState.IsValid)
            {
                _context.Add(volley);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(volley);
        }

        // GET: Volleys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volley = await _context.Volley.FindAsync(id);
            if (volley == null)
            {
                return NotFound();
            }
            return View(volley);
        }

        // POST: Volleys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LaunchDate")] Volley volley)
        {
            if (id != volley.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(volley);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolleyExists(volley.Id))
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
            return View(volley);
        }

        // GET: Volleys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volley = await _context.Volley
                .FirstOrDefaultAsync(m => m.Id == id);
            if (volley == null)
            {
                return NotFound();
            }

            return View(volley);
        }

        // POST: Volleys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var volley = await _context.Volley.FindAsync(id);
            if (volley != null)
            {
                _context.Volley.Remove(volley);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VolleyExists(int id)
        {
            return _context.Volley.Any(e => e.Id == id);
        }
    }
}
