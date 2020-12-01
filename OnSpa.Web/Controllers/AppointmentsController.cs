using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnSpa.Web.Data;
using OnSpa.Web.Data.Entities;
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
        private readonly IUserHelper _userHelper;

        public AppointmentsController(DataContext context, IAppointmentHelper appointmentHelper, ICombosHelper combosHelper, IUserHelper userHelper)
        {
            _context = context;
            _appointmentHelper = appointmentHelper;
            _combosHelper = combosHelper;
            _userHelper = userHelper;
        }

        public IActionResult Index()
        {
            return View(_context.Appointments
                .Include(a => a.Service)
                .Include(a => a.User)
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
                Services = _combosHelper.GetComboServices(),
                Employees = _combosHelper.GetComboEmployees()
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
                    agenda.EmployeeId = model.EmployeeId;
                    agenda.User = await _userHelper.GetUserAsync(model.EmailCustomer); //Customer
                    agenda.Service = await _context.Services.FindAsync(model.ServiceId);
                    _context.Appointments.Update(agenda);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            model.Services = _combosHelper.GetComboServices();

            return View(model);
        }

        public async Task<JsonResult> GetServiceTypeAsync(int serviceId)
        {
            var serviceTypes = await _context.ServiceTypes
                .Where(s => s.Service.Id == serviceId)
                .OrderBy(p => p.Name)
                .ToListAsync();
            return Json(serviceTypes);
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
