using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonelServisTakip.Models;
using PersonelServisTakip.Models.Entity;
using System.IO;
using System.Threading.Tasks;

namespace PersonelServisTakip.Controllers
{
    [Authorize]
    public class PersonelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Personels
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["VehicleSortParm"] = sortOrder == "Vehicle" ? "vehicle_desc" : "Vehicle";
            ViewData["DeptSortParm"] = sortOrder == "Dept" ? "dept_desc" : "Dept";
            ViewData["CurrentFilter"] = searchString;

            var personels = from p in _context.Personels
                            .Include(p => p.Department)
                            .Include(p => p.ServiceVehicle)
                            select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                personels = personels.Where(p => p.Name.Contains(searchString)
                                       || p.ServiceVehicle.VehicleNumber.Contains(searchString)
                                       || p.Department.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    personels = personels.OrderByDescending(p => p.Name);
                    break;

                case "Vehicle":
                    personels = personels.OrderBy(p => p.ServiceVehicle.VehicleNumber);
                    break;

                case "vehicle_desc":
                    personels = personels.OrderByDescending(p => p.ServiceVehicle.VehicleNumber);
                    break;

                case "Dept":
                    personels = personels.OrderBy(p => p.Department.Name);
                    break;

                case "dept_desc":
                    personels = personels.OrderByDescending(p => p.Department.Name);
                    break;

                default:
                    personels = personels.OrderBy(p => p.Name);
                    break;
            }

            return View(await personels.AsNoTracking().ToListAsync());
        }

        // GET: Personels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Personels == null)
            {
                return NotFound();
            }

            var personel = await _context.Personels
                .Include(p => p.Department)
                .Include(p => p.ServiceVehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personel == null)
            {
                return NotFound();
            }

            return View(personel);
        }

        // GET: Personels/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["ServiceVehicleId"] = new SelectList(_context.ServiceVehicles, "Id", "VehicleNumber");
            return View();
        }

        // POST: Personels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PhotoFile,ServiceVehicleId,DepartmentId")] Personel personel)
        {
            if (ModelState.IsValid)
            {
                if (personel.PhotoFile != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await personel.PhotoFile.CopyToAsync(memoryStream);
                        personel.Photo = memoryStream.ToArray();
                    }
                }

                _context.Add(personel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", personel.DepartmentId);
            ViewData["ServiceVehicleId"] = new SelectList(_context.ServiceVehicles, "Id", "VehicleNumber", personel.ServiceVehicleId);
            return View(personel);
        }

        // GET: Personels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Personels == null)
            {
                return NotFound();
            }

            var personel = await _context.Personels.FindAsync(id);
            if (personel == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", personel.DepartmentId);
            ViewData["ServiceVehicleId"] = new SelectList(_context.ServiceVehicles, "Id", "VehicleNumber", personel.ServiceVehicleId);
            return View(personel);
        }

        // POST: Personels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PhotoFile,ServiceVehicleId,DepartmentId")] Personel personel)
        {
            if (id != personel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (personel.PhotoFile != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await personel.PhotoFile.CopyToAsync(memoryStream);
                            personel.Photo = memoryStream.ToArray();
                        }
                    }

                    _context.Update(personel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonelExists(personel.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", personel.DepartmentId);
            ViewData["ServiceVehicleId"] = new SelectList(_context.ServiceVehicles, "Id", "VehicleNumber", personel.ServiceVehicleId);
            return View(personel);
        }

        // GET: Personels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Personels == null)
            {
                return NotFound();
            }

            var personel = await _context.Personels
                .Include(p => p.Department)
                .Include(p => p.ServiceVehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personel == null)
            {
                return NotFound();
            }

            return View(personel);
        }

        // POST: Personels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Personels == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Personels' is null.");
            }
            var personel = await _context.Personels.FindAsync(id);
            if (personel != null)
            {
                _context.Personels.Remove(personel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonelExists(int id)
        {
            return (_context.Personels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}