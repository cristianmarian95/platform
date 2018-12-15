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
    }
}