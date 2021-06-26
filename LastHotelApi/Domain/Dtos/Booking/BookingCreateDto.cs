using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Dtos.Booking
{
    public class BookingCreateDto : BookingInputDto
    {
        [Required(ErrorMessage = "Client Id is required")]
        public Guid? ClientId { get; set; }
    }
}
