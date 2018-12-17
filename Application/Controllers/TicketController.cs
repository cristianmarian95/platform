using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.Helpers;
using Application.Models;

namespace Application.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private readonly dbEntities _db = new dbEntities();

        [HttpGet]
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.Name);
            var list = _db.Tickets.Where(x => x.FK_User.Equals(userId)).ToList();
            list.Sort((x, y) => x.IsClosed.CompareTo(y.IsClosed));
            var model = new TicketListModel
            {
                Ticktes = list
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Admin()
        {
            var list = _db.Tickets.ToList();
            list.Sort((x, y) => x.IsClosed.CompareTo(y.IsClosed));

            var model = new TicketAdminListModel
            {
                Ticktes = list
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new TicketCreateModel
            {
                FkUser = User.Identity.Name
            });
        }

        [HttpGet]
        public ActionResult CreateResponse(Guid id)
        {
            var model = new TicketResponseCreateModel
            {
                FkTicket = id.ToString(),
                FkUser = User.Identity.Name,
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult View(Guid id)
        {
            var model = new TicketViewModel
            {
                Ticket = _db.Tickets.FirstOrDefault(x => x.Id.Equals(id)),
                Response = _db.Ticket_Response.Where(x => x.FK_Ticket.Equals(id)).ToList()
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult CloseTicket(Guid idTicket)
        {
            var ticket = _db.Tickets.FirstOrDefault(x => x.Id.Equals(idTicket));
            if (ticket == null)
            {
                return RedirectToAction("View", new
                {
                    id = idTicket
                });
            }

            ticket.IsClosed = true;
            _db.Entry(ticket).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("Index", "Ticket");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateTicket(TicketCreateModel model)
        {

            if (model.Content == null)
            {
                return RedirectToAction("Index", "Ticket");
            }

            var ticketId = Guid.NewGuid();
            var ticket = new Ticket
            {
                Id = ticketId,
                IsClosed = false,
                Title = model.Title,
                Content = model.Content,
                FK_User = Guid.Parse(User.Identity.Name)
            };

            _db.Tickets.Add(ticket);
            _db.SaveChanges();

            return RedirectToAction("View", new
            {
                id = ticketId
            });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateResponseTicket(TicketResponseCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("View", "Ticket");
            }

            var ticketId = Guid.Parse(model.FkTicket);

            var ticketResponse = new Ticket_Response
            {
                Id = Guid.NewGuid(),
                FK_User = Guid.Parse(User.Identity.Name),
                Content = model.Content,
                FK_Ticket = Guid.Parse(model.FkTicket),
            };

            _db.Ticket_Response.Add(ticketResponse);
            _db.SaveChanges();

            return RedirectToAction("View", new
            {
                id = ticketId
            });
        }


    }
}