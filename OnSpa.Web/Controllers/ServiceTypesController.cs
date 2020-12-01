    using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnSpa.Web.Data;
using OnSpa.Web.Data.Entities;
using OnSpa.Web.Helpers;
using OnSpa.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnSpa.Web.Controllers
{
    public class ServiceTypesController : Controller
    {
        private readonly DataContext _context;
        private readonly IBlobHelper _blobHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;

        public ServiceTypesController(DataContext context, IBlobHelper blobHelper, ICombosHelper combosHelper, IConverterHelper converterHelper)
        {
            _context = context;
            _blobHelper = blobHelper;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.ServiceTypes.Include(s => s.Service).Include(p => p.ServiceImages).ToListAsync());
        }


        public IActionResult Create()
        {
            ServiceTypeViewModel model = new ServiceTypeViewModel
            {
                Services = _combosHelper.GetComboServices()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;

                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "service-types");
                }

                try
                {
                    Service service = await _context.Services.FirstOrDefaultAsync(s => s.Id == model.ServiceId);
                    model.Service = service;
                    ServiceType serviceType = _converterHelper.ToServiceType(model, imageId, true);
                    serviceType.ServiceImages = new List<ServiceImage>
                    {
                        new ServiceImage { ImageId = imageId }
                    };
                    _context.Add(serviceType);
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
            model.Services = _combosHelper.GetComboServiceTypes();
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ServiceType serviceType = await _context.ServiceTypes
                .Include(p => p.ServiceImages)
                .FirstOrDefaultAsync(p => p.Id == id);


            if (serviceType == null)
            {
                return NotFound();
            }

            ServiceTypeViewModel model = _converterHelper.ToServiceTypeViewModel(serviceType);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ServiceTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = model.ImageId;

                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "service-types");
                }

                try
                {
                    ServiceType serviceType = _converterHelper.ToServiceType(model, imageId, false);
                    if (serviceType.ServiceImages == null)
                    {
                        serviceType.ServiceImages = new List<ServiceImage>();
                    }

                    serviceType.ServiceImages.Add(new ServiceImage { ImageId = imageId });
                    _context.Update(serviceType);
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
            model.Services = _combosHelper.GetComboServiceTypes();
            return View(model);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ServiceType serviceType = await _context.ServiceTypes
                .Include(p => p.ServiceImages)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceType == null)
            {
                return NotFound();
            }

            try
            {
                _context.ServiceTypes.Remove(serviceType);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ServiceType service = await _context.ServiceTypes
                .Include(c => c.ServiceImages)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        public async Task<IActionResult> AddImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Service service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            AddServiceImageViewModel model = new AddServiceImageViewModel { ServiceId = service.Id };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImage(AddServiceImageViewModel model)
        {
            if (ModelState.IsValid)
            {
                ServiceType service = await _context.ServiceTypes
                    .Include(p => p.ServiceImages)
                    .FirstOrDefaultAsync(p => p.Id == model.ServiceId);
                if (service == null)
                {
                    return NotFound();
                }

                try
                {
                    Guid imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "service-types");
                    if (service.ServiceImages == null)
                    {
                        service.ServiceImages = new List<ServiceImage>();
                    }

                    service.ServiceImages.Add(new ServiceImage { ImageId = imageId });
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(Details)}/{service.Id}");

                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(model);
        }


        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ServiceImage serviceImage = await _context.ServiceImages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceImage == null)
            {
                return NotFound();
            }

            ServiceType service = await _context.ServiceTypes.FirstOrDefaultAsync(p => p.ServiceImages.FirstOrDefault(pi => pi.Id == serviceImage.Id) != null);
            _context.ServiceImages.Remove(serviceImage);
            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(Details)}/{service.Id}");
        }
    }
}
