using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnSpa.Web.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string Document { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [Display(Name = "Phone Number")]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        [Display(Name = "Image")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://onspa.blob.core.windows.net/users/images/noimage.png"
            : $"https://onspa.blob.core.windows.net/users/{ImageId}";

        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

        [DisplayFormat(DataFormatString = "{0:N4}")]
        public double Latitude { get; set; }

        [DisplayFormat(DataFormatString = "{0:N4}")]
        public double Logitude { get; set; }

    }

}
