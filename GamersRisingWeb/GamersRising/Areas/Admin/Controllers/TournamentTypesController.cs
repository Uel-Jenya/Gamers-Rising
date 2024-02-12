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

    public class TournamentTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TournamentTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/TournamentTypes
        public async Task<IActionResult> Index()
        {
              return _context.TournamentType != null ? 
                          View(await _context.TournamentType.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TournamentType'  is null.");
        }

        // GET: Admin/TournamentTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TournamentType == null)
            {
                return NotFound();
            }

            var tournamentType = await _context.TournamentType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tournamentType == null)
            {
                return NotFound();
            }

            return View(tournamentType);
        }

        // GET: Admin/TournamentTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TournamentTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ThumbnailImagePath")] TournamentType tournamentType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tournamentType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tournamentType);
        }

        // GET: Admin/TournamentTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TournamentType == null)
            {
                return NotFound();
            }

            var tournamentType = await _context.TournamentType.FindAsync(id);
            if (tournamentType == null)
            {
                return NotFound();
            }
            return View(tournamentType);
        }

        // POST: Admin/TournamentTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ThumbnailImagePath")] TournamentType tournamentType)
        {
            if (id != tournamentType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tournamentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TournamentTypeExists(tournamentType.Id))
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
            return View(tournamentType);
        }

        // GET: Admin/TournamentTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TournamentType == null)
            {
                return NotFound();
            }

            var tournamentType = await _context.TournamentType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tournamentType == null)
            {
                return NotFound();
            }

            return View(tournamentType);
        }

        // POST: Admin/TournamentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TournamentType == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TournamentType'  is null.");
            }
            var tournamentType = await _context.TournamentType.FindAsync(id);
            if (tournamentType != null)
            {
                _context.TournamentType.Remove(tournamentType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TournamentTypeExists(int id)
        {
          return (_context.TournamentType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
