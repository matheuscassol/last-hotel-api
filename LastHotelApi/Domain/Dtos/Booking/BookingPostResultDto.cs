using System;

namespace Domain.Dtos.Booking
{
    public class BookingPostResultDto: BasePostResultDto
    {
        public Guid ClientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
