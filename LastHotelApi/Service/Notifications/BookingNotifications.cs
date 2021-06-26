using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Notifications
{
    public class BookingNotifications
    {
        public static Notification InvertedDates => new Notification("Inverted Dates", "Start Date must be prior to End Date");
        public static Notification InvalidPeriod => new Notification("Invalid Period", "Booking period must not be greated than 3 days");
        public static Notification InvalidClient => new Notification("Invalid Client", "Client must be registered");
        public static Notification InvalidDate => new Notification("Invalid Date", "Booking must be made from 1 to 30 days in advance");
        public static Notification NotAvailable => new Notification("Not Available", "Room is not available");
    }
}
