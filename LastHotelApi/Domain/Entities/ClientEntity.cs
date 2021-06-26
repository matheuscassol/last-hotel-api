using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ClientEntity: BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<BookingEntity> Bookings { get; set; }
    }
}
