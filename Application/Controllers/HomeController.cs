using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.Helpers;

namespace Application.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly dbEntities db = new dbEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyProfile()
        {
            MyProfileModel Data = new MyProfileModel()
            {
                FullName = Utility.getUser(User.Identity.Name).Name,
                Adddress = Utility.getUser(User.Identity.Name).Address,
                CNP = Utility.getUser(User.Identity.Name).CNP,
                Email = Utility.getUser(User.Identity.Name).Email,
                Phone = Utility.getUser(User.Identity.Name).Phone,
                Group = Utility.getUser(User.Identity.Name).Group
            };
            
            return View(Data);
        }
    }
}