using AutoMapper;
using Domain.Dtos.Booking;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Moq;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Service.Test.Booking
{
    public class IsAvailableBookingServiceTests : BookingServiceTests
    {
        [Fact]
        public async Task Should_Check_If_Booking_Is_Available_When_Query_Is_Valid()
        {
            _mockRepository.Setup(m => m.IsAvailable(BookingEntity)).ReturnsAsync(true);
            _mockMapper.Setup(m => m.Map<BookingEntity>(BookingModel)).Returns(BookingEntity);
            var service = new BookingService(_mockRepository.Object, _mockMapper.Object, _mockClientRepository.Object);
            
            var result = await service.IsAvailable(BookingModel);

            Assert.True(result.IsAvailable);
            Assert.Empty(result.Notifications);
        }

        //I left this one as Theory because there might be more validations in the future
        [Theory]
        [InlineData(2, 1, "Inverted Dates")]
        public async Task Should_Not_Check_If_Booking_Is_Available_And_Add_Notification_When_Invalid(int daysToStartDate, int daysToEndDate, string notificationKey)
        {
            BookingModel.StartDate = DateTime.UtcNow.AddDays(daysToStartDate);
            BookingModel.EndDate = DateTime.UtcNow.AddDays(daysToEndDate);

            var service = new BookingService(_mockRepository.Object, _mockMapper.Object, _mockClientRepository.Object);
            
            var result = await service.IsAvailable(BookingModel);

            _mockRepository.Verify(s => s.IsAvailable(BookingEntity), Times.Never);
            Assert.Collection(result.Notifications, item => {
                Assert.Equal(notificationKey, item.Key); ;
            });
        }
    }
}
