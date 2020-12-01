using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnSpa.Web.Data;
using OnSpa.Web.Helpers;
using OnSpa.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace OnSpa.Web.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly DataContext _context;
        private readonly IAppointmentHelper _appointmentHelper;
        private readonly ICombosHelper _combosHelper;

        public AppointmentsController(DataContext context, IAppointmentHelper appointmentHelper, ICombosHelper combosHelper)
        {
            _context = context;
            _appointmentHelper = appointmentHelper;
            _combosHelper = combosHelper;
        }

        public IActionResult Index()
        {
            return View(_context.Appointments
                .Include(a => a.Service)
                // .ThenInclude(o => o.User)
                .Where(a => a.Date >= DateTime.Today.ToUniversalTime()));
        }

        public async Task<IActionResult> AddDays()
        {
            await _appointmentHelper.AddDaysAsync(7);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Assing(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agenda = await _context.Appointments
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (agenda == null)
            {
                return NotFound();
            }

            var model = new AppointmentViewModel
            {
                Id = agenda.Id,
                ServiceTypes = _combosHelper.GetComboServiceTypes(),
                Services = _combosHelper.GetComboServices()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assing(AppointmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var agenda = await _context.Appointments.FindAsync(model.Id);
                if (agenda != null)
                {
                    agenda.IsAvailable = false;
                    agenda.User = await _context.Users.FindAsync(model.EmployeeId);
                    agenda.Service = await _context.Services.FindAsync(model.ServiceId);
                    _context.Appointments.Update(agenda);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            model.ServiceTypes = _combosHelper.GetComboServiceTypes();
            model.Services = _combosHelper.GetComboServices();

            return View(model);
        }


        public async Task<JsonResult> GetPetsAsync(int ownerId)
        {
            var Services = await _context.Services
                
                .OrderBy(p => p.Name)
                .ToListAsync();
            return Json(Services);
        }

        public async Task<IActionResult> Unassign(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agenda = await _context.Appointments
                
                .Include(a => a.Service)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (agenda == null)
            {
                return NotFound();
            }
            agenda.IsAvailable = true;
            agenda.Service= null;
            

            _context.Appointments.Update(agenda);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
