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
    public class UpdateBookingServiceTests: BookingServiceTests
    {
        [Fact]
        public async Task Should_Update_Booking_And_Return_Result_When_Booking_Is_Valid()
        {
            _mockRepository.Setup(m => m.UpdateAsync(BookingEntity)).ReturnsAsync(BookingEntity);
            _mockRepository.Setup(m => m.IsAvailable(BookingEntity)).ReturnsAsync(true);

            _mockMapper.Setup(m => m.Map<BookingModel>(BookingUpdateDto)).Returns(BookingModel);
            _mockMapper.Setup(m => m.Map<BookingEntity>(BookingModel)).Returns(BookingEntity);
            _mockMapper.Setup(m => m.Map<BookingUpdateResultDto>(BookingEntity)).Returns(BookingUpdateResultDto);

            _mockClientRepository.Setup(m => m.ExistsAsync(BookingModel.ClientId)).ReturnsAsync(true);

            var service = new BookingService(_mockRepository.Object, _mockMapper.Object, _mockClientRepository.Object);
            
            var result = await service.Edit(BookingUpdateDto);

            Assert.Equal(BookingUpdateResultDto, result);
        }

        [Theory]
        [InlineData(1, 4, false, true, "Not Available")]
        [InlineData(1, 4, true, false, "Invalid Client")]
        [InlineData(1, 5, true, true, "Invalid Period")]
        [InlineData(2, 1, true, true, "Inverted Dates")]
        [InlineData(0, 2, true, true, "Invalid Date")]
        [InlineData(31, 33, true, true, "Invalid Date")]
        public async Task Should_Not_Update_Booking_And_Add_Notification_When_Invalid(int daysToStartDate, int daysToEndDate, bool isAvailable, bool clientExists, string notificationKey)
        {
            _mockRepository.Setup(m => m.UpdateAsync(BookingEntity)).ReturnsAsync(BookingEntity);
            _mockRepository.Setup(m => m.IsAvailable(BookingEntity)).ReturnsAsync(isAvailable);
            
            BookingModel.StartDate = DateTime.UtcNow.AddDays(daysToStartDate);
            BookingModel.EndDate = DateTime.UtcNow.AddDays(daysToEndDate);

            _mockMapper.Setup(m => m.Map<BookingModel>(BookingUpdateDto)).Returns(BookingModel);
            _mockMapper.Setup(m => m.Map<BookingUpdateResultDto>(BookingModel)).Returns(BookingUpdateResultDto);
            _mockMapper.Setup(m => m.Map<BookingEntity>(BookingModel)).Returns(BookingEntity);

            _mockClientRepository.Setup(m => m.ExistsAsync(BookingModel.ClientId)).ReturnsAsync(clientExists);

            var service = new BookingService(_mockRepository.Object, _mockMapper.Object, _mockClientRepository.Object);
            var result = await service.Edit(BookingUpdateDto);

            _mockRepository.Verify(s => s.UpdateAsync(BookingEntity), Times.Never);
            Assert.Collection(BookingModel.Notifications, item => {
                Assert.Equal(notificationKey, item.Key);;
            });
        }
    }
}
