using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Application.Models
{
    public class TicketResponseCreateModel
    {
        [Display(Name = "Ticket Content")]
        [Required(ErrorMessage = "Please enter your content")]
        public string Content { get; set; }

        public string FkUser { get; set; }

        public string FkTicket { get; set; }
    }
}