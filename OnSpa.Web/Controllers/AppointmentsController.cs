using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnSpa.Web.Data;
using OnSpa.Web.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnSpa.Web.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly DataContext _context;
        private readonly IAppointmentHelper _appointmentHelper;

        public AppointmentsController(DataContext context, IAppointmentHelper appointmentHelper)
        {
            _context = context;
            _appointmentHelper = appointmentHelper;
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
    }
}
