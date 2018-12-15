using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Application.Models
{
    public class CategoryEditModel
    {
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Please enter your category name")]
        public string Name { get; set; }
        public Guid Id { get; set; }
    }
}