using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcLibrary.Data;
using MvcLibrary.Models;

namespace MvcLibrary.Controllers
{
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "User")]
    public class BorrowingsController : Controller
    {
        private readonly MvcLibraryContext _context;

        public BorrowingsController(MvcLibraryContext context)
        {
            _context = context;
        }

        // GET: Borrowings
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var mvcLibraryContext = _context.Borrowings.Include(b => b.Borrower).Include(b => b.Librarian);
            return View(await mvcLibraryContext.ToListAsync());
        }

        // GET: Borrowings/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowings
                .Include(b => b.Borrower)
                .Include(b => b.Librarian)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowing == null)
            {
                return NotFound();
            }

            return View(borrowing);
        }

        // GET: Borrowings/Create
        public IActionResult Create()
        {
            ViewData["IdBorrower"] = new SelectList(_context.Borrowers, "Id", "Id");
            ViewData["IdLibrarian"] = new SelectList(_context.Librarians, "Id", "Id");
            return View();
        }

        // POST: Borrowings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreateDate,IdBorrower,IdLibrarian")] Borrowing borrowing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(borrowing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBorrower"] = new SelectList(_context.Borrowers, "Id", "Id", borrowing.IdBorrower);
            ViewData["IdLibrarian"] = new SelectList(_context.Librarians, "Id", "Id", borrowing.IdLibrarian);
            return View(borrowing);
        }

        // GET: Borrowings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowings.FindAsync(id);
            if (borrowing == null)
            {
                return NotFound();
            }
            ViewData["IdBorrower"] = new SelectList(_context.Borrowers, "Id", "Id", borrowing.IdBorrower);
            ViewData["IdLibrarian"] = new SelectList(_context.Librarians, "Id", "Id", borrowing.IdLibrarian);
            return View(borrowing);
        }

        // POST: Borrowings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreateDate,IdBorrower,IdLibrarian")] Borrowing borrowing)
        {
            if (id != borrowing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrowing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowingExists(borrowing.Id))
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
            ViewData["IdBorrower"] = new SelectList(_context.Borrowers, "Id", "Id", borrowing.IdBorrower);
            ViewData["IdLibrarian"] = new SelectList(_context.Librarians, "Id", "Id", borrowing.IdLibrarian);
            return View(borrowing);
        }

        // GET: Borrowings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowings
                .Include(b => b.Borrower)
                .Include(b => b.Librarian)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowing == null)
            {
                return NotFound();
            }

            return View(borrowing);
        }

        // POST: Borrowings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var borrowing = await _context.Borrowings.FindAsync(id);
            _context.Borrowings.Remove(borrowing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowingExists(int id)
        {
            return _context.Borrowings.Any(e => e.Id == id);
        }
    }
}
