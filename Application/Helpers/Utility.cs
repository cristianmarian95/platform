using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Application.Models;

namespace Application.Helpers
{
    public static class Utility
    {
        private static readonly dbEntities _db = new dbEntities();

        public static User GetUser(string UserId)
        {
            return _db.Users.FirstOrDefault(User => UserId.Equals(User.Id.ToString()));
        }

        public static ICollection<User> GetDoctors()
        {
            return _db.Users.Where(User => User.Group.Equals(Groups.Doctor)).ToList();
        }

        public static bool IsAdmin()
        {
            return Groups.Admin.Equals(_db.Users.FirstOrDefault(Data => Data.Id.ToString().Equals(HttpContext.Current.User.Identity.Name)).Group);
        }

        public static bool IsDoctor()
        {
            return Groups.Doctor.Equals(_db.Users.FirstOrDefault(Data => Data.Id.ToString().Equals(HttpContext.Current.User.Identity.Name)).Group);
        }
    }
}