using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Application.Models;

namespace Application.Controllers
{
    public class AccountController : Controller
    {
        private readonly dbEntities db = new dbEntities();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LoginAction(LoginModel Model)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(User => User.Email == Model.Email && User.Password == Model.Password))
                {
                    User data = db.Users.FirstOrDefault(User => User.Email == Model.Email && User.Password == Model.Password);
                    FormsAuthentication.SetAuthCookie(data.Id.ToString(), true);
                    return RedirectToAction("Index", "Home");
                }

                TempData["danger"] = "Email address or password incorect.";
                return RedirectToAction("Login", "Account");
            }

            TempData["danger"] = "Unexpected error please try again.";
            return RedirectToAction("Login", "Account");
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterAction(RegisterModel Model)
        {
            if (ModelState.IsValid)
            {

                if (db.Users.Any(User => User.Email == Model.Email))
                {
                    TempData["danger"] = "The email address is already used.";
                    return RedirectToAction("Register", "Account");
                }

                if (db.Users.Any(User => User.CNP == Model.CNP))
                {
                    TempData["danger"] = "The CNP is already used.";
                    return RedirectToAction("Register", "Account");
                }

                if (db.Users.Any(User => User.Phone == Model.Phone))
                {
                    TempData["danger"] = "The phone number is already used.";
                    return RedirectToAction("Register", "Account");
                }

                User Data = new User
                {
                    Id = Guid.NewGuid(),
                    Email = Model.Email,
                    Password = Model.Password,
                    Name = Model.FullName,
                    Address = Model.Adddress,
                    Phone = Model.Phone,
                    CNP = Model.CNP,
                    Group = "Member"
                };

                db.Users.Add(Data);
                db.SaveChanges();

                TempData["success"] = "Account successfuly created.";
                return RedirectToAction("Register", "Account");
            }

            TempData["danger"] = "Unexpected error please try again.";
            return RedirectToAction("Register", "Account");
        }
        
    }
}