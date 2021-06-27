using Data.Context;
using Data.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Data.Test
{
    public class BookingRepositoryTests : RepositoryTests
    {
        private BookingEntity _bookingEntity;

        public BookingRepositoryTests()
        {
            _bookingEntity = new BookingEntity
            {
                Id = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        [Theory]
        [InlineData(1, 2, true)]
        [InlineData(2, 3, false)]
        [InlineData(2, 4, false)]
        [InlineData(3, 5, false)]
        [InlineData(4, 5, false)]
        [InlineData(5, 6, true)]
        public async Task Should_Check_If_Booking_Is_Available_In_Database_And_Return_Result(int daysToStartDate, int daysToEndDate, bool expectedResult)
        {
            using (var context = _serviceProvider.GetService<HotelContext>())
            {
                _bookingEntity.StartDate = DateTime.UtcNow.Date.AddDays(daysToStartDate);
                _bookingEntity.EndDate = DateTime.UtcNow.Date.AddDays(daysToEndDate).AddSeconds(-1);

                context.Bookings.Add(_bookingEntity);
                await context.SaveChangesAsync();

                var queryEntity = new BookingEntity
                {
                    StartDate = DateTime.UtcNow.Date.AddDays(2),
                    EndDate = DateTime.UtcNow.Date.AddDays(5).AddSeconds(-1)
                };

                BookingRepository repository = new BookingRepository(context);

                var isAvailable = await repository.IsAvailable(queryEntity);

                Assert.Equal(expectedResult, isAvailable);
            }
        }

    }
}
