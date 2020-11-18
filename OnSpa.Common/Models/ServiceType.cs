using System;
using System.Collections.Generic;
using System.Text;

namespace OnSpa.Common.Models
{
    public class ServiceType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Guid ImageId { get; set; }

        public string ImageFullPath => ImageId == Guid.Empty
          ? $"https://onspa.blob.core.windows.net/service-types/images/noimage.png"
          : $"https://onspa.blob.core.windows.net/service-types/{ImageId}";

        public Service Services { get; set; }

        public decimal Price { get; set; }

        public ICollection<ServiceTypeCampus> ServiceTypeCampuses { get; set; }
    }
}
