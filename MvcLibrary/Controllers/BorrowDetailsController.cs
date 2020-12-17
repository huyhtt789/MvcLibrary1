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
    public class BorrowDetailsController : Controller
    {
        private readonly MvcLibraryContext _context;

        public BorrowDetailsController(MvcLibraryContext context)
        {
            _context = context;
        }

        // GET: BorrowDetails
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var mvcLibraryContext = _context.BorrowDetails.Include(b => b.Book).Include(b => b.Borrowing);
            return View(await mvcLibraryContext.ToListAsync());
        }

        // GET: BorrowDetails/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowDetail = await _context.BorrowDetails
                .Include(b => b.Book)
                .Include(b => b.Borrowing)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowDetail == null)
            {
                return NotFound();
            }

            return View(borrowDetail);
        }

        // GET: BorrowDetails/Create
        public IActionResult Create()
        {
            ViewData["IdBook"] = new SelectList(_context.Books, "Id", "Id");
            ViewData["IdBorrowing"] = new SelectList(_context.Borrowings, "Id", "Id");
            return View();
        }

        // POST: BorrowDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BorrowDate,ReturnDate,IdBook,IdBorrowing")] BorrowDetail borrowDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(borrowDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBook"] = new SelectList(_context.Books, "Id", "Id", borrowDetail.IdBook);
            ViewData["IdBorrowing"] = new SelectList(_context.Borrowings, "Id", "Id", borrowDetail.IdBorrowing);
            return View(borrowDetail);
        }

        // GET: BorrowDetails/Edit/1
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowDetail = await _context.BorrowDetails.FindAsync(id);
            if (borrowDetail == null)
            {
                return NotFound();
            }
            ViewData["IdBook"] = new SelectList(_context.Books, "Id", "Id", borrowDetail.IdBook);
            ViewData["IdBorrowing"] = new SelectList(_context.Borrowings, "Id", "Id", borrowDetail.IdBorrowing);
            return View(borrowDetail);
        }

        // POST: BorrowDetails/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BorrowDate,ReturnDate,IdBook,IdBorrowing")] BorrowDetail borrowDetail)
        {
            if (id != borrowDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrowDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowDetailExists(borrowDetail.Id))
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
            ViewData["IdBook"] = new SelectList(_context.Books, "Id", "Id", borrowDetail.IdBook);
            ViewData["IdBorrowing"] = new SelectList(_context.Borrowings, "Id", "Id", borrowDetail.IdBorrowing);
            return View(borrowDetail);
        }

        // GET: BorrowDetails/Delete/1
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowDetail = await _context.BorrowDetails
                .Include(b => b.Book)
                .Include(b => b.Borrowing)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowDetail == null)
            {
                return NotFound();
            }

            return View(borrowDetail);
        }

        // POST: BorrowDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var borrowDetail = await _context.BorrowDetails.FindAsync(id);
            _context.BorrowDetails.Remove(borrowDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowDetailExists(int id)
        {
            return _context.BorrowDetails.Any(e => e.Id == id);
        }
    }
}
