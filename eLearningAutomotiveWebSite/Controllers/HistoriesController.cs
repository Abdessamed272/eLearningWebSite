using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eLearningAutomotiveWebSite.Data;
using eLearningAutomotiveWebSite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace eLearningAutomotiveWebSite.Controllers
{
    public class HistoriesController : Controller
    {
        private readonly eLearningAutomotiveWebSiteContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HistoriesController(eLearningAutomotiveWebSiteContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //if (SignInManager.IsSignedIn(User))

        // GET: Histories
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var idcontents = _context.History.Where(x => x.IdUser == _userManager.GetUserId(HttpContext.User)).Select(y => y.IdContent);
            var ContentList = _context.Content.Where(c => idcontents.Contains(c.Id));
            ViewBag.contents = ContentList.ToList();

            var histories = _context.History.Where(x => x.IdUser == _userManager.GetUserId(HttpContext.User));


            return histories != null ? 
                          View(await histories.ToListAsync()) :
                          Problem("Entity set 'eLearningAutomotiveWebSiteContext.History'  is null.");
        }

        // GET: Histories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.History == null)
            {
                return NotFound();
            }

            var history = await _context.History
                .FirstOrDefaultAsync(m => m.Id == id);
            if (history == null)
            {
                return NotFound();
            }

            return View(history);
        }

        // GET: Histories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Histories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VisitDate,IdUser,IdContent")] History history)
        {
            if (ModelState.IsValid)
            {
                _context.Add(history);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(history);
        }

        // GET: Histories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.History == null)
            {
                return NotFound();
            }

            var history = await _context.History.FindAsync(id);
            if (history == null)
            {
                return NotFound();
            }
            return View(history);
        }

        // POST: Histories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VisitDate,IdUser,IdContent")] History history)
        {
            if (id != history.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(history);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistoryExists(history.Id))
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
            return View(history);
        }

        // GET: Histories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.History == null)
            {
                return NotFound();
            }

            var history = await _context.History
                .FirstOrDefaultAsync(m => m.Id == id);
            if (history == null)
            {
                return NotFound();
            }

            return View(history);
        }

        // POST: Histories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.History == null)
            {
                return Problem("Entity set 'eLearningAutomotiveWebSiteContext.History'  is null.");
            }
            var history = await _context.History.FindAsync(id);
            if (history != null)
            {
                _context.History.Remove(history);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoryExists(int id)
        {
          return (_context.History?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
