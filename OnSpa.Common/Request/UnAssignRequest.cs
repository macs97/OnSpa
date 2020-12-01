using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnSpa.Common.Request
{
    public class UnAssignRequest
    {
        [Required]
        public int AppointmentId { get; set; }
    }

}
