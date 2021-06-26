using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos.Booking
{
    public class BookingCreateResultDto: Notifiable<Notification>
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
