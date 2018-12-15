using Application.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Application.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly dbEntities db = new dbEntities();

        [HttpGet]
        public ActionResult Index()
        {

            return View(new CategoryListModel
            {
                Categories = db.Categories.ToList()

            });
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(Guid Id)
        {
            var category = db.Categories.FirstOrDefault(x => x.Id.Equals(Id));

            if (category == null)
            {
                TempData["danger"] = "Invalid ID on Category";
                return RedirectToAction("Index", "Category");
            }

            return View(new CategoryEditModel
            {
                Name = category.Name,
                Id = category.Id
            });
        }

        [HttpGet]
        public ActionResult Delete(Guid Id)
        {
            var category = db.Categories.FirstOrDefault(x => x.Id.Equals(Id));

            if (category == null)
            {
                TempData["danger"] = "Invalid ID on Category";
                return RedirectToAction("Index", "Category");
            }

            db.Categories.Remove(category);
            db.SaveChanges();

            return RedirectToAction("Index", "Category");
        }

        [HttpPost]
        public ActionResult CreateAction(CategoryCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["danger"] = "Error";
                return RedirectToAction("Create", "Category");
            }

            Category cat = new Category
            {
                Name = model.Name,
                Id = Guid.NewGuid()
            };

            db.Categories.Add(cat);
            db.SaveChanges();

            return RedirectToAction("Index", "Category");
        }

        public ActionResult EditAction(CategoryEditModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["danger"] = "Error";
                return RedirectToAction("Create", "Category");
            }

            var category = db.Categories.FirstOrDefault(x => x.Id.Equals(model.Id));
            if (category == null)
            {
                TempData["danger"] = "Invalid ID";
                return RedirectToAction("Create", "Category");
            }

            category.Name = model.Name;
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "Category");
        }
    }
}