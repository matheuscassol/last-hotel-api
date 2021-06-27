using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class BookingModel : Notifiable<Notification>
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value.Date; }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value.Date.AddDays(1).AddSeconds(-1); }
        }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsAvailable { get; set; } = false;
    }
}
