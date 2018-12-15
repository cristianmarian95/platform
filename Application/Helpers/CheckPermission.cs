using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.Models;

namespace Application.Helpers
{
    public class CheckPermission : Attribute
    {
        public CheckPermission()
        {
            AuthorizationContext context = new AuthorizationContext();
           
        }
    }
}