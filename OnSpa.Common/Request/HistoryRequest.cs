using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnSpa.Common.Request
{
    public class HistoryRequest
    {
        [Required]
        public string CustomerId { get; set; }
    }
}
