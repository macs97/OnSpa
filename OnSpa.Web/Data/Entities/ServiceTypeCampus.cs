using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnSpa.Web.Data.Entities
{
    public class ServiceTypeCampus
    {
        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
        public int CampusId { get; set; }
        public Campus Campus { get; set; }
    }
}
