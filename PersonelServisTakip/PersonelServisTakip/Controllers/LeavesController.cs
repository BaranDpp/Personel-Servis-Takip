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
	public class LeavesController : Controller
	{
		private readonly ApplicationDbContext _context;

		public LeavesController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Leaves
		public async Task<IActionResult> Index()
		{
			var applicationDbContext = _context.Leaves.Include(l => l.Personel);
			return View(await applicationDbContext.ToListAsync());
		}

		// GET: Leaves/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Leaves == null)
			{
				return NotFound();
			}

			var leave = await _context.Leaves
				.Include(l => l.Personel)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (leave == null)
			{
				return NotFound();
			}

			return View(leave);
		}

		// GET: Leaves/Create
		public IActionResult Create()
		{
			ViewData["PersonelId"] = new SelectList(_context.Personels, "Id", "Id");
			return View();
		}

		// POST: Leaves/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,PersonelId,StartDate,EndDate,Reason")] Leave leave)
		{
			if (ModelState.IsValid)
			{
				_context.Add(leave);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["PersonelId"] = new SelectList(_context.Personels, "Id", "Id", leave.PersonelId);
			return View(leave);
		}

		// GET: Leaves/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Leaves == null)
			{
				return NotFound();
			}

			var leave = await _context.Leaves.FindAsync(id);
			if (leave == null)
			{
				return NotFound();
			}
			ViewData["PersonelId"] = new SelectList(_context.Personels, "Id", "Id", leave.PersonelId);
			return View(leave);
		}

		// POST: Leaves/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,PersonelId,StartDate,EndDate,Reason")] Leave leave)
		{
			if (id != leave.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(leave);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!LeaveExists(leave.Id))
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
			ViewData["PersonelId"] = new SelectList(_context.Personels, "Id", "Id", leave.PersonelId);
			return View(leave);
		}

		// GET: Leaves/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Leaves == null)
			{
				return NotFound();
			}

			var leave = await _context.Leaves
				.Include(l => l.Personel)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (leave == null)
			{
				return NotFound();
			}

			return View(leave);
		}

		// POST: Leaves/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Leaves == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Leave'  is null.");
			}
			var leave = await _context.Leaves.FindAsync(id);
			if (leave != null)
			{
				_context.Leaves.Remove(leave);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool LeaveExists(int id)
		{
			return (_context.Leaves?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}