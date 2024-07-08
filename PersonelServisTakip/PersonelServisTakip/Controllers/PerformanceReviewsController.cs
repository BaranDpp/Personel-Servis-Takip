using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonelServisTakip.Models;
using PersonelServisTakip.Models.Entity;

namespace PersonelServisTakip.Controllers
{
	[Authorize]
	public class PerformanceReviewsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public PerformanceReviewsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: PerformanceReviews
		public async Task<IActionResult> Index()
		{
			var applicationDbContext = _context.PerformanceReviews.Include(p => p.Personel);
			return View(await applicationDbContext.ToListAsync());
		}

		// GET: PerformanceReviews/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.PerformanceReviews == null)
			{
				return NotFound();
			}

			var performanceReview = await _context.PerformanceReviews
				.Include(p => p.Personel)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (performanceReview == null)
			{
				return NotFound();
			}

			return View(performanceReview);
		}

		// GET: PerformanceReviews/Create
		public IActionResult Create()
		{
			ViewData["PersonelId"] = new SelectList(_context.Personels, "Id", "Id");
			return View();
		}

		// POST: PerformanceReviews/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,PersonelId,ReviewDate,Comments,Rating")] PerformanceReview performanceReview)
		{
			if (ModelState.IsValid)
			{
				_context.Add(performanceReview);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["PersonelId"] = new SelectList(_context.Personels, "Id", "Id", performanceReview.PersonelId);
			return View(performanceReview);
		}

		// GET: PerformanceReviews/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.PerformanceReviews == null)
			{
				return NotFound();
			}

			var performanceReview = await _context.PerformanceReviews.FindAsync(id);
			if (performanceReview == null)
			{
				return NotFound();
			}
			ViewData["PersonelId"] = new SelectList(_context.Personels, "Id", "Id", performanceReview.PersonelId);
			return View(performanceReview);
		}

		// POST: PerformanceReviews/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,PersonelId,ReviewDate,Comments,Rating")] PerformanceReview performanceReview)
		{
			if (id != performanceReview.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(performanceReview);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!PerformanceReviewExists(performanceReview.Id))
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
			ViewData["PersonelId"] = new SelectList(_context.Personels, "Id", "Id", performanceReview.PersonelId);
			return View(performanceReview);
		}

		// GET: PerformanceReviews/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.PerformanceReviews == null)
			{
				return NotFound();
			}

			var performanceReview = await _context.PerformanceReviews
				.Include(p => p.Personel)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (performanceReview == null)
			{
				return NotFound();
			}

			return View(performanceReview);
		}

		// POST: PerformanceReviews/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.PerformanceReviews == null)
			{
				return Problem("Entity set 'ApplicationDbContext.PerformanceReview'  is null.");
			}
			var performanceReview = await _context.PerformanceReviews.FindAsync(id);
			if (performanceReview != null)
			{
				_context.PerformanceReviews.Remove(performanceReview);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool PerformanceReviewExists(int id)
		{
			return (_context.PerformanceReviews?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}