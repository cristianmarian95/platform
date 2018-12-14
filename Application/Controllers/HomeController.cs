using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Application.Controllers
{
    public class HomeController : Controller
    {
        private dbEntities db = new dbEntities();

        public ActionResult Index()
        {
            return View();
        }
    }
}