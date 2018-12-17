using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.Helpers;
using Application.Models;

namespace Application.Controllers
{
    public class AppointmentController : Controller
    {

        private readonly dbEntities _db = new dbEntities();

        [HttpPost]
        [Authorize]
        public ActionResult CreateAppointmentAction(CreateAppointmentModel Model)
        {
            if (ModelState.IsValid)
            {

                string AppDate =  Model.Hour + ":00-" + (Int32.Parse(Model.Hour) + 1).ToString() + ":00 - " + Model.Date;

                if (_db.Appointments.Any(App => App.FK_Doctor.ToString().Equals(Model.Doctor.ToString()) && App.Date.Equals(AppDate)))
                {
                    TempData["danger"] = "Please select other date or doctor.";
                    return RedirectToAction("View", "Service", new {id = Model.Id});
                }

                Guid AppId = Guid.NewGuid();

                _db.Appointments.Add(new Appointment
                {
                    Id = AppId,
                    FK_Doctor = Guid.Parse(Model.Doctor),
                    FK_User_Appointed = Guid.Parse(User.Identity.Name),
                    Date = AppDate,
                    IsConfirmed = false
                });

                _db.SaveChanges();

                _db.Appointments_Service.Add(new Appointments_Service
                {
                    Id = Guid.NewGuid(),
                    FK_Appointment = AppId,
                    FK_Service = Model.Id
                });

                _db.SaveChanges();

                TempData["success"] = "Your appoiment has been created.";
                return RedirectToAction("View", "Service", new {id = Model.Id});
            }

            TempData["danger"] = "Unexpected error please try again.";
            return RedirectToAction("View", "Service", new {id = Model.Id});
        }

        [HttpGet]
        [Authorize]
        public ActionResult MyServices()
        {
            return View(_db.Appointments.Where(App => App.FK_User_Appointed.ToString().Equals(User.Identity.Name)).ToList());
        }


        [HttpGet]
        [Authorize]
        public ActionResult DeleteAppointment(Guid? id = null)
        {
            if (id == null)
            {
                return RedirectToAction("MyServices", "Appointment");
            }

            if (_db.Appointments.Any(App => App.Id == id.Value))
            {
                Appointment App = _db.Appointments.FirstOrDefault(Data => Data.Id == id.Value);
                Appointments_Service Service = _db.Appointments_Service.FirstOrDefault(Data => Data.FK_Appointment == App.Id);

                _db.Appointments.Remove(App);
                _db.Appointments_Service.Remove(Service);
                _db.SaveChanges();

                TempData["success"] = "The service was canceled!";
                return RedirectToAction("MyServices", "Appointment");
            }

            TempData["danger"] = "Unexpected error please try again";
            return RedirectToAction("MyServices", "Appointment");
        }


        [HttpGet]
        [Authorize]
        [Permission(Groups = "Admin,Doctor")]
        public ActionResult Confirm()
        {
            return View(_db.Appointments.Where(Data => User.Identity.Name.Equals(Data.FK_Doctor.ToString())).ToList());
        }

        [HttpGet]
        [Authorize]
        [Permission(Groups = "Admin,Doctor")]
        public ActionResult ConfirmAppointment(Guid? id = null)
        {
            if (id == null)
            {
                return RedirectToAction("Confirm", "Appointment");
            }

            if (_db.Appointments.Any(App => App.Id == id.Value))
            {
                Appointment App = _db.Appointments.FirstOrDefault(Data => Data.Id == id.Value);

                App.IsConfirmed = true;

                _db.SaveChanges();

                TempData["success"] = "The service was confirmed!";
                return RedirectToAction("Confirm", "Appointment");
            }

            TempData["danger"] = "Unexpected error please try again";
            return RedirectToAction("Confirm", "Appointment");
        }

        [HttpGet]
        [Authorize]
        [Permission(Groups = "Admin,Doctor")]
        public ActionResult CancelAppointment(Guid? id = null)
        {
            if (id == null)
            {
                return RedirectToAction("MyServices", "Appointment");
            }

            if (_db.Appointments.Any(App => App.Id == id.Value))
            {
                Appointment App = _db.Appointments.FirstOrDefault(Data => Data.Id == id.Value);
                Appointments_Service Service = _db.Appointments_Service.FirstOrDefault(Data => Data.FK_Appointment == App.Id);

                _db.Appointments.Remove(App);
                _db.Appointments_Service.Remove(Service);
                _db.SaveChanges();

                TempData["success"] = "The service was canceled!";
                return RedirectToAction("MyServices", "Appointment");
            }

            TempData["danger"] = "Unexpected error please try again";
            return RedirectToAction("MyServices", "Appointment");
        }

    }
}