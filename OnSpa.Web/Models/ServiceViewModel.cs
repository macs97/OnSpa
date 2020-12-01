using Microsoft.AspNetCore.Http;
using OnSpa.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace OnSpa.Web.Models
{
    public class ServiceViewModel : Service
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

    }
}
