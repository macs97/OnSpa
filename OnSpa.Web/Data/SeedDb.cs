using OnSpa.Web.Data.Entities;
using OnSpa.Web.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnSpa.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;

        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckDeparmentsAsync();
        }

        private async Task CheckDeparmentsAsync()
        {
            if (!_context.Departments.Any())
            {
                _context.Departments.Add(new Department
                {
                    Name = "Antioquia",
                    Cities = new List<City>
                {
                    new City
                    {
                        Name = "Medellin",
                        Campuses = new List<Campus>
                        {
                            new Campus { Name = "Campus 1" },
                            new Campus { Name = "Campus 2" },
                            new Campus { Name = "Campus 3" }
                        }
                    },
                    new City
                    {
                        Name = "Bogotá",
                        Campuses = new List<Campus>
                        {
                            new Campus { Name = "Campus 4" },
                            new Campus { Name = "Campus 5" },
                            new Campus { Name = "Campus 6" }
                        }
                    }
                   }
                });
                await _context.SaveChangesAsync();
            }
        }
    }

}
