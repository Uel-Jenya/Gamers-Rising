using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GamersRising.Data;
using GamersRising.Entities;
using GamersRising.Extensions;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace GamersRising.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class TournamentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TournamentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Tournaments
        public async Task<IActionResult> Index(int gameId)
        {
            //var applicationDbContext = _context.Tournament.Include(t => t.Game).Include(t => t.TournamentFormat).Include(t => t.TournamentMode).Include(t => t.TournamentType);
            //return View(await applicationDbContext.ToListAsync());

            ViewBag.gameId = gameId;

            List<Tournament> list = await (from catItem in _context.Tournament
                                           join contentItem in _context.Content
                                           on catItem.Id equals contentItem.Tournament.Id
                                           into gj
                                           from subContent in gj.DefaultIfEmpty()
                                           where catItem.GameId == gameId
                                           select new Tournament
                                           {
                                               Id = catItem.Id,
                                               Title = catItem.Title,
                                               Description = catItem.Description,
                                               StartTime = catItem.StartTime,
                                               EndTime = catItem.EndTime,
                                               TournamentFormat = catItem.TournamentFormat,
                                               TournamentFormatId = catItem.TournamentFormatId,
                                               TournamentMode = catItem.TournamentMode,
                                               TournamentModeId = catItem.TournamentModeId,
                                               TournamentType = catItem.TournamentType,
                                               TournamentTypeId = catItem.TournamentTypeId,
                                               Game = catItem.Game,
                                               GameId = catItem.GameId,
                                               ContentId = (subContent != null) ? subContent.Id : 0
                                           }).ToListAsync();

            return View(list);
        }

        // GET: Admin/Tournaments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tournament == null)
            {
                return NotFound();
            }

            var tournament = await _context.Tournament
                .Include(t => t.Game)
                .Include(t => t.TournamentFormat)
                .Include(t => t.TournamentMode)
                .Include(t => t.TournamentType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tournament == null)
            {
                return NotFound();
            }

            return View(tournament);
        }

        // GET: Admin/Tournaments/Create
        public async Task<IActionResult> Create(int gameId)
        {
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Title");
            ViewData["TournamentFormatId"] = new SelectList(_context.TournamentFormat, "Id", "ThumbnailImagePath");
            ViewData["TournamentModeId"] = new SelectList(_context.TournamentMode, "Id", "ThumbnailImagePath");
            ViewData["TournamentTypeId"] = new SelectList(_context.TournamentType, "Id", "ThumbnailImagePath");

            List<TournamentType> tournamentTypes = await _context.TournamentType.ToListAsync();
            List<TournamentMode> tournamentModes = await _context.TournamentMode.ToListAsync();
            List<TournamentFormat> tournamentFormats = await _context.TournamentFormat.ToListAsync();

            Tournament tournament = new Tournament
            {
                GameId = gameId,
                TournamentTypes = tournamentTypes.ConvertToSelectList(0),
                TournamentModes = tournamentModes.ConvertToSelectList(0),
                TournamentFormats = tournamentFormats.ConvertToSelectList(0),
            


            };
            return View(tournament);
        }

        // POST: Admin/Tournaments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,GameId,TournamentFormatId,TournamentModeId,TournamentTypeId,StartTime,EndTime")] Tournament tournament)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tournament);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { gameId = tournament.GameId});
            }
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Title", tournament.GameId);
            ViewData["TournamentFormatId"] = new SelectList(_context.TournamentFormat, "Id", "ThumbnailImagePath", tournament.TournamentFormatId);
            ViewData["TournamentModeId"] = new SelectList(_context.TournamentMode, "Id", "ThumbnailImagePath", tournament.TournamentModeId);
            ViewData["TournamentTypeId"] = new SelectList(_context.TournamentType, "Id", "ThumbnailImagePath", tournament.TournamentTypeId);
            return View(tournament);
        }

        // GET: Admin/Tournaments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tournament == null)
            {
                return NotFound();
            }


            List<TournamentType> tournamentTypes = await _context.TournamentType.ToListAsync();
            List<TournamentMode> tournamentModes = await _context.TournamentMode.ToListAsync();
            List<TournamentFormat> tournamentFormats = await _context.TournamentFormat.ToListAsync();


            var tournament = await _context.Tournament.FindAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }

            tournament.TournamentTypes = tournamentTypes.ConvertToSelectList(tournament.TournamentTypeId);
            tournament.TournamentModes = tournamentModes.ConvertToSelectList(tournament.TournamentModeId);
            tournament.TournamentFormats = tournamentFormats.ConvertToSelectList(tournament.TournamentFormatId);

            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Title", tournament.GameId);
            ViewData["TournamentFormatId"] = new SelectList(_context.TournamentFormat, "Id", "ThumbnailImagePath", tournament.TournamentFormatId);
            ViewData["TournamentModeId"] = new SelectList(_context.TournamentMode, "Id", "ThumbnailImagePath", tournament.TournamentModeId);
            ViewData["TournamentTypeId"] = new SelectList(_context.TournamentType, "Id", "ThumbnailImagePath", tournament.TournamentTypeId);
            return View(tournament);
        }

        // POST: Admin/Tournaments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,GameId,TournamentFormatId,TournamentModeId,TournamentTypeId,StartTime,EndTime")] Tournament tournament)
        {
            if (id != tournament.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tournament);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TournamentExists(tournament.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { gameId = tournament.GameId });
            }
            ViewData["GameId"] = new SelectList(_context.Game, "Id", "Title", tournament.GameId);
            ViewData["TournamentFormatId"] = new SelectList(_context.TournamentFormat, "Id", "ThumbnailImagePath", tournament.TournamentFormatId);
            ViewData["TournamentModeId"] = new SelectList(_context.TournamentMode, "Id", "ThumbnailImagePath", tournament.TournamentModeId);
            ViewData["TournamentTypeId"] = new SelectList(_context.TournamentType, "Id", "ThumbnailImagePath", tournament.TournamentTypeId);
            return View(tournament);
        }

        // GET: Admin/Tournaments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tournament == null)
            {
                return NotFound();
            }

            var tournament = await _context.Tournament
                .Include(t => t.Game)
                .Include(t => t.TournamentFormat)
                .Include(t => t.TournamentMode)
                .Include(t => t.TournamentType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tournament == null)
            {
                return NotFound();
            }

            return View(tournament);
        }

        // POST: Admin/Tournaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tournament == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tournament'  is null.");
            }
            var tournament = await _context.Tournament.FindAsync(id);
            if (tournament != null)
            {
                _context.Tournament.Remove(tournament);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { gameId = tournament.GameId });
        }

        private bool TournamentExists(int id)
        {
          return (_context.Tournament?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
