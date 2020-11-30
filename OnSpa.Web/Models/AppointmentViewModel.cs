using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnSpa.Web.Data.Entities;

namespace OnSpa.Web.Models
{
    public class AppointmentViewModel : Appointment
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Employee")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select an Employee.")]
     


        public IEnumerable<SelectListItem> Services { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Services")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Service.")]
        public int ServiceId { get; set; }


        public IEnumerable<SelectListItem> ServiceTypes { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Servicetypes")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Service type.")]
        public int ServiceTypeId { get; set; }
    }
}
