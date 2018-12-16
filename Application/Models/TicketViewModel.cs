using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Models
{
    public partial class TicketViewModel
    {
        public Ticket Ticket { get; set; }

        public List<Ticket_Response> Response { get; set; }
    }
}