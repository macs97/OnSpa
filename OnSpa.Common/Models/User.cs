using OnSpa.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnSpa.Common.Models
{
    public class User
    {
        public string Id { get; set; }

        public string Document { get; set; }

        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Address { get; set; }

        public Guid ImageId { get; set; }
        public string ImageFullPath => ImageId == Guid.Empty
          ? $"https://onspa.blob.core.windows.net/users/images/noimage.png"
          : $"https://onspa.blob.core.windows.net/users/{ImageId}";

        public ICollection<Appointment> Appointments { get; set; }

        public UserType UserType { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";
    }
}
