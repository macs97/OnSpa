using OnSpa.Common.Enums;
using OnSpa.Common.Models;
using OnSpa.Common.Services;
using OnSpa.Web.Data.Entities;
using OnSpa.Web.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnSpa.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IApiService _apiService;
        private readonly Random _random;


        public SeedDb(DataContext context, IUserHelper userHelper, IBlobHelper blobHelper, IApiService apiService)
        {
            _context = context;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
            _apiService = apiService;
            _random = new Random();

        }


        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckDeparmentsAsync();
            await CheckRolesAsync();
            await CheckUsersAsync();
          //  await CheckAppointmentsAsync();

        }




        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.Customer.ToString());
            await _userHelper.CheckRoleAsync(UserType.Employee.ToString());
        }

        private async Task CheckUsersAsync()
        {
            if (!_context.Users.Any())
            {
                await CheckAdminsAsync();
                await CheckBuyersAsync();
            }
        }

        private async Task CheckBuyersAsync()
        {
            for (int i = 1; i <= 50; i++)
            {
                await CheckUserAsync($"100{i}", $"customer{i}@yopmail.com", UserType.Customer);
            }
            for (int i = 1; i <= 10; i++)
            {
                await CheckUserAsync($"300{i}", $"employee{i}@yopmail.com", UserType.Employee);
            }
        }

        private async Task CheckAdminsAsync()
        {
            await CheckUserAsync("1001", "admin1@yopmail.com", UserType.Admin);
        }

        private async Task<Data.Entities.User> CheckUserAsync(
            string document,
            string email,
            UserType userType)
        {
            RandomUsers randomUsers;

            do
            {
                randomUsers = await _apiService.GetRandomUser("https://randomuser.me", "api");
            } while (randomUsers == null);

            Guid imageId = Guid.Empty;
            RandomUser randomUser = randomUsers.Results.FirstOrDefault();
            string imageUrl = randomUser.Picture.Large.ToString().Substring(22);
            Stream stream = await _apiService.GetPictureAsync("https://randomuser.me", imageUrl);
            if (stream != null)
            {
                imageId = await _blobHelper.UploadBlobAsync(stream, "users");
            }

            int campusId = _random.Next(1, _context.Campuses.Count());
            Data.Entities.User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new Data.Entities.User
                {
                    FirstName = randomUser.Name.First,
                    LastName = randomUser.Name.Last,
                    Email = email,
                    UserName = email,
                    PhoneNumber = randomUser.Cell,
                    Address = $"{randomUser.Location.Street.Number}, {randomUser.Location.Street.Name}",
                    Document = document,
                    UserType = userType,
                    ImageId = imageId
                    //Latitude = double.Parse(randomUser.Location.Coordinates.Latitude),
                    //Logitude = double.Parse(randomUser.Location.Coordinates.Longitude)
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
                _context.Departments.Add(new Data.Entities.Department
                {
                    Name = "Antioquia",
                    Cities = new List<Data.Entities.City>
                {
                    new Data.Entities.City
                    {
                        Name = "Medellin",
                        Campuses = new List<Data.Entities.Campus>
                        {
                            new Data.Entities.Campus { Name = "Campus 1" },
                            new Data.Entities.Campus { Name = "Campus 2" },
                            new Data.Entities.Campus { Name = "Campus 3" }
                        }
                    },
                    new Data.Entities.City
                    {
                        Name = "Bello",
                        Campuses = new List<Data.Entities.Campus>
                        {
                            new Data.Entities.Campus { Name = "Campus 4" },
                            new Data.Entities.Campus { Name = "Campus 5" },
                            new Data.Entities.Campus { Name = "Campus 6" }
                        }
                    }
                   }
                });
                await _context.SaveChangesAsync();
            }
        }


        private async Task CheckAppointmentsAsync()
        {
            if (!_context.Appointments.Any())
            {
                var initialDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
                var finalDate = initialDate.AddYears(1);
                while (initialDate < finalDate)
                {
                    if (initialDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        var finalDate2 = initialDate.AddHours(10);
                        while (initialDate < finalDate2)
                        {
                            _context.Appointments.Add(new Entities.Appointment
                            {
                                Date = initialDate,
                                IsAvailable = true
                            });

                            initialDate = initialDate.AddMinutes(30);
                        }

                        initialDate = initialDate.AddHours(14);
                    }
                    else
                    {
                        initialDate = initialDate.AddDays(1);
                    }
                }
            }

            await _context.SaveChangesAsync();
        }
    }

}
