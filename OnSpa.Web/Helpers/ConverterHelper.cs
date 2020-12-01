using Microsoft.EntityFrameworkCore;
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
