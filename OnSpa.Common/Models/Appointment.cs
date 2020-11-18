using System;
using System.Collections.Generic;
using System.Text;

namespace OnSpa.Common.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public bool IsAvailable { get; set; }

        public DateTime DateLocal => Date.ToLocalTime();

        public Service Service { get; set; }
        public string EmployeeId { get; set; }

        public User User { get; set; }
    }
}
