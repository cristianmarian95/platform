using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.Models;

namespace Application.Helpers
{
    public class Permission : ActionFilterAttribute
    {
        public string Groups { get; set; }
        private readonly  dbEntities db = new dbEntities();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string UserId = filterContext.HttpContext.User.Identity.Name;
            string[] Roles;

            User Data = db.Users.FirstOrDefault(User => UserId.Equals(User.Id.ToString()));

            try
            {
                Roles= Groups.Split(',');
            }
            catch (Exception)
            {
                Roles = new string[]{Groups};

            }
            
            foreach (string Group in Roles)
            {
                if (Group.Equals(Data.Group))
                {
                    return;
                }
            }

            filterContext.Result = new RedirectResult("/Home/Index");
        }
    }
}