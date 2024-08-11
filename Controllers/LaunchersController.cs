using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IronDome.Data;
using IronDome.Models;

namespace IronDome.Controllers
{
    public class LaunchersController : Controller
    {
        private readonly IronDomeContextV3 _context;

        public LaunchersController(IronDomeContextV3 context)
        {
            _context = context;
        }

        // GET: Launchers
        [Route("Attacker/{attackerId}/Launchers/Index")]
        public async Task<IActionResult> Index(int attackerId)
        {
            if (attackerId == 0) return NotFound();
            var launchers = await _context.Launcher
                .Where(l => l.Attacker.Id == attackerId)
                .ToListAsync();
            if (launchers == null) return NotFound();
            var attacker = await _context.Attacker.FindAsync(attackerId);
            if (attacker == null) return NotFound();
            ViewData["AttackerName"] = attacker.Name;
            ViewData["AttackerId"] = attacker.Id;

            return View(launchers);
        }

        // GET: Launchers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var launcher = await _context.Launcher
                .FirstOrDefaultAsync(m => m.Id == id);
            if (launcher == null)
            {
                return NotFound();
            }

            return View(launcher);
        }

        // GET: Launchers/Create
        [Route("Attacker/{attackerId}/Launchers/Create")]
        public async Task<IActionResult> Create(int attackerId)
        {
            if (attackerId == 0) return NotFound();

            ViewData["AttackerId"] = attackerId;

            var launchers = await _context.Launcher
                .Where(l => l.Attacker.Id == attackerId)
                .ToListAsync();

            ViewData["Launchers"] = launchers;

            return View();
        }


        // POST: Launchers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Attacker/{attackerId}/Launchers/Create")]
        public async Task<IActionResult> Create(
            [Bind("Id,Name,Range,Velocity")] Launcher launcher,int attackerId)
        {
            if (attackerId == 0) return NotFound();
            ViewData["AttackerId"] = attackerId;

            ModelState.Remove("Attacker");
            if (ModelState.IsValid == false) return View(launcher);
            launcher.Attacker = await _context.Attacker.FindAsync(attackerId);
            _context.Add(launcher);
            await _context.SaveChangesAsync();
            // change to Attacker/{attackerId}/Launchers/Index
            return RedirectToAction("Index", new { attackerId = attackerId });
        }

        // GET: Launchers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var launcher = await _context.Launcher.FindAsync(id);
            if (launcher == null)
            {
                return NotFound();
            }
            return View(launcher);
        }

        // POST: Launchers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Range,Velocity")] Launcher launcher)
        {
            if (id != launcher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(launcher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LauncherExists(launcher.Id))
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
            return View(launcher);
        }

        // GET: Launchers/Delete/5
        [Route("Attacker/{attackerId}/Launchers/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var launcher = await _context.Launcher
                .FirstOrDefaultAsync(m => m.Id == id);
            if (launcher == null)
            {
                return NotFound();
            }

            return View(launcher);
        }

        // POST: Launchers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Attacker/{attackerId}/Launchers/Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id,int attackerId)
        {
            var launcher = await _context.Launcher.FindAsync(id);
            if (launcher != null)
            {
                _context.Launcher.Remove(launcher);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { attackerId = attackerId });

        }

        private bool LauncherExists(int id)
        {
            return _context.Launcher.Any(e => e.Id == id);
        }
    }
}
