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
	public class TrainingsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public TrainingsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Trainings
		public async Task<IActionResult> Index()
		{
			return _context.Trainings != null ?
						View(await _context.Trainings.ToListAsync()) :
						Problem("Entity set 'ApplicationDbContext.Training'  is null.");
		}

		// GET: Trainings/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Trainings == null)
			{
				return NotFound();
			}

			var training = await _context.Trainings
				.FirstOrDefaultAsync(m => m.Id == id);
			if (training == null)
			{
				return NotFound();
			}

			return View(training);
		}

		// GET: Trainings/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Trainings/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Title,Description,TrainingDate")] Training training)
		{
			if (ModelState.IsValid)
			{
				_context.Add(training);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(training);
		}

		// GET: Trainings/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Trainings == null)
			{
				return NotFound();
			}

			var training = await _context.Trainings.FindAsync(id);
			if (training == null)
			{
				return NotFound();
			}
			return View(training);
		}

		// POST: Trainings/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,TrainingDate")] Training training)
		{
			if (id != training.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(training);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!TrainingExists(training.Id))
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
			return View(training);
		}

		// GET: Trainings/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Trainings == null)
			{
				return NotFound();
			}

			var training = await _context.Trainings
				.FirstOrDefaultAsync(m => m.Id == id);
			if (training == null)
			{
				return NotFound();
			}

			return View(training);
		}

		// POST: Trainings/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Trainings == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Training'  is null.");
			}
			var training = await _context.Trainings.FindAsync(id);
			if (training != null)
			{
				_context.Trainings.Remove(training);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool TrainingExists(int id)
		{
			return (_context.Trainings?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}