using Application.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.Helpers;

namespace Application.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly dbEntities _db = new dbEntities();

        [HttpGet]
        [Permission(Groups = "Admin")]
        public ActionResult Index()
        {

            return View(new CategoryListModel
            {
                Categories = _db.Categories.ToList()

            });
        }

        [HttpGet]
        [Permission(Groups = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        [Permission(Groups = "Admin")]
        public ActionResult Edit(Guid Id)
        {
            var category = _db.Categories.FirstOrDefault(x => x.Id.Equals(Id));

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
        [Permission(Groups = "Admin")]
        public ActionResult Delete(Guid Id)
        {
            var category = _db.Categories.FirstOrDefault(x => x.Id.Equals(Id));

            if (category == null)
            {
                TempData["danger"] = "Invalid ID on Category";
                return RedirectToAction("Index", "Category");
            }

            _db.Categories.Remove(category);
            _db.SaveChanges();

            return RedirectToAction("Index", "Category");
        }

        [HttpPost]
        [Permission(Groups = "Admin")]
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

            _db.Categories.Add(cat);
            _db.SaveChanges();

            return RedirectToAction("Index", "Category");
        }

        [HttpPost]
        [Permission(Groups = "Admin")]
        public ActionResult EditAction(CategoryEditModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["danger"] = "Error";
                return RedirectToAction("Create", "Category");
            }

            var category = _db.Categories.FirstOrDefault(x => x.Id.Equals(model.Id));
            if (category == null)
            {
                TempData["danger"] = "Invalid ID";
                return RedirectToAction("Create", "Category");
            }

            category.Name = model.Name;
            _db.Entry(category).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("Index", "Category");
        }
    }
}