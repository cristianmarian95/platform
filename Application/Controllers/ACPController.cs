using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.Helpers;
using Application.Models;

namespace Application.Controllers
{
    [Authorize]
    [Permission(Groups = "Admin")]
    public class ACPController : Controller
    {
        private readonly dbEntities _db = new dbEntities();

        [HttpGet]
        public ActionResult Index()
        {
            return View(_db.Users.ToList());
        }

        [HttpGet]
        public ActionResult User(Guid? id = null)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "ACP");
            }

            if (_db.Users.Any(Data => Data.Id == id.Value))
            {

                User Data = _db.Users.FirstOrDefault(User => User.Id == id.Value);

                return View(new AdminProfileModel
                {
                    Id = Data.Id,
                    FullName = Utility.GetUser(Data.Id.ToString()).Name,
                    Adddress = Utility.GetUser(Data.Id.ToString()).Address,
                    CNP = Utility.GetUser(Data.Id.ToString()).CNP,
                    Email = Utility.GetUser(Data.Id.ToString()).Email,
                    Phone = Utility.GetUser(Data.Id.ToString()).Phone,
                    Group = Utility.GetUser(Data.Id.ToString()).Group
                });
            }
            return RedirectToAction("Index", "ACP");
        }


        [HttpGet]
        public ActionResult DeleteUser(Guid id)
        {
            var data = _db.Users.FirstOrDefault(x => x.Id.Equals(id));

            if (data == null)
            {
                return RedirectToAction("Index", "ACP");
            }

            _db.Users.Remove(data);
            _db.SaveChanges();

            return RedirectToAction("Index", "ACP");
        }


        [HttpPost]
        public ActionResult UpdateUser(AdminProfileModel Model)
        {
            if (ModelState.IsValid)
            {

                User Data = _db.Users.FirstOrDefault(u => u.Id == Model.Id);
                Data.Name = Model.FullName;
                Data.Address = Model.Adddress;
                Data.CNP = Model.CNP;
                Data.Phone = Model.Phone;
                Data.Group = Model.Group;
                Data.Email = Model.Email;

                _db.SaveChanges();

                TempData["success"] = "Updated personal information.";
                return RedirectToAction("User", "ACP", new {id = Model.Id});

            }
            return RedirectToAction("Index", "ACP");
        }

    }
}