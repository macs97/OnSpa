using System;
using System.Collections.Generic;
using System.Text;

namespace OnSpa.Common.Models
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Campus> Campuses { get; set; }

        public int CampusNumber => Campuses == null ? 0 : Campuses.Count;

        public int IdDepartment { get; set; }

        public Department Department { get; set; }
    }
}
