using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnSpa.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnSpa.Web.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]

    public class DepartmentController : ControllerBase
    {
        private readonly DataContext _context;
        public DepartmentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetDepartment()
        {
            return Ok(_context.Departments
                .Include(c => c.Cities)
                .ThenInclude(d => d.Campuses));
        }
    }

}

