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

    public class TournamentFormatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TournamentFormatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/TournamentFormats
        public async Task<IActionResult> Index()
        {
              return _context.TournamentFormat != null ? 
                          View(await _context.TournamentFormat.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TournamentFormat'  is null.");
        }

        // GET: Admin/TournamentFormats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TournamentFormat == null)
            {
                return NotFound();
            }

            var tournamentFormat = await _context.TournamentFormat
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tournamentFormat == null)
            {
                return NotFound();
            }

            return View(tournamentFormat);
        }

        // GET: Admin/TournamentFormats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TournamentFormats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ThumbnailImagePath")] TournamentFormat tournamentFormat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tournamentFormat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tournamentFormat);
        }

        // GET: Admin/TournamentFormats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TournamentFormat == null)
            {
                return NotFound();
            }

            var tournamentFormat = await _context.TournamentFormat.FindAsync(id);
            if (tournamentFormat == null)
            {
                return NotFound();
            }
            return View(tournamentFormat);
        }

        // POST: Admin/TournamentFormats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ThumbnailImagePath")] TournamentFormat tournamentFormat)
        {
            if (id != tournamentFormat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tournamentFormat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TournamentFormatExists(tournamentFormat.Id))
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
            return View(tournamentFormat);
        }

        // GET: Admin/TournamentFormats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TournamentFormat == null)
            {
                return NotFound();
            }

            var tournamentFormat = await _context.TournamentFormat
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tournamentFormat == null)
            {
                return NotFound();
            }

            return View(tournamentFormat);
        }

        // POST: Admin/TournamentFormats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TournamentFormat == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TournamentFormat'  is null.");
            }
            var tournamentFormat = await _context.TournamentFormat.FindAsync(id);
            if (tournamentFormat != null)
            {
                _context.TournamentFormat.Remove(tournamentFormat);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TournamentFormatExists(int id)
        {
          return (_context.TournamentFormat?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
