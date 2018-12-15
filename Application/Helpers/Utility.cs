using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Application.Models;

namespace Application.Helpers
{
    public static class Utility
    {
        private static readonly dbEntities db = new dbEntities();

        public static User getUser(string UserId)
        {
            return db.Users.FirstOrDefault(User => UserId.Equals(User.Id.ToString()));;
        }
    }
}