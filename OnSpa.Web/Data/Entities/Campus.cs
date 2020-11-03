using System.ComponentModel.DataAnnotations;

namespace OnSpa.Web.Data.Entities
{
    public class Campus
    {
        public int Id { get; set; }

        [Display(Name = "Campus")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }
    }
}
