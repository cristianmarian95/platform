using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebGrease.Css;

namespace Application.Models
{
    public partial class LoginModel
    {
        
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Please enter an valid email address.")]
        public string Email { set; get; }

        
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter your account password.")]
        public string Password { set; get; }

    }
}