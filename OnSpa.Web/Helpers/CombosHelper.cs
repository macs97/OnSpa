using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnSpa.Web.Data;
using OnSpa.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace OnSpa.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetComboCampuses(int cityId)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            City city = _context.Cities
                .Include(d => d.Campuses)
                .FirstOrDefault(d => d.Id == cityId);
            if (city != null)
            {
                list = city.Campuses.Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = $"{t.Id}"
                })
                    .OrderBy(t => t.Text)
                    .ToList();
            }

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a Campus...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboDepartments()        {

            List<SelectListItem> list = _context.Departments.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"
            })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a Department...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboCities(int departmentId)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            Department department = _context.Departments
                .Include(c => c.Cities)
                .FirstOrDefault(c => c.Id == departmentId);
            if (department != null)
            {
                list = department.Cities.Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = $"{t.Id}"
                })
                    .OrderBy(t => t.Text)
                    .ToList();
            }

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a City...]",
                Value = "0"
            });

            return list;
        }


    }
}
