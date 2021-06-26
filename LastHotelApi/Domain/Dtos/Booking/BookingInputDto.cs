using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Dtos.Booking
{
    public class BookingInputDto
    {
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        public DateTime? EndDate { get; set; }
    }
}
