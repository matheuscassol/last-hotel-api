using AutoMapper;
using Domain.Interfaces.Repositories;
using Moq;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Service.Test.Booking
{
    public class DeleteBookingServiceTests: BookingServiceTests
    {
        [Fact]
        public async Task Should_Delete_Booking()
        {
            _mockRepository.Setup(m => m.DeleteAsync(BookingId)).ReturnsAsync(true);
            var service = new BookingService(_mockRepository.Object, _mockMapper.Object, _mockClientRepository.Object);
            
            var result = await service.Delete(BookingId);

            Assert.True(result);
        }
    }
}
