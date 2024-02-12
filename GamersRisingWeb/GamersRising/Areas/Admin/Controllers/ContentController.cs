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

    public class ContentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContentController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Create(int gameId, int tournamentId)
        {
            Content content = new Content
            {
                GameId = gameId,
                TourneyId = tournamentId
            };
            return View(content);
        }

        // POST: Admin/Content/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,HTMLContent,VideoLink,TourneyId,GameId")] Content content)
        {
            if (ModelState.IsValid)
            {
                content.Tournament = await _context.Tournament.FindAsync(content.TourneyId);
                _context.Add(content);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Tournaments", new  {gameId = content.GameId});
            }
            return View(content);
        }

        // GET: Admin/Content/Edit/5
        public async Task<IActionResult> Edit(int gameId, int tournamentId)
        {
            if (tournamentId == null || _context.Content == null)
            {
                return NotFound();
            }

            var content = await _context.Content.SingleOrDefaultAsync(item => item.Tournament.Id == tournamentId);
            content.GameId = gameId;
            content.TourneyId = tournamentId;
            if (content == null)
            {
                return NotFound();
            }
            return View(content);
        }

        // POST: Admin/Content/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,HTMLContent,VideoLink,TourneyId,GameId")] Content content)
        {
            if (id != content.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(content);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentExists(content.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), "Tournaments", new {gameId = content.GameId});
            }
            return View(content);
        }

        private bool ContentExists(int id)
        {
          return (_context.Content?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
