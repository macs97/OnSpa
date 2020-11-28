using OnSpa.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnSpa.Common.Responses
{
    public class UserResponse
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Document { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public Guid ImageId { get; set; }

        public string ImageFullPath { get; set; }


        public UserType UserType { get; set; }

        //public string ImageFullPath => ImageId == Guid.Empty
            //? $"https://onsalezulu.azurewebsites.net/images/noimage.png"
            //: $"https://onsale.blob.core.windows.net/users/{ImageId}";

        public UserType UserType { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

        public double Latitude { get; set; }

        public double Logitude { get; set; }
    }
}
