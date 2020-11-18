using System;
using System.ComponentModel.DataAnnotations;

namespace OnSpa.Web.Data.Entities
{
    public class Appointment
    {
        public int Id { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Is Available?")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}")]
        public DateTime DateLocal => Date.ToLocalTime();

      //  public Service Service { get; set; }
        //public string EmployeeId { get; set; }

        public User User { get; set; }
    }
}
