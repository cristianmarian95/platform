using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Application.Models
{
    public partial class MyProfileModel
    {
        [Display(Name = "Email Address")]
        public string Email { set; get; }
       
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

        [Display(Name = "Group")]
        public string Group { set; get; }
    }
}