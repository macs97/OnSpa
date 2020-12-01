using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnSpa.Common.Request
{
    public class AssignRequest
    {
        [Required]
        public int AppointmentId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        public string EmployeeId { get; set; }
    }

}
