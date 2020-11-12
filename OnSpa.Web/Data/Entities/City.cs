using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace OnSpa.Web.Data.Entities
{
    public class City
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "The filed {0} must contain less than {1} characteres.")]
        [Required]
        public string Name { get; set; }
       
        public ICollection<Campus> Campuses { get; set; }
        [DisplayName("Cities Number")]
        public int CampusesNumber => Campuses == null ? 0 : Campuses.Count;

        
        [JsonIgnore]
        [NotMapped]
        public int IdDepartment { get; set; }

        [JsonIgnore]
        public Department Department { get; set; }
        [Display(Name = "# Users")]
        public int UsersNumber => Campuses == null ? 0 : Campuses.Sum(c => c.UsersNumber);

    }
}
