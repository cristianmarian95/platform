using Application.Models;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.Helpers;

namespace Application.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly dbEntities _db = new dbEntities();

        public ActionResult Index()
        {
            return View(_db.Services.ToList());
        }

        [Authorize]
        public ActionResult MyProfile()
        {
            MyProfileModel Data = new MyProfileModel()
            {
                FullName = Utility.GetUser(User.Identity.Name).Name,
                Adddress = Utility.GetUser(User.Identity.Name).Address,
                CNP = Utility.GetUser(User.Identity.Name).CNP,
                Email = Utility.GetUser(User.Identity.Name).Email,
                Phone = Utility.GetUser(User.Identity.Name).Phone,
                Group = Utility.GetUser(User.Identity.Name).Group
            };
            
            return View(Data);
        }
    }
}