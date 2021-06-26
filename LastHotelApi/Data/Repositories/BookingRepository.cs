using Data.Context;
using Data.Repository;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class BookingRepository : BaseRepository<BookingEntity>, IBookingRepository
    {
        public BookingRepository(HotelContext context) : base(context)
        {

        }

        public async Task<bool> IsAvailable(BookingEntity booking)
        {
            return !await _dataset.AnyAsync(x => x.Id != booking.Id && x.StartDate <= booking.EndDate && booking.StartDate <= x.EndDate);
        }
    }
}
