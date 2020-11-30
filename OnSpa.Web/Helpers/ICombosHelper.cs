using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnSpa.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboDepartments();

        IEnumerable<SelectListItem> GetComboCities(int departmentId);

        IEnumerable<SelectListItem> GetComboCampuses(int cityId);

        IEnumerable<SelectListItem> GetComboServices();

        IEnumerable<SelectListItem> GetComboServiceTypes();

        
       

    }
}
