using Domain.Dtos.Booking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services.Booking
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingSelectResultDto>> GetAll();
        Task<BookingSelectResultDto> GetById(Guid id);
        Task<BookingCreateResultDto> Create(BookingCreateDto booking);
        Task<BookingUpdateResultDto> Edit(BookingUpdateDto booking);
        Task<bool> Delete(Guid id);
        Task<BookingAvailableResultDto> IsAvailable(BookingInputDto booking);
    }
}
