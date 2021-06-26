using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class BookingEntity: BaseEntity
    {
        public Guid ClientId { get; set; }
        public ClientEntity Client { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
