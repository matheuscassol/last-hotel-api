using AutoMapper;
using Domain.Dtos.Booking;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Moq;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Service.Test.Booking
{
    public class CreateBookingServiceTests: BookingServiceTests
    {
        [Theory]
        [InlineData(1, 3, false, true, "Not Available")]
        [InlineData(1, 3, true, false, "Invalid Client")]
        [InlineData(1, 4, true, true, "Invalid Period")]
        [InlineData(2, 0, true, true, "Inverted Dates")]
        [InlineData(0, 1, true, true, "Invalid Date")]
        [InlineData(31, 32, true, true, "Invalid Date")]
        public async Task Should_Not_Insert_Booking_And_Add_Notification_When_Invalid(int daysToStartDate, int daysToEndDate, bool isAvailable, bool clientExists, string expectedNotificationKey)
        {
            _mockRepository.Setup(m => m.InsertAsync(BookingEntity)).ReturnsAsync(BookingEntity);
            _mockRepository.Setup(m => m.IsAvailable(BookingEntity)).ReturnsAsync(isAvailable);

            BookingModel.StartDate = DateTime.UtcNow.AddDays(daysToStartDate);
            BookingModel.EndDate = DateTime.UtcNow.AddDays(daysToEndDate);
            _mockMapper.Setup(m => m.Map<BookingEntity>(BookingModel)).Returns(BookingEntity);

            _mockClientRepository.Setup(m => m.ExistsAsync(BookingModel.ClientId)).ReturnsAsync(clientExists);
            var service = new BookingService(_mockRepository.Object, _mockMapper.Object, _mockClientRepository.Object);

            var result = await service.Create(BookingModel);

            _mockRepository.Verify(s => s.InsertAsync(BookingEntity), Times.Never);
            Assert.Collection(result.Notifications, item => {
                Assert.Equal(expectedNotificationKey, item.Key);;
            });
        }
    }
}
