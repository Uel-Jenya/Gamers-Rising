using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GamersRising.Data;
using GamersRising.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace GamersRising.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class TournamentModesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TournamentModesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/TournamentModes
        public async Task<IActionResult> Index()
        {
              return _context.TournamentMode != null ? 
                          View(await _context.TournamentMode.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TournamentMode'  is null.");
        }

        // GET: Admin/TournamentModes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TournamentMode == null)
            {
                return NotFound();
            }

            var tournamentMode = await _context.TournamentMode
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tournamentMode == null)
            {
                return NotFound();
            }

            return View(tournamentMode);
        }

        // GET: Admin/TournamentModes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TournamentModes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ThumbnailImagePath")] TournamentMode tournamentMode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tournamentMode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tournamentMode);
        }

        // GET: Admin/TournamentModes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TournamentMode == null)
            {
                return NotFound();
            }

            var tournamentMode = await _context.TournamentMode.FindAsync(id);
            if (tournamentMode == null)
            {
                return NotFound();
            }
            return View(tournamentMode);
        }

        // POST: Admin/TournamentModes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ThumbnailImagePath")] TournamentMode tournamentMode)
        {
            if (id != tournamentMode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tournamentMode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TournamentModeExists(tournamentMode.Id))
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
            return View(tournamentMode);
        }

        // GET: Admin/TournamentModes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TournamentMode == null)
            {
                return NotFound();
            }

            var tournamentMode = await _context.TournamentMode
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tournamentMode == null)
            {
                return NotFound();
            }

            return View(tournamentMode);
        }

        // POST: Admin/TournamentModes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TournamentMode == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TournamentMode'  is null.");
            }
            var tournamentMode = await _context.TournamentMode.FindAsync(id);
            if (tournamentMode != null)
            {
                _context.TournamentMode.Remove(tournamentMode);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TournamentModeExists(int id)
        {
          return (_context.TournamentMode?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
