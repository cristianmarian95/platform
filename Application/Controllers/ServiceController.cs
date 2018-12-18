using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.Helpers;
using Application.Models;

namespace Application.Controllers
{
  
    public class ServiceController : Controller
    {
        private readonly dbEntities _db = new dbEntities();

        [HttpGet]
        [Permission(Groups = "Admin")]
        public ActionResult Index()
        {
            return View(new ServiceListModel
            {
                Services = _db.Services.ToList()
            });
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult View(Guid? id = null)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (_db.Services.Any(Service => Service.Id.Equals(id.Value)))
            {
                return View(_db.Services.FirstOrDefault(Service => Service.Id.Equals(id.Value)));
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [Permission(Groups = "Admin")]
        public ActionResult Create()
        {
            return View(new ServiceCreateModel
            {
                Categories = _db.Categories.ToList()
            });
        }

        [HttpGet]
        [Permission(Groups = "Admin")]
        public ActionResult Edit(Guid id)
        {
            var service = _db.Services.FirstOrDefault(x => x.Id.Equals(id));

            if (service == null)
            {
                return RedirectToAction("Index", "Service");
            }

            var model = new ServiceEditModel()
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                Price = service.Price,
                Categories = _db.Categories.ToList(),
                FkCategory = service.FK_Category.ToString()
            };

            return View(model);
        }

        [HttpGet]
        [Permission(Groups = "Admin")]
        public ActionResult Delete(Guid id)
        {
            var service = _db.Services.FirstOrDefault(x => x.Id.Equals(id));

            if (service == null)
            {
                TempData["danger"] = "Error";
                return RedirectToAction("Index", "Service");
            }

            _db.Services.Remove(service);
            _db.SaveChanges();

            TempData["success"] = "Service deleted";
            return RedirectToAction("Index", "Service");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateService(ServiceCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["danger"] = "Error";
                return RedirectToAction("Index", "Service");
            }

            if (model.Description == null)
            {
                TempData["danger"] = "Error";
                return RedirectToAction("Index", "Service");
            }

            var categoryId = Guid.Parse(model.FkCategory);
            var service = new Service
            {
                Category = _db.Categories.FirstOrDefault(x => x.Id.Equals(categoryId)),
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                FK_Category = categoryId
            };

            _db.Services.Add(service);
            _db.SaveChanges();

            TempData["success"] = "Service create";
            return RedirectToAction("Index", "Service");
        }

        [HttpPost]
        [ValidateInput(false)]
        [Permission(Groups = "Admin")]
        public ActionResult EditService(ServiceEditModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["danger"] = "Error";
                return RedirectToAction("Index", "Service");
            }

            if (model.Description == null)
            {
                TempData["danger"] = "Error";
                return RedirectToAction("Index", "Service");
            }

            var service = _db.Services.FirstOrDefault(x => x.Id.Equals(model.Id));
            if (service == null)
            {
                TempData["danger"] = "Error";
                return RedirectToAction("Index", "Service");
            }

            var catId = Guid.Parse(model.FkCategory);

            service.Category = _db.Categories.FirstOrDefault(x => x.Id.Equals(catId));
            service.FK_Category = Guid.Parse(model.FkCategory);
            service.Description = model.Description;
            service.Name = model.Name;
            service.Price = model.Price;
            _db.Entry(service).State = EntityState.Modified;
            _db.SaveChanges();

            TempData["success"] = "Service edited";
            return RedirectToAction("Index", "Service");
        }




    }
}