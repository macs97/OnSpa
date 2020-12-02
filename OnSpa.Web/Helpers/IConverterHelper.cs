using OnSpa.Common.Responses;
using OnSpa.Web.Data.Entities;
using OnSpa.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnSpa.Web.Helpers
{
    public interface IConverterHelper
    {
        User ToUser(UserViewModel model, Guid imageId, bool isNew);

        UserViewModel ToUserViewModel(User user);

        ServiceType ToServiceType(ServiceTypeViewModel model, Guid imageId, bool isNew);

        ServiceTypeViewModel ToServiceTypeViewModel(ServiceType serviceType);

        Service ToServiceAsync(ServiceViewModel model, bool isNew);

        ServiceResponse ToServiceResponse(Service service);

        UserResponse ToUserResponse(User user);

        ServiceViewModel ToServiceViewModel(Service service);

    }
}