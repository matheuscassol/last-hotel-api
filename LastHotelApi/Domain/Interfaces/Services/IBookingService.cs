using Domain.Models;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services.Booking
{
    public interface IBookingService : ICrudService<BookingModel>
    {
        Task<BookingModel> IsAvailable(BookingModel booking);
    }
}
