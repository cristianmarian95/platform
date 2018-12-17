using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Application.Models
{
    public partial class CreateAppointmentModel
    {
        [Required(ErrorMessage = "Plese select a hour for appointment.")]
        public string Hour { set; get; }

        [Required(ErrorMessage = "Plese select the doctor.")]
        public string Doctor { set; get; }

        [Required(ErrorMessage = "Plese select the date.")]
        public string Date { set; get; }

        public Guid Id { set; get; }
    }
}