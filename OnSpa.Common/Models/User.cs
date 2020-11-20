using OnSpa.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnSpa.Common.Models
{
    public class User
    {
        public string Id { get; set; }

        public string Document { get; set; }

        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Address { get; set; }

        public Guid ImageId { get; set; }
        public string ImageFullPath { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

        public UserType UserType { get; set; }

        public string FullName { get; set; }

        public string FullNameWithDocument { get; set; }
    }
}
