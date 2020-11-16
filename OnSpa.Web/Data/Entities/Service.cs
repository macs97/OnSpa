﻿using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace OnSpa.Web.Data.Entities
{
    public class Service
    {
        public int Id { get; set; }
        [Display(Name = "Service ")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; } 
        public Collection<Appointment> Appointments { get; set; }
        public Collection<ServiceType> ServiceTypes { get; set; }

    }
}
