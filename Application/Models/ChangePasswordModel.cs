using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Application.Models
{
    public partial class ChangePasswordModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "Current Passsword")]
        [Required(ErrorMessage = "Please enter your current password.")]
        public string CurrentPassword { set; get; }

        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        [Required(ErrorMessage = "Please chose a new password")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { set; get; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Please confirm the password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string ConfirmPassword { set; get; }
    }
}