using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnSpa.Common.Enums;
using OnSpa.Common.Request;
using OnSpa.Common.Responses;
using OnSpa.Web.Data;
using OnSpa.Web.Data.Entities;
using OnSpa.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnSpa.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AppointmentsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;
        private readonly IUserHelper _userHelper;

        public AppointmentsController(DataContext context, IConverterHelper converterHelper, IUserHelper userHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
            _userHelper = userHelper;
        }

        [HttpPost]
        [Route("GetAgendaForCustomer")]
        public async Task<IActionResult> GetAgendaForCustomer(EmailRequest emailRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appointments = await _context.Appointments
                .Include(a => a.User)
                .Include(a => a.Service)
                .Where(a => a.Date >= DateTime.Today.ToUniversalTime())
                .OrderBy(a => a.Date)
                .ToListAsync();

            var response = new List<AppointmentResponse>();
            foreach (var appointment in appointments)
            {
                var appointmentResponse = new AppointmentResponse
                {
                    Date = appointment.Date,
                    Id = appointment.Id,
                    IsActive = appointment.IsAvailable
                };

                if (appointment.User != null)
                {
                    if (appointment.User.Email.ToLower().Equals(emailRequest.Email.ToLower()))
                    {
                        appointmentResponse.User = _converterHelper.ToUserResponse(appointment.User);
                        appointmentResponse.Service = _converterHelper.ToServiceResponse(appointment.Service);
                    }
                    else
                    {
                        appointmentResponse.User = new UserResponse { FirstName = "Reserved" };
                    }
                }

                response.Add(appointmentResponse);
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("AssignAppointment")]
        public async Task<IActionResult> AssignAppointment(AssignRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appointment = await _context.Appointments.FindAsync(request.AppointmentId);
            if (appointment == null)
            {
                return BadRequest("Appointment doesn't exists.");
            }

            if (!appointment.IsAvailable)
            {
                return BadRequest("Appointment is not available.");
            }

            User customer = await _userHelper.GetUserAsync(new Guid(request.UserId));
            //User customer = await _userHelper.GetUserAsync(request.UserId);
            if (customer == null || !customer.UserType.Equals(UserType.Customer))
            {
                return BadRequest("Customer doesn't exists.");
            }

            var service = await _context.Services.FindAsync(request.ServiceId);
            if (service == null)
            {
                return BadRequest("Service doesn't exists.");
            }

            appointment.IsAvailable = false;
            appointment.User = customer;
            appointment.Service = service;
            appointment.EmployeeId = request.EmployeeId;

            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            appointment.User.Appointments = null;
            appointment.Service.Appointments = null;
            return Ok(appointment);
        }

        [HttpPost]
        [Route("UnAssignAppointment")]
        public async Task<IActionResult> UnAssignAppointment(UnAssignRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appointment = await _context.Appointments
                .Include(a => a.User)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(a => a.Id == request.AppointmentId);
            if (appointment == null)
            {
                return BadRequest("Appointment doesn't exists.");
            }

            if (appointment.IsAvailable)
            {
                return BadRequest("Appointment is available.");
            }

            appointment.IsAvailable = true;
            appointment.User = null;
            appointment.Service = null;

            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return Ok(appointment);
        }

        [HttpPost]
        [Route("HistoryByCustomer")]
        public async Task<IActionResult> HistoryByCustomer(HistoryRequest historyRequest)
        {
            List<Appointment> appointments = await _context.Appointments
                .Include(a => a.User)
                .Where(a => a.User.Email.Equals(historyRequest.CustomerId)).ToListAsync();
            appointments.ForEach(a => a.User.Appointments = null);
            return Ok(appointments);
        }

    }
}
