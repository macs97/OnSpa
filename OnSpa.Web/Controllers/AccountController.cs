using Microsoft.AspNetCore.Mvc;
using OnSpa.Web.Data;
using OnSpa.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnSpa.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext _context;
       




        public AccountController(DataContext context)

        {
            _context = context;
        

        }


        
        public IActionResult NotAuthorized()
        {
            return View();
        }
    }
}
