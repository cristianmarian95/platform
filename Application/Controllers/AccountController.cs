﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Application.Models;
using Application.Helpers;

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

        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
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
                    FormsAuthentication.SetAuthCookie(data.Id.ToString(), false);
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
                    Group = Groups.Member
                };

                db.Users.Add(Data);
                db.SaveChanges();

                TempData["success"] = "Account successfuly created.";
                return RedirectToAction("Register", "Account");
            }

            TempData["danger"] = "Unexpected error please try again.";
            return RedirectToAction("Register", "Account");
        }


        [HttpPost]
        [Authorize]
        public ActionResult UpdateMyProfile(MyProfileModel Model)
        {
            if (ModelState.IsValid)
            {

                User Data = db.Users.FirstOrDefault(u => User.Identity.Name.Equals(u.Id.ToString()));
                Data.Name = Model.FullName;
                Data.Address = Model.Adddress;
                Data.CNP = Model.CNP;
                Data.Phone = Model.Phone;

                TempData["success"] = "Updated personal information.";
                return RedirectToAction("MyProfile", "Home");

            }

            TempData["danger"] = "Unexpected error please try again.";
            return RedirectToAction("MyProfile", "Home");
        }


        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(ChangePasswordModel Model)
        {
            if (ModelState.IsValid)
            {
                User Data = db.Users.FirstOrDefault(u => User.Identity.Name.Equals(u.Id.ToString()));
                if (Model.CurrentPassword.Equals(Data.Password))
                {

                    Data.Password = Model.Password;
                    db.SaveChanges();

                    TempData["success"] = "The passsword successfuly changed.";
                    return RedirectToAction("MyProfile", "Home");

                }

                TempData["danger"] = "The current password is incorect.";
                return RedirectToAction("MyProfile", "Home");

            }

            TempData["danger"] = "Unexpected error please try again.";
            return RedirectToAction("MyProfile", "Home");
        }
    }
}