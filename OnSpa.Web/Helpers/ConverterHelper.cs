using Microsoft.EntityFrameworkCore;
using OnSpa.Common.Responses;
using OnSpa.Web.Data;
using OnSpa.Web.Data.Entities;
using OnSpa.Web.Models;
using System;
using System.Threading.Tasks;

namespace OnSpa.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(DataContext context, ICombosHelper combosHelper)
        {
           _context = context;
           _combosHelper = combosHelper;
        }

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
                Name = model.Name,
                Price = ToPrice(model.PriceString),
                Service = model.Service,
                ServiceImages = model.ServiceImages
            };
        }

        public ServiceTypeViewModel ToServiceTypeViewModel(ServiceType serviceType)
        {
            return new ServiceTypeViewModel
            {
                Id = serviceType.Id,
                ImageId = serviceType.ImageId,
                Name = serviceType.Name,
                Services = _combosHelper.GetComboServices(),
                Service = serviceType.Service,
                ServiceId = serviceType.Id,
                PriceString = $"{serviceType.Price}",
                ServiceImages = serviceType.ServiceImages
            };
        }

        public Service ToServiceAsync(ServiceViewModel model, bool isNew)
        {
            return new Service
            {
                Description = model.Description,
                Id = isNew ? 0 : model.Id,
                IsActive = model.IsActive,
                Name = model.Name
            };
        }

        public ServiceResponse ToServiceResponse(Service service)
        {
            if (service == null)
            {
                return null;
            }

            return new ServiceResponse
            {
                Id = service.Id,
                Name = service.Name,
                ImageFullPath = service.ImageFullPath
            };
        }

        public UserResponse ToUserResponse(User user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Document = user.Document,
                Address = user.Address,
                Email = user.Email,
                ImageFacebook = user.ImageFacebook,
                Latitude = user.Latitude,
                ImageId = user.ImageId,
                LoginType = user.LoginType,
                Logitude = user.Logitude,
                PhoneNumber = user.PhoneNumber,
                UserType = user.UserType
            };
        }

        private decimal ToPrice(string priceString)
        {
            string nds = ".";
            if (nds == ".")
            {
                priceString = priceString.Replace(',', '.');
            }
            else
            {
                priceString = priceString.Replace('.', ',');
            }

            return decimal.Parse(priceString);
        }



        public ServiceViewModel ToServiceViewModel(Service service)
        {
            return new ServiceViewModel
            {

                Description = service.Description,
                Id = service.Id,
                IsActive = service.IsActive,
                Name = service.Name,
                Price = service.Price,
                ImageId = service.ImageId
            };
        }
    }
}
