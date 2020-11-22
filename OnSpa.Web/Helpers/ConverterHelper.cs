using OnSpa.Web.Data.Entities;
using OnSpa.Web.Models;
using System;

namespace OnSpa.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {



        public User ToUser(UserViewModel model, Guid imageId, bool isNew)
        {
            return new User
            {
                // Id = isNew ? 0 : model.Id,
                ImageId = imageId,
                //Name = model.Name
            };
        }

        public UserViewModel ToUserViewModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                ImageId = user.ImageId,
            };
        }

        public ServiceType ToServiceType(ServiceTypeViewModel model, Guid imageId, bool isNew)
        {
            return new ServiceType
            {
                Id = isNew ? 0 : model.Id,
                ImageId = imageId,
                Name = model.Name
            };
        }

        public ServiceTypeViewModel ToServiceTypeViewModel(ServiceType serviceType)
        {
            return new ServiceTypeViewModel
            {
                Id = serviceType.Id,
                ImageId = serviceType.ImageId,
                Name = serviceType.Name
            };
        }
    }
}
