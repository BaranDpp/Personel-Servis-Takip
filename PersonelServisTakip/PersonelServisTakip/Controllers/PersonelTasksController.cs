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
	public class PersonelTasksController : Controller
	{
		private readonly ApplicationDbContext _context;

		public PersonelTasksController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: PersonelTasks
		public async Task<IActionResult> Index()
		{
			var applicationDbContext = _context.PersonelTasks.Include(p => p.Personel);
			return View(await applicationDbContext.ToListAsync());
		}

		// GET: PersonelTasks/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.PersonelTasks == null)
			{
				return NotFound();
			}

			var personelTask = await _context.PersonelTasks
				.Include(p => p.Personel)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (personelTask == null)
			{
				return NotFound();
			}

			return View(personelTask);
		}

		// GET: PersonelTasks/Create
		public IActionResult Create()
		{
			ViewData["PersonelId"] = new SelectList(_context.Personels, "Id", "Id");
			return View();
		}

		// POST: PersonelTasks/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Title,Description,AssignedDate,DueDate,PersonelId")] PersonelTask personelTask)
		{
			if (ModelState.IsValid)
			{
				_context.Add(personelTask);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["PersonelId"] = new SelectList(_context.Personels, "Id", "Id", personelTask.PersonelId);
			return View(personelTask);
		}

		// GET: PersonelTasks/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.PersonelTasks == null)
			{
				return NotFound();
			}

			var personelTask = await _context.PersonelTasks.FindAsync(id);
			if (personelTask == null)
			{
				return NotFound();
			}
			ViewData["PersonelId"] = new SelectList(_context.Personels, "Id", "Id", personelTask.PersonelId);
			return View(personelTask);
		}

		// POST: PersonelTasks/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,AssignedDate,DueDate,PersonelId")] PersonelTask personelTask)
		{
			if (id != personelTask.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(personelTask);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!PersonelTaskExists(personelTask.Id))
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
			ViewData["PersonelId"] = new SelectList(_context.Personels, "Id", "Id", personelTask.PersonelId);
			return View(personelTask);
		}

		// GET: PersonelTasks/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.PersonelTasks == null)
			{
				return NotFound();
			}

			var personelTask = await _context.PersonelTasks
				.Include(p => p.Personel)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (personelTask == null)
			{
				return NotFound();
			}

			return View(personelTask);
		}

		// POST: PersonelTasks/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.PersonelTasks == null)
			{
				return Problem("Entity set 'ApplicationDbContext.PersonelTasks'  is null.");
			}
			var personelTask = await _context.PersonelTasks.FindAsync(id);
			if (personelTask != null)
			{
				_context.PersonelTasks.Remove(personelTask);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool PersonelTaskExists(int id)
		{
			return (_context.PersonelTasks?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}