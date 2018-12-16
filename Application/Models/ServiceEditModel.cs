using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Application.Models
{
    public partial class ServiceEditModel
    { 
        public Guid Id { get; set; }

        [Display(Name = "Service Name")]
        [Required(ErrorMessage = "Please enter your category name")]
        public string Name { get; set; }

        [Display(Name = "Service Description")]
        [Required(ErrorMessage = "Please enter your category name")]
        public string Description { get; set; }

        [Display(Name = "Service Price")]
        [Required(ErrorMessage = "Please enter your category name")]
        public double Price { get; set; }

        public string FkCategory { get; set; }

        public List<Category> Categories { get; set; }
    }
}