using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Application.Models
{
    public partial class RegisterModel
    {
        
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Please enter the email address")]
        [EmailAddress(ErrorMessage = "Please enter an valid email address.")]
        public string Email { set; get; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please chose a password")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { set; get; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please confirm the password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string ConfirmPassword { set; get; }

        [Display(Name = "Full Name")] 
        [Required(ErrorMessage = "Please enter your full name")]
        public string FullName { set; get; }

        [Display(Name = "CNP")]
        [Required(ErrorMessage = "Please enter your CNP")]
        public string CNP { set; get; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Please enter address")]
        public string Adddress { set; get; }

        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Please enter your phone number")]
        public string Phone { set; get; }

    }
}