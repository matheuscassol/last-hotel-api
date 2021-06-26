using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Dtos.Booking
{
    public class BookingUpdateDto : BookingInputDto
    {
        [Required(ErrorMessage = "Booking Id is required")]
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Client Id is required")]
        public Guid? ClientId { get; set; }
    }
}
