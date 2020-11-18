using System.Collections.Generic;

namespace OnSpa.Common.Models
{
    public class Campus
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int IdCity { get; set; }

        public City City { get; set; }

        public ICollection<ServiceTypeCampus> ServiceTypeCampuses { get; set; }
    }
}
