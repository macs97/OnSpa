using System;
using System.ComponentModel.DataAnnotations;

namespace OnSpa.Web.Data.Entities
{
    public class ServiceImage
    {
        public int Id { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        [Display(Name = "Image")]

        public string ImageFullPath => ImageId == Guid.Empty
         ? $"https://onspa.blob.core.windows.net/service-types/images/noimage.png"
          : $"https://onspa.blob.core.windows.net/service-types/{ImageId}";
    }
}


