using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace OnSpa.Web.Data.Entities
{
    public class ServiceType
    {
        public int Id { get; set; }

        [Display(Name = "Service Type ")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

         [Display(Name = "Image")]
        public string ImageFullPath => ServiceImages == null || ServiceImages.Count == 0
          ? $"https://onspa.blob.core.windows.net/images/noimage.png"
          : ServiceImages.FirstOrDefault().ImageFullPath;

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Price { get; set; }

        public ICollection<ServiceImage> ServiceImages { get; set; }

        [DisplayName("Service Type Images Number")]
        public int ServiceImagesNumber => ServiceImages == null ? 0 : ServiceImages.Count;

        public ICollection<ServiceTypeCampus> ServiceTypeCampuses { get; set; }

        public Service Service { get; set; }

    }
}
