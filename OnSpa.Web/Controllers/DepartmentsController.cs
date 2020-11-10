using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnSpa.Web.Data;
using OnSpa.Web.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnSpa.Web.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly DataContext _context;

        public DepartmentsController(DataContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Departments
                 .Include(c => c.Cities)
                 .ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Department Department = await _context.Departments
                 .Include(c => c.Cities)
                  .ThenInclude(d => d.Campuses)
                 .FirstOrDefaultAsync(m => m.Id == id);
            if (Department == null)
            {
                return NotFound();
            }

            return View(Department);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(department);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }


                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }

            }
            return View(department);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Department department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(department);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Department department = await _context.Departments
                .Include(c => c.Cities)
                .ThenInclude(d => d.Campuses)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> AddCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Department department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            City model = new City { IdDepartment = department.Id };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCity(City city)
        {
            if (ModelState.IsValid)
            {
                Department department = await _context.Departments
                    .Include(c => c.Cities)
                    .FirstOrDefaultAsync(c => c.Id == city.IdDepartment);
                if (department == null)
                {
                    return NotFound();
                }

                try
                {
                    city.Id = 0;
                    department.Cities.Add(city);
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(Details)}/{department.Id}");

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(city);
        }

        public async Task<IActionResult> EditCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            City city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            Department department = await _context.Departments.FirstOrDefaultAsync(c => c.Cities.FirstOrDefault(d => d.Id == city.Id) != null);
            city.IdDepartment = department.Id;
            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCity(City city)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(city);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(Details)}/{city.IdDepartment}");

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(city);
        }

        public async Task<IActionResult> DetailsCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            City city = await _context.Cities
               .Include(d => d.Campuses)
               .FirstOrDefaultAsync(m => m.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            Department department = await _context.Departments.FirstOrDefaultAsync(c => c.Cities.FirstOrDefault(d => d.Id == city.Id) != null);
            city.IdDepartment = department.Id;
            return View(city);
        }

        public async Task<IActionResult> DeleteCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            City city = await _context.Cities
                .Include(d => d.Campuses)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            Department department = await _context.Departments.FirstOrDefaultAsync(c => c.Cities.FirstOrDefault(d => d.Id == city.Id) != null);
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(Details)}/{department.Id}");
        }

        public async Task<IActionResult> AddCampus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            City city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            Campus model = new Campus { IdCity = city.Id };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCampus(Campus campus)
        {
            if (ModelState.IsValid)
            {
                City city = await _context.Cities
                    .Include(d => d.Campuses)
                    .FirstOrDefaultAsync(c => c.Id == campus.IdCity);
                if (city == null)
                {
                    return NotFound();
                }

                try
                {
                    campus.Id = 0;
                    city.Campuses.Add(campus);
                    _context.Update(city);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(DetailsCity)}/{city.Id}");

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(campus);
        }

        public async Task<IActionResult> EditCampus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Campus campus = await _context.Campuses.FindAsync(id);
            if (campus == null)
            {
                return NotFound();
            }

            City city = await _context.Cities.FirstOrDefaultAsync(d => d.Campuses.FirstOrDefault(c => c.Id == campus.Id) != null);
            campus.IdCity = city.Id;
            return View(campus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCampus(Campus campus)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campus);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(DetailsCity)}/{campus.IdCity}");

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(campus);
        }

        public async Task<IActionResult> DeleteCampus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Campus campus = await _context.Campuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campus == null)
            {
                return NotFound();
            }

            City city = await _context.Cities.FirstOrDefaultAsync(d => d.Campuses.FirstOrDefault(c => c.Id == campus.Id) != null);
            _context.Campuses.Remove(campus);
            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(DetailsCity)}/{city.Id}");
        }
    }
}
