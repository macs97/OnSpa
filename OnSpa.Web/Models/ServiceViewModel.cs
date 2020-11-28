using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnSpa.Web.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnSpa.Web.Models
{
    public class ServiceViewModel : Service
    {
            [Display(Name = "ServiceType")]
            [Range(1, int.MaxValue, ErrorMessage = "You must select a ServiceType.")]
            [Required]
            public int ServiceTypeId { get; set; }

            public IEnumerable<SelectListItem> ServiceTypes { get; set; }

           [Display(Name = "Image")]
           public IFormFile ImageFile { get; set; }

           [Display(Name = "Price")]
           [MaxLength(12)]
           [RegularExpression(@"^\d+([\.\,]?\d+)?$", ErrorMessage = "Use only numbers and . or , to put decimals")]

           [Required]
           public string PriceString { get; set; }

    }
}
