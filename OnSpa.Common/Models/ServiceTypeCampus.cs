using System;
using System.Collections.Generic;
using System.Text;

namespace OnSpa.Common.Models
{
    public class ServiceTypeCampus
    {
        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
        public int CampusId { get; set; }
        public Campus Campus { get; set; }
    }
}
