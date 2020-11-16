using Newtonsoft.Json;
using OnSpa.Common.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnSpa.Web.Data.Entities
{
    public class Campus
    {
        public int Id { get; set; }

        [Display(Name = "Campus")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        [JsonIgnore]
        [NotMapped]
        public ICollection<UserType> Users { get; set; }

        [JsonIgnore]  
        [Display(Name = "# Users")]
        public int UsersNumber => Users == null ? 0 : Users.Count;
        [JsonIgnore]
        [NotMapped]
        public int IdCity { get; set; }

        [JsonIgnore]
        public City City { get; set; }
        public ICollection<Appointment> Appointments { get; set; }

    }
}
