using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnSpa.Web.Data;
using OnSpa.Web.Data.Entities;
using OnSpa.Web.Helpers;
using OnSpa.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnSpa.Web.Controllers
{
    public class ServicesController : Controller
    {
        private readonly DataContext _context;
        private readonly IBlobHelper _blobHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;

        public ServicesController(DataContext context, IBlobHelper blobHelper, ICombosHelper combosHelper, IConverterHelper converterHelper)
        {
            _context = context;
            _blobHelper = blobHelper;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Services
                .Include(p => p.ServiceType)
                .Include(p => p.ServiceImages)
                .ToListAsync());
        }


        public IActionResult Create()
        {
            ServiceViewModel model = new ServiceViewModel
            {
                ServiceTypes = _combosHelper.GetComboServiceTypes(),
                IsActive = true
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Service service = await _converterHelper.ToServiceAsync(model, true);

                    if (model.ImageFile != null)
                    {
                        Guid imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "service-types");
                        service.ServiceImages = new List<ServiceImage>
                        {
                          new ServiceImage { ImageId = imageId }
                         };
                    }

                    _context.Add(service);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            model.ServiceTypes = _combosHelper.GetComboServiceTypes();
            return View(model);
        }

    }
}