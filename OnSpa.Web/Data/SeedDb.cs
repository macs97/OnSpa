using OnSpa.Common.Enums;
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
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Esneider", "Cano", "esneiderclon17@gmail.com", "322 311 4620", "Calle Luna Calle Sol", UserType.Admin);
            await CheckUserAsync("1011", "Marcela", "Cardona", "marcela@yopmail.com", "322 315 4620", "Calle estrella Calle Sol", UserType.Admin);
            await CheckUserAsync("1012", "Heiber", "Bedoya", "heiber@yopmail.com", "322 313 4620", "Calle tierra Calle Sol", UserType.Admin);
        }
        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.Costumer.ToString());
        }

        private async Task<User> CheckUserAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            string address,
            UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    Campus = _context.Campuses.FirstOrDefault(),
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);

            }

            return user;
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
