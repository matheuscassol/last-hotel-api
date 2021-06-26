using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IBookingRepository: IRepository<BookingEntity>
    {
        Task<bool> IsAvailable(BookingEntity booking);
    }
}
