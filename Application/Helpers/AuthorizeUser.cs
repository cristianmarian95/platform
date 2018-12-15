using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Application.Models;

namespace Application.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizeUser : AuthorizeAttribute
    {
        public string AccessLevel { get; set; }
        private readonly dbEntities db = new dbEntities();

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {                
                return false;
            }

            string privilegeLevels = string.Join("", db.Users.First(User => httpContext.User.Identity.Name.Equals(User.Id.ToString())).Group);
            return privilegeLevels.Contains(this.AccessLevel);           
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    { 
                        controller = "Account", 
                        action = "Login" 
                    })
            );
        }

    }
}