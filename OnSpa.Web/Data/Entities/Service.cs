using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OnSpa.Web.Data.Entities
{
    public class Service
    {
        public int Id { get; set; }
        [Display(Name = "Service ")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Price { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        public ICollection<ServiceImage> ServiceImages { get; set; }

        [DisplayName("Service Images Number")]
        public int ServiceImagesNumber => ServiceImages == null ? 0 : ServiceImages.Count;

        [Display(Name = "Image")]
        public string ImageFullPath => ServiceImages == null || ServiceImages.Count == 0
            ? $"https://onspa.blob.core.windows.net/service-types/images/noimage.png"
            : ServiceImages.FirstOrDefault().ImageFullPath;

        public ICollection<Appointment> Appointments { get; set; }

        public ServiceType ServiceType { get; set; }

    }
}
