using Microsoft.AspNetCore.Identity;
using OnSpa.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace OnSpa.Web.Data.Entities
{
    public class User : IdentityUser
    {
        [Display(Name = "Document")]
        [MaxLength(20, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Document { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string LastName { get; set; }

        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Address { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }
        public string ImageFullPath => ImageId == Guid.Empty
          // ? "https://localhost:44374/images/noimage.png"
          ? $"https://onchurch.azurewebsites.net/images/noimage.png" // falta organizar
          : $"https://onchurchtaller.blob.core.windows.net/users/{ImageId}";

        public ICollection<Appointment> Appointments { get; set; }


        [Display(Name = "User Type")]
        public UserType UserType { get; set; }

        public Campus Campus { get; set; }

        [Display(Name = "User")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "User")]
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

      //  public ICollection<Appointment> Appointments { get; set; }
    }
}
