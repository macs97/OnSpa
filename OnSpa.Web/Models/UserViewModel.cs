using Microsoft.AspNetCore.Http;
using OnSpa.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnSpa.Web.Models
{
    public class UserViewModel : User
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

    }
}
