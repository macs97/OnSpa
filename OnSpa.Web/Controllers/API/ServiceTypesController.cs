using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnSpa.Web.Data;
using OnSpa.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace OnSpa.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypesController : ControllerBase
    {

        private readonly DataContext _context;

        public ServiceTypesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetServiceTypes/{serviceId}")]
        public IActionResult GetServiceTypes(int? serviceId)
        {
            List<ServiceType> services = _context.ServiceTypes
                .Include(s => s.ServiceImages)
                .Where(s => s.Service.Id == serviceId).ToList();
            return Ok(services);
        }
    }
}
