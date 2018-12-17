using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Application.Models
{
    public class TicketCreateModel
    {
        [Display(Name = "Ticket Title")]
        [Required(ErrorMessage = "Please enter your title name")]
        public string Title { get; set; }

        [Display(Name = "Ticket Content")]
        [Required(ErrorMessage = "Please enter your content")]
        public string Content { get; set; }
        public string FkUser { get; set; }
        public bool IsClosed { get; set; }

    }
}