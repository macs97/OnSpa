using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnSpa.Common.Request
{
   public class EmailRequest
    { 
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}
