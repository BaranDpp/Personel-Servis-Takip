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
	public class ServiceVehiclesController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ServiceVehiclesController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: ServiceVehicles
		public async Task<IActionResult> Index()
		{
			return _context.ServiceVehicles != null ?
						View(await _context.ServiceVehicles.ToListAsync()) :
						Problem("Entity set 'ApplicationDbContext.ServiceVehicles'  is null.");
		}

		// GET: ServiceVehicles/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.ServiceVehicles == null)
			{
				return NotFound();
			}

			var serviceVehicle = await _context.ServiceVehicles
				.FirstOrDefaultAsync(m => m.Id == id);
			if (serviceVehicle == null)
			{
				return NotFound();
			}

			return View(serviceVehicle);
		}

		// GET: ServiceVehicles/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: ServiceVehicles/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,VehicleNumber,DriverName,ServiceDate")] ServiceVehicle serviceVehicle)
		{
			if (ModelState.IsValid)
			{
				_context.Add(serviceVehicle);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(serviceVehicle);
		}

		// GET: ServiceVehicles/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.ServiceVehicles == null)
			{
				return NotFound();
			}

			var serviceVehicle = await _context.ServiceVehicles.FindAsync(id);
			if (serviceVehicle == null)
			{
				return NotFound();
			}
			return View(serviceVehicle);
		}

		// POST: ServiceVehicles/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,VehicleNumber,DriverName,ServiceDate")] ServiceVehicle serviceVehicle)
		{
			if (id != serviceVehicle.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(serviceVehicle);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ServiceVehicleExists(serviceVehicle.Id))
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
			return View(serviceVehicle);
		}

		// GET: ServiceVehicles/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.ServiceVehicles == null)
			{
				return NotFound();
			}

			var serviceVehicle = await _context.ServiceVehicles
				.FirstOrDefaultAsync(m => m.Id == id);
			if (serviceVehicle == null)
			{
				return NotFound();
			}

			return View(serviceVehicle);
		}

		// POST: ServiceVehicles/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.ServiceVehicles == null)
			{
				return Problem("Entity set 'ApplicationDbContext.ServiceVehicles'  is null.");
			}
			var serviceVehicle = await _context.ServiceVehicles.FindAsync(id);
			if (serviceVehicle != null)
			{
				_context.ServiceVehicles.Remove(serviceVehicle);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ServiceVehicleExists(int id)
		{
			return (_context.ServiceVehicles?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}