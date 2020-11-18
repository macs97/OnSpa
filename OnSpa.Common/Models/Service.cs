using System.Collections.Generic;

namespace OnSpa.Common.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<ServiceType> ServiceTypes { get; set; }
    }
}
