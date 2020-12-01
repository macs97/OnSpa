using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnSpa.Web.Data;
using OnSpa.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnSpa.Web.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly DataContext _context;

        public ServicesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            List<Service> services = await _context.Services
                .Include(p => p.ServiceImages)
                .Where(p => p.IsActive)
                .ToListAsync();
            return Ok(services);
        }
    }
}