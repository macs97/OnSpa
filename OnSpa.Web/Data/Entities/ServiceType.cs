using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public string ImageFullPath => ImageId == Guid.Empty
          ? $"https://onspa.blob.core.windows.net/service-types/images/noimage.png"
          : $"https://onspa.blob.core.windows.net/service-types/{ImageId}";

        [JsonIgnore]
        public Service Services { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Price { get; set; }

        public ICollection<ServiceTypeCampus> ServiceTypeCampuses { get; set; }

    }
}
