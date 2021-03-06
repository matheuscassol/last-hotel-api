using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos.Booking
{
    public class BookingIsAvailableResultDto: Notifiable<Notification>
    {
        public BookingIsAvailableResultDto(bool isAvalailable)
        {
            IsAvailable = isAvalailable;
        }
        public bool IsAvailable { get; set; }
    }
}
