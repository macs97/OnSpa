using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnSpa.Web.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnSpa.Web.Models
{
    public class ServiceTypeViewModel : ServiceType
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "Price")]
        [MaxLength(12)]
        [RegularExpression(@"^\d+([\.\,]?\d+)?$", ErrorMessage = "Use only numbers and . or , to put decimals")]

        [Required]
        public string PriceString { get; set; }

        [Display(Name = "Service")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Service.")]
        [Required]
        public int ServiceId { get; set; }

        public IEnumerable<SelectListItem> Services { get; set; }

    }
}
