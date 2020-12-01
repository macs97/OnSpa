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

        ServiceViewModel ToServiceViewModel(Service service);



    }
}